using FeedBackApp.Core.QuestionnaireUtils.ReviewUtils.ReviewContracts;
using FeedBackApp.Core.QuestionnaireUtils.ReviewUtils.ReviewUtilityPOCOs;

namespace FeedBackApp.Core.QuestionnaireUtils.ReviewUtils
{
    public static class ReviewCompiler
    {
        // TEACHER
        public static List<ReviewFile> CompileReviews<TReview>(
            RecipientContainer<Teacher> container)
            where TReview : IReviewCompiler<Teacher>, new()
        {
            var gen = new TReview();
            var result = new List<ReviewFile>(container.Count);
            foreach (var r in container)
                result.Add(gen.GenerateFor(r));
            return result;
        }

        // ADMINISTRATOR
        public static List<ReviewFile> CompileReviews<TReview>(
            RecipientContainer<Administrator> container)
            where TReview : IReviewCompiler<Administrator>, new()
        {
            var gen = new TReview();
            var result = new List<ReviewFile>(container.Count);
            foreach (var r in container)
                result.Add(gen.GenerateFor(r));
            return result;
        }
    }
}
