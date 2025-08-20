using FeedBackApp.Core.QuestionnaireUtils.ReviewUtils.ReviewContracts;
using FeedBackApp.Core.QuestionnaireUtils.ReviewUtils.ReviewUtilityPOCOs;

namespace FeedBackApp.Core.QuestionnaireUtils.ReviewUtils
{
    public sealed class AdministratorReview<TReviewFormat> : IReviewCompiler<Administrator> where TReviewFormat : IReviewFormat, new()
    {

        public AdministratorReview() { }
        public ReviewFile GenerateFor(Administrator recipient)
        {
            throw new NotImplementedException();
        }
    }
}
