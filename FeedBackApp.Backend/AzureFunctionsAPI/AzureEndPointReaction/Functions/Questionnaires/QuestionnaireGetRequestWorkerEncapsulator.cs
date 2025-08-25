using FeedBackApp.Backend.Infrastructure.Middleware.Utils
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace AzureFunctionsAPI.AzureEndPointReaction.Functions.Questionnaires;

public class QuestionnaireGetRequestWorkerEncapsulator
{
    private readonly ILogger<QuestionnaireGetRequestWorkerEncapsulator> _logger;

    public QuestionnaireGetRequestWorkerEncapsulator(ILogger<QuestionnaireGetRequestWorkerEncapsulator> logger)
    {
        _logger = logger;
    }

    [RequireStudent]
    [Function("GetQuestionnaires")]
    public async Task<HttpResponseData> ExecuteTaskAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "questionnaires")] HttpRequestData request)
    {
        var principal = request.FunctionContext.GetUser();

        if (principal == null)
        {
            var unauthorizedResponse = request.CreateResponse(HttpStatusCode.Unauthorized);
            return unauthorizedResponse;
        }

        // Extract the email claim
        var email = principal.Claims.FirstOrDefault(c => c.Type == "email")?.Value;

        if (string.IsNullOrEmpty(email))
        {
            var badResponse = request.CreateResponse(HttpStatusCode.BadRequest);
            await badResponse.WriteStringAsync("Email not found in token.");
            return badResponse;
        }

        _logger.LogInformation("Student email: {Email}", email);
    }
}