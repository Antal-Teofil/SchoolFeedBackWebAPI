using AzureFunctionsAPI.AzureEndPointReaction.Functions.QuestionnaireInterfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace AzureEndPointReaction.Functions.Questionnaires
{
    public sealed class QuestionnaireDeletionWorkerEncapsulator(IQuestionnaireService service, ILogger<QuestionnaireDeletionWorkerEncapsulator> logger)
    {
        private readonly IQuestionnaireService _service = service;
        private readonly ILogger<QuestionnaireDeletionWorkerEncapsulator> _logger = logger;

        [Function("PerformQuestionnarieDeletion")]
        public async Task<HttpResponseData> ExecuteTaskAsync([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "questionnaires/{id:guid}")] HttpRequestData request, FunctionContext context, CancellationToken token)
        {

            /*implementation in progress*/
            var response = request.CreateResponse(HttpStatusCode.OK);
            return response;
        }
    }
}
