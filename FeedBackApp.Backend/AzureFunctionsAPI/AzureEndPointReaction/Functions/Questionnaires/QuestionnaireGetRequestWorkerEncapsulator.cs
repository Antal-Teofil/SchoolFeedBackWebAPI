using Application.Services.Interfaces;
using FeedBackApp.Backend.Infrastructure.Middleware.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace AzureFunctionsAPI.AzureEndPointReaction.Functions.Questionnaires;

public class QuestionnaireGetRequestWorkerEncapsulator :IQuestionnaireWorker
{
    private readonly ILogger<QuestionnaireGetRequestWorkerEncapsulator> _logger;

    public QuestionnaireGetRequestWorkerEncapsulator(ILogger<QuestionnaireGetRequestWorkerEncapsulator> logger)
    {
        _logger = logger;
    }

    [RequireStudent]
    [Function("GetQuestionnaires")]
    public async Task<HttpResponseData> ExecuteTaskAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "questionnaires")] HttpRequestData request, FunctionContext context)
    {
        throw new NotImplementedException();
    }
}