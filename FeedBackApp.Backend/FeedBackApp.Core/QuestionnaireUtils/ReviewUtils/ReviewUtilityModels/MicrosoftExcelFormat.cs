using FeedBackApp.Core.QuestionnaireUtils.ReviewUtils.ReviewContracts;

namespace FeedBackApp.Core.QuestionnaireUtils.ReviewUtils.ReviewUtilityModels
{
    public sealed class MicrosoftExcelFormat : IReviewFormat
    {
        public string FileExtension => "xlsx";

        public string MimeType => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        public byte[] Render(object model)
        {
            //excel formatum kigeneralasa
            return [];
        }
    }
}
