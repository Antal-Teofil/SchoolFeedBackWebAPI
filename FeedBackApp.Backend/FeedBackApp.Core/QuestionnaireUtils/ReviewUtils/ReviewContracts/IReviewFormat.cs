namespace FeedBackApp.Core.QuestionnaireUtils.ReviewUtils.ReviewContracts
{
    public interface IReviewFormat
    {
        public string FileExtension { get; }
        string MimeType { get; }
        byte[] Render(object model);
    }
}
