using AzureEndPointReaction.Functions.QuestionnaireInterfaces;
using AzureFunctionsAPI.AzureEndPointReaction.Functions.QuestionnaireInterfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;

namespace AzureEndPointReaction.Functions.Questionnaires
{
    public sealed class QuestionnaireUpdateRequestWorkerEncapsulator(IEvaluationService service, ILogger<QuestionnaireUpdateRequestWorkerEncapsulator> logger) : IQuestionnaireWorker
    {
        private readonly IEvaluationService _service = service;
        private readonly ILogger<QuestionnaireUpdateRequestWorkerEncapsulator> _logger = logger;
        [Function("PerformQuestionnaireUpdate")]
        [OpenApiOperation(
            operationId: "PerformQuestionnaireUpdate",
            tags: new[] { "Evaluations" }
        )]
        [OpenApiParameter(
            name: "id",
            In = ParameterLocation.Path,
            Required = true,
            Type = typeof(Guid)
        )]
        [OpenApiRequestBody(
            contentType: "application/json",
            bodyType: typeof(object), // replace with update DTO
            Required = true
        )]
        public async Task<HttpResponseData> ExecuteTaskAsync([HttpTrigger(AuthorizationLevel.Anonymous, "patch", Route = "evaluations/{id:guid}")] HttpRequestData request, FunctionContext context, CancellationToken token)
        {
            /*implementation in progress*/
            var response = request.CreateResponse(HttpStatusCode.OK);
            return response;
        }
    }
}
