using FeedBackApp.Core.QuestionnaireUtils.ReviewUtils.ReviewContracts;

namespace FeedBackApp.Core.QuestionnaireUtils.ReviewUtils.ReviewUtilityPOCOs
{
    public sealed record Administrator(string Email, string FirstName, string LastName) : IRecipientType;
}
