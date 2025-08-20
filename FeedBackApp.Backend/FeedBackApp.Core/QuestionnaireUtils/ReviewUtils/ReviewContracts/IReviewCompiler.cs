using FeedBackApp.Core.QuestionnaireUtils.ReviewUtils.ReviewUtilityPOCOs;

namespace FeedBackApp.Core.QuestionnaireUtils.ReviewUtils.ReviewContracts
{
    public interface IReviewCompiler<TRecipient> where TRecipient : IRecipientType
    {
        public ReviewFile GenerateFor(TRecipient recipient);
    }
}
