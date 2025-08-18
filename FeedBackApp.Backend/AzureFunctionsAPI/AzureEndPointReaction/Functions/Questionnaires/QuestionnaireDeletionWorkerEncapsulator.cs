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
    public sealed class QuestionnaireDeletionWorkerEncapsulator(IQuestionnaireService service, ILogger<QuestionnaireDeletionWorkerEncapsulator> logger) : IQuestionnaireWorker
    {
        private readonly IQuestionnaireService _service = service;
        private readonly ILogger<QuestionnaireDeletionWorkerEncapsulator> _logger = logger;

        [RequireAdmin]
        [Function("PerformQuestionnarieDeletion")]
        [OpenApiOperation(
            operationId: "PerformQuestionnaireDeletion",
            tags: new[] { "Questionnaires" }
        )]
        [OpenApiParameter(
            name: "id",
            In = ParameterLocation.Path,
            Required = true,
            Type = typeof(Guid)
        )]
        public async Task<HttpResponseData> ExecuteTaskAsync([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "questionnaires/{id:guid}")] HttpRequestData request, FunctionContext context, CancellationToken token)
        {

            /*implementation in progress*/
            var response = request.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(new { message = "Delete successful" });
            return response;

        }
    }
}
