using FeedBackApp.Core.QuestionnaireUtils.ReviewUtils.ReviewContracts;

namespace FeedBackApp.Core.QuestionnaireUtils.ReviewUtils.ReviewUtilityPOCOs
{
    public sealed record Teacher(string Email, string FirstName, string LastName, string Subject) : IRecipientType;
}
