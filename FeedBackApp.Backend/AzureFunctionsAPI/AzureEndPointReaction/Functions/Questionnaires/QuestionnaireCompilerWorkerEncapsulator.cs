using System.Net;
using AzureEndPointReaction.Functions.QuestionnaireInterfaces;
using AzureFunctionsAPI.AzureEndPointReaction.Functions.QuestionnaireInterfaces;
using FeedBackApp.Backend.Infrastructure.Middleware.Utils;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;

namespace AzureEndPointReaction.Functions.Questionnaires
{
    public sealed class QuestionnaireCompilerWorkerEncapsulator(IQuestionnaireService service, ILogger<QuestionnaireCompilerWorkerEncapsulator> logger) : IQuestionnaireWorker
    {
        private readonly IQuestionnaireService _service = service;
        private readonly ILogger<QuestionnaireCompilerWorkerEncapsulator> _logger = logger;

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
            var body = await new StreamReader(request.Body).ReadToEndAsync();

            if (string.IsNullOrWhiteSpace(body))
            {
                var emptyResponse = request.CreateResponse(HttpStatusCode.BadRequest);
                await emptyResponse.WriteAsJsonAsync(new { error = "Request body cannot be empty." }, cancellationToken: CancellationToken.None);
                return emptyResponse;
            }
            /*implementation in progress*/
            var response = request.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(new { message = "Post successful" });
            return response;

        }
    }
}
