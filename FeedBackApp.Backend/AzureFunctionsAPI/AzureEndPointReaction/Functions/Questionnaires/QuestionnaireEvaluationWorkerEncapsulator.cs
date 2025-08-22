using Application.Services.Interfaces;
using FeedBackApp.Backend.Infrastructure.Middleware.Utils;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;


namespace AzureEndPointReaction.Functions.Questionnaires
{
    public sealed class QuestionnaireEvaluationWorkerEncapsulator(IEvaluationService service, ILogger<QuestionnaireEvaluationWorkerEncapsulator> logger)
    {
        private readonly IEvaluationService _service = service;
        private readonly ILogger<QuestionnaireEvaluationWorkerEncapsulator> _logger = logger;

        [RequireAdmin]
        [Function("PerformQuestionnaireEvaluation")]
        [OpenApiOperation(
            operationId: "PerformQuestionnaireEvaluation",
            tags: new[] { "Evaluations" }
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
            bodyType: typeof(object) // replace with actual evaluation DTO
        )]
        public async Task<HttpResponseData> ExecuteTaskAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "evaluations/{id:guid}")] HttpRequestData request, Guid id)
        {
            /*implementation in progress*/
            var response = request.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(new { message = "Get successful" });
            return response;

        }
    }
}
