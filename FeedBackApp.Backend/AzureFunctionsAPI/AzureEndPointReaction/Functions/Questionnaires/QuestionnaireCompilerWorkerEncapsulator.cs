﻿using System.Net;
using Application.DTOs.QuestionnaireDTOs;
using Application.Services.Interfaces;
using AzureFunctionsAPI.AzureEndPointReaction.Functions.Utils;
using FeedBackApp.Backend.Infrastructure.Middleware.Utils;
using Microsoft.AspNetCore.Http;
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
            bodyType: typeof(CreationResponseDTO) // replace dto
            )]
        public async Task<HttpResponseData> ExecuteTaskAsync([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "questionnaires")] HttpRequestData request, FunctionContext context, CancellationToken token)
        {
            try
            {
                var dto = await JsonUtil.ReadFromJsonAsync<CreateSurveyMetadataDto>(request);

                if (dto == null)
                {
                    var badResponse = request.CreateResponse(HttpStatusCode.BadRequest);
                    await badResponse.WriteStringAsync("Invalid or empty JSON body.");
                    return badResponse;
                }

                var result = await _service.CompileAndSaveAsync(dto);

                var response = request.CreateResponse(HttpStatusCode.OK);
                await response.WriteAsJsonAsync(result);
                return response;

            }
            catch (Exception e)
            {
                _logger.LogError("Something unexpected happenned!");
                var response = request.CreateResponse(HttpStatusCode.InternalServerError);
                await response.WriteAsJsonAsync(new { error = e.Message });
                return response;
            }

        }
    }
}
