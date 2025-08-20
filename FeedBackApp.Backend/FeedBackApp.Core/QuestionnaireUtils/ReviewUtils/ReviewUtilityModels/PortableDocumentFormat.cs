using FeedBackApp.Core.QuestionnaireUtils.ReviewUtils.ReviewContracts;

namespace FeedBackApp.Core.QuestionnaireUtils.ReviewUtils.ReviewUtilityModels
{
    public sealed class PortableDocumentFormat : IReviewFormat
    {
        public string FileExtension => "pdf";

        public string MimeType => "application/pdf";

        public byte[] Render(object model)
        {
            //itt lesz actually a formatum;
            return [];
        }
    }
}
