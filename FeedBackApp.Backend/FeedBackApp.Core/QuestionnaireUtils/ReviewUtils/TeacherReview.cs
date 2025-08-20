using FeedBackApp.Core.QuestionnaireUtils.ReviewUtils.ReviewContracts;
using FeedBackApp.Core.QuestionnaireUtils.ReviewUtils.ReviewUtilityPOCOs;

namespace FeedBackApp.Core.QuestionnaireUtils.ReviewUtils
{
    public sealed class TeacherReview<TFormat> : IReviewCompiler<Teacher> where TFormat : IReviewFormat, new()
    {
        public ReviewFile GenerateFor(Teacher recipient)
        {
            var fmt = new TFormat();
            var model = new
            {
                //dolgok

            };

            var bytes = fmt.Render(model);
            var fileName = "";

            return new ReviewFile(fileName, fmt.MimeType, bytes);
        }
    }
}
