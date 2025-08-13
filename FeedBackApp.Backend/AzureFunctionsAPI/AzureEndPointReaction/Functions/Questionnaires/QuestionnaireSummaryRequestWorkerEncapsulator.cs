using AzureFunctionsAPI.AzureEndPointReaction.Functions.QuestionnaireInterfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace AzureEndPointReaction.Functions.Questionnaires
{
    public sealed class QuestionnaireSummaryRequestWorkerEncapsulator(IQuestionnaireService service, ILogger<QuestionnaireSummaryRequestWorkerEncapsulator> logger)
    {
        private readonly IQuestionnaireService _service = service;
        private readonly ILogger<QuestionnaireSummaryRequestWorkerEncapsulator> _logger = logger;

        [Function("PerformQuestionnarieStatistics")]
        public async Task<HttpResponseData> ExecuteTaskAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "questionnaires/{id:guid}")] HttpRequestData request, FunctionContext context, CancellationToken token)
        {
            /*implementation in progress*/
            var response = request.CreateResponse(HttpStatusCode.OK);
            return response;
        }
    }
}
