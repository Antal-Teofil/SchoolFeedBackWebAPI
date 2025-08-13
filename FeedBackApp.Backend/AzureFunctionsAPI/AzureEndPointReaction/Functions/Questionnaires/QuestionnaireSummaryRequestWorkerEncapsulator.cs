using AzureFunctionsAPI.AzureEndPointReaction.Functions.QuestionnaireInterfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;

namespace AzureEndPointReaction.Functions.Questionnaires
{
    public sealed class QuestionnaireSummaryRequestWorkerEncapsulator(IQuestionnaireService service, ILogger<QuestionnaireSummaryRequestWorkerEncapsulator> logger)
    {
        private readonly IQuestionnaireService _service = service;
        private readonly ILogger<QuestionnaireSummaryRequestWorkerEncapsulator> _logger = logger;

        [Function("PerformQuestionnarieStatistics")]
        [OpenApiOperation(
            operationId: "PerformQuestionnaireStatistics",
            tags: new[] { "Questionnaires" }
        )]
        [OpenApiParameter(
            name: "id",
            In = ParameterLocation.Path,
            Required = true,
            Type = typeof(Guid)
        )]
        [OpenApiResponseWithBody(
            statusCode: HttpStatusCode.OK,
            contentType: "application/json",
            bodyType: typeof(object) // replace with `summary` DTO
        )]
        public async Task<HttpResponseData> ExecuteTaskAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "questionnaires/{id:guid}")] HttpRequestData request, FunctionContext context, CancellationToken token)
        {
            /*implementation in progress*/
            var response = request.CreateResponse(HttpStatusCode.OK);
            return response;
        }
    }
}
