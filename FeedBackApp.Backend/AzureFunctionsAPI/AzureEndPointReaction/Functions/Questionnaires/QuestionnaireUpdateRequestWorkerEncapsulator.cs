using AzureFunctionsAPI.AzureEndPointReaction.Functions.QuestionnaireInterfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace AzureEndPointReaction.Functions.Questionnaires
{
    public sealed class QuestionnaireUpdateRequestWorkerEncapsulator(IEvaluationService service, ILogger<QuestionnaireUpdateRequestWorkerEncapsulator> logger)
    {
        private readonly IEvaluationService _service = service;
        private readonly ILogger<QuestionnaireUpdateRequestWorkerEncapsulator> _logger = logger;
        [Function("PerformQuestionnaireUpdate")]
        public async Task<HttpResponseData> ExecuteTaskAsync([HttpTrigger(AuthorizationLevel.Anonymous, "patch", Route = "evaluations/{id:guid}")] HttpRequestData request, FunctionContext context, CancellationToken token)
        {
            /*implementation in progress*/
            var response = request.CreateResponse(HttpStatusCode.OK);
            return response;
        }
    }
}
