using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AzureEndPointReaction.Functions.Questionnaires
{
    public sealed class QuestionnaireEvaluationWorkerEncapsulator(IServiceProvider service, ILogger<QuestionnaireEvaluationWorkerEncapsulator> logger)
    {
        private readonly IServiceProvider _service = service;
        private readonly ILogger<QuestionnaireEvaluationWorkerEncapsulator> _logger = logger;

        [Function("PerformQuestionnaireEvaluation")]
        public async Task<HttpResponseData> ExecuteTaskAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "evaluations/{id:guid}")] HttpRequestData request, FunctionContext context, CancellationToken token)
        {
            /*implementation in progress*/
            var response = request.CreateResponse(HttpStatusCode.OK);
            return response;
        }
    }
}
