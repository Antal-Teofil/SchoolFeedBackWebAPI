using Newtonsoft.Json;
using System.Globalization;
using System.Text;

namespace FeedBackApp.Core.QuestionnaireGeneratorUtils;

public sealed class QuestionnaireGenerator
{
    public string SourceFilePath { get; }
    public FileInfo SourceFileInfo { get; }

    private QuestionnaireGenerator(FileInfo sourceInfo)
    {
        SourceFileInfo = sourceInfo;
        SourceFilePath = sourceInfo.FullName;
    }

    public static QuestionnaireGenerator InitializeSourceFile(FileInfo sourceInfo)
    {
        if (sourceInfo is null)
            throw new ArgumentNullException(nameof(sourceInfo), "A fájl hivatkozás nem lehet null.");

        var fullPath = Path.GetFullPath(sourceInfo.FullName);
        var fi = new FileInfo(fullPath);

        if (!string.Equals(fi.Extension, ".json", StringComparison.OrdinalIgnoreCase))
            throw new ArgumentException("Csak .json fájl engedélyezett.", nameof(sourceInfo));

        if (!fi.Exists)
            throw new FileNotFoundException("A megadott .json fájl nem létezik.", fi.FullName);

        if (fi.Length == 0)
            throw new InvalidOperationException("A .json fájl üres (0 bájt).");

        // opcionális
        try
        {
            using var _ = fi.Open(FileMode.Open, FileAccess.Read, FileShare.Read);
        }
        catch (Exception ex)
        {
            throw new IOException($"A .json fájl nem olvasható: {fi.FullName}", ex);
        }

        return new QuestionnaireGenerator(fi);
    }

    public async Task<int> GenerateQuestionnaires(string outputFilePath, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(outputFilePath))
            throw new ArgumentException("A kimeneti fájl útvonala nem lehet üres.", nameof(outputFilePath));

        // Input: async + sequential scan
        await using var fsIn = new FileStream(
            SourceFilePath,
            FileMode.Open,
            FileAccess.Read,
            FileShare.Read,
            bufferSize: 64 * 1024,
            options: FileOptions.Asynchronous | FileOptions.SequentialScan);

        using var sr = new StreamReader(fsIn, Encoding.UTF8, detectEncodingFromByteOrderMarks: true, bufferSize: 64 * 1024, leaveOpen: false);
        using var jr = new JsonTextReader(sr)
        {
            CloseInput = false,
            SupportMultipleContent = false,
            DateParseHandling = DateParseHandling.None,    // small perf win
            FloatParseHandling = FloatParseHandling.Double // default, explicit
        };

        // Output: async + large buffer; UTF8 without BOM
        await using var fsOut = new FileStream(
            outputFilePath,
            FileMode.Append,
            FileAccess.Write,
            FileShare.Read,
            bufferSize: 64 * 1024,
            options: FileOptions.Asynchronous | FileOptions.SequentialScan);

