namespace FeedBackApp.Core.QuestionnaireGeneratorUtils;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

    internal class Application
    {
        private static async Task<int> Main(string[] args)
        {
            try
            {

                var projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\.."));
                var inputPath = Path.Combine(projectRoot, "teachers.json");
                var outputPath = Path.Combine(projectRoot, "text.txt");

                var gen = QuestionnaireGenerator.InitializeSourceFile(new FileInfo(inputPath));

                using var cts = new CancellationTokenSource();
                // optional timeout: cts.CancelAfter(TimeSpan.FromSeconds(10));

                var count = await gen.GenerateQuestionnaires(outputPath, cts.Token);

                Console.WriteLine($"OK — {count} record(s) appended to: {outputPath}");
                return 0;
            }
            catch (OperationCanceledException)
            {
                Console.Error.WriteLine("Cancelled.");
                return 2;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
                return 1;
            }
        }
}
