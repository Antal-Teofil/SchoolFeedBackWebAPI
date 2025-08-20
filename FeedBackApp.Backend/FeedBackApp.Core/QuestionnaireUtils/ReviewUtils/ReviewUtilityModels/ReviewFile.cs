namespace FeedBackApp.Core.QuestionnaireUtils.ReviewUtils.ReviewUtilityPOCOs
{
    public sealed record ReviewFile(string FileName, string ContentType, byte[] Bytes);
}