        using var sw = new StreamWriter(fsOut, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false), bufferSize: 64 * 1024, leaveOpen: false);
        using var jw = new JsonTextWriter(sw)
        {
            CloseOutput = false,
            Formatting = Formatting.Indented
        };

        var written = 0;
        var foundTeachers = false;
        var nowIso = DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture);

        // Helper locals
        //static string? ReadTrimmedString(JToken? t) => (t as JValue)?.Value as string is string s ? s.Trim() : t?.Value<string>()?.Trim();

        while (await jr.ReadAsync(cancellationToken).ConfigureAwait(false))
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (jr.TokenType == JsonToken.PropertyName &&
                string.Equals(jr.Value as string, "teachers", StringComparison.Ordinal))
            {
                foundTeachers = true;

                // Expect start of array
                if (!await jr.ReadAsync(cancellationToken).ConfigureAwait(false) || jr.TokenType != JsonToken.StartArray)
                    throw new JsonReaderException("A 'teachers' property értéke tömb (array) kell legyen.");

                // Iterate teachers array
                while (await jr.ReadAsync(cancellationToken).ConfigureAwait(false) && jr.TokenType != JsonToken.EndArray)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    if (jr.TokenType != JsonToken.StartObject)
                    {
                        await jr.SkipAsync(cancellationToken).ConfigureAwait(false);
                        continue;
                    }

                    string? teacherName = null;

                    // We will collect subjects as we see them
                    // For each subject we will also collect unique student emails
                    // Structure: teacher { name, subjects: [ { name, students: [...] }, ... ] }
                    while (await jr.ReadAsync(cancellationToken).ConfigureAwait(false) && jr.TokenType != JsonToken.EndObject)
                    {
                        cancellationToken.ThrowIfCancellationRequested();

                        if (jr.TokenType != JsonToken.PropertyName)
                            continue;

                        var propName = jr.Value as string;

                        if (string.Equals(propName, "name", StringComparison.Ordinal))
                        {
                            if (!await jr.ReadAsync(cancellationToken).ConfigureAwait(false))
                                break;

                            teacherName = (jr.Value as string)?.Trim();
                        }
                        else if (string.Equals(propName, "subjects", StringComparison.Ordinal))
                        {
                            if (!await jr.ReadAsync(cancellationToken).ConfigureAwait(false) || jr.TokenType != JsonToken.StartArray)
                                throw new JsonReaderException("A 'subjects' property tömb kell legyen.");

                            // Iterate subjects
                            while (await jr.ReadAsync(cancellationToken).ConfigureAwait(false) && jr.TokenType != JsonToken.EndArray)
                            {
                                cancellationToken.ThrowIfCancellationRequested();

                                if (jr.TokenType != JsonToken.StartObject)
                                {
                                    await jr.SkipAsync(cancellationToken).ConfigureAwait(false);
                                    continue;
                                }

                                string? subjectName = null;
                                HashSet<string>? uniqueStudents = null;

                                // subject object
                                while (await jr.ReadAsync(cancellationToken).ConfigureAwait(false) && jr.TokenType != JsonToken.EndObject)
                                {
                                    cancellationToken.ThrowIfCancellationRequested();

                                    if (jr.TokenType != JsonToken.PropertyName)
                                        continue;

                                    var subjProp = jr.Value as string;

                                    if (string.Equals(subjProp, "name", StringComparison.Ordinal))
                                    {
                                        if (!await jr.ReadAsync(cancellationToken).ConfigureAwait(false))
                                            break;
                                        subjectName = (jr.Value as string)?.Trim();
                                    }
                                    else if (string.Equals(subjProp, "students", StringComparison.Ordinal))
                                    {

                                        if (!await jr.ReadAsync(cancellationToken).ConfigureAwait(false) || jr.TokenType != JsonToken.StartArray)
                                            throw new JsonReaderException("A 'students' property tömb kell legyen.");

                                        uniqueStudents ??= new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                                        while (await jr.ReadAsync(cancellationToken).ConfigureAwait(false) && jr.TokenType != JsonToken.EndArray)
                                        {
                                            cancellationToken.ThrowIfCancellationRequested();

                                            if (jr.TokenType == JsonToken.String && jr.Value is string emailRaw)
                                            {
                                                var email = emailRaw.Trim();
                                                if (email.Length != 0)
                                                    uniqueStudents.Add(email);
                                            }
                                            else
                                            {
                                                await jr.SkipAsync(cancellationToken).ConfigureAwait(false);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // skip unknown property
                                        await jr.SkipAsync(cancellationToken).ConfigureAwait(false);
                                    }
                                }

                                if (!string.IsNullOrWhiteSpace(teacherName) &&
                                    !string.IsNullOrWhiteSpace(subjectName) &&
                                    uniqueStudents is { Count: > 0 })
                                {

                                    // ez lehetne egy kulon fuggveny, mert ide jonnek a kerdoivek.
                                    foreach (var studentId in uniqueStudents)
                                    {
                                        cancellationToken.ThrowIfCancellationRequested();

                                        // Optional: deterministic ID for idempotency
                                        // var id = ComputeStableId(studentId, teacherName!, subjectName!);
                                        var id = Guid.NewGuid().ToString("D");

                                        // Write one NDJSON object
                                        jw.WriteStartObject();
                                        jw.WritePropertyName("id"); jw.WriteValue(id);
                                        jw.WritePropertyName("studentId"); jw.WriteValue(studentId);
                                        jw.WritePropertyName("teacherName"); jw.WriteValue(teacherName);
                                        jw.WritePropertyName("subjectName"); jw.WriteValue(subjectName);
                                        jw.WritePropertyName("partitionKey"); jw.WriteValue(studentId);
                                        jw.WritePropertyName("status"); jw.WriteValue("empty");
                                        jw.WritePropertyName("createdAt"); jw.WriteValue(nowIso);
                                        jw.WriteEndObject();
                                        await jw.FlushAsync(cancellationToken).ConfigureAwait(false);
                                        await sw.WriteLineAsync().ConfigureAwait(false); // newline for NDJSON

                                        written++;
                                    }
                                }
                            }
                        }
                        else
                        {
                            await jr.SkipAsync(cancellationToken).ConfigureAwait(false);
                        }
                    }
                }

                break; // done with 'teachers'
            }
        }

        await sw.FlushAsync().ConfigureAwait(false);

        if (!foundTeachers)
            throw new InvalidDataException($"A forrás JSON-ban nem található 'teachers' property. Forrás: {SourceFilePath}");

        return written;
    }

    // Example deterministic ID (optional)
    private static string ComputeStableId(string student, string teacher, string subject)
    {
        using var sha = System.Security.Cryptography.SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes($"{student}|{teacher}|{subject}");
        var hash = sha.ComputeHash(bytes);
        // Base32/Hex are both fine; Hex shown here
        return Convert.ToHexString(hash);
    }

}
