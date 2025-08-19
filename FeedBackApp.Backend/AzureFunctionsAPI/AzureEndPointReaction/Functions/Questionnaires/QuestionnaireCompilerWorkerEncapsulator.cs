using System.Net;
using Application.DTOs;
using Application.Services.Interfaces;
using FeedBackApp.Backend.Infrastructure.Middleware.Utils;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;

namespace AzureEndPointReaction.Functions.Questionnaires
{
    public sealed class QuestionnaireCompilerWorkerEncapsulator(IQuestionnaireService service, ILogger<QuestionnaireCompilerWorkerEncapsulator> logger, IEmailService emailService) : IQuestionnaireWorker
    {
        private readonly IQuestionnaireService _service = service;
        private readonly ILogger<QuestionnaireCompilerWorkerEncapsulator> _logger = logger;
        private readonly IEmailService _emailService = emailService;

        [RequireAdmin]
        [Function("PerformQuestionnaireCompilation")]
        [OpenApiOperation(
            operationId: "PerformQuestionnaireCompilation",
            tags: new[] { "Questionnaires" }
            )]
        [OpenApiRequestBody(
            contentType: "application/json", 
            bodyType: typeof(object), // replace with dto
            Required = true
            )]
        [OpenApiResponseWithBody(
            statusCode: HttpStatusCode.OK, 
            contentType: "application/json", 
            bodyType: typeof(object) // replace dto
            )]
        public async Task<HttpResponseData> ExecuteTaskAsync([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "questionnaires")] HttpRequestData request, FunctionContext context, CancellationToken token)
        {
            var response = request.CreateResponse(HttpStatusCode.OK);

            await response.WriteAsJsonAsync(new { message = "Metadata and questionnaires saved" });
            return response;

        }
    }
}
