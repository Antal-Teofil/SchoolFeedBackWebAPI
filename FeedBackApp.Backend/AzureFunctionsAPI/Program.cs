using System.Text.Json;
using Azure.Core.Serialization;
using AzureEndPointReaction.Functions.QuestionnaireInterfaces;
using AzureEndPointReaction.Functions.Questionnaires;
using AzureFunctionsAPI.AzureEndPointReaction.Functions;
using AzureFunctionsAPI.AzureEndPointReaction.Functions.QuestionnaireInterfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


var host = new HostBuilder()
    .ConfigureAppConfiguration((ctx, cfg) =>
    {
        // ide az appsettings helyere a vegleges konfiguracios file kellene bekeruljon. Ez csak pelda!!!!
        cfg.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
           .AddJsonFile($"appsettings.{ctx.HostingEnvironment.EnvironmentName}.json", optional: true)
           .AddEnvironmentVariables();
    })
    .ConfigureServices((ctx, services) =>
    {

        services.AddApplicationInsightsTelemetryWorkerService();

        // WorkerOptions (serializer stb.)
        services.Configure<WorkerOptions>(o =>
        {
            // Példa: System.Text.Json testreszabás
            o.Serializer = new JsonObjectSerializer(
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    // Example: Ignore nulls
                    // DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                });
        });

        // DI regisztrációid
        // services.AddScoped<IMyService, MyService>();
        services.AddScoped<IQuestionnaireService, QuestionnaireService>();
        services.AddScoped<IQuestionnaireWorker, QuestionnaireCompilerWorkerEncapsulator>();
        services.AddScoped<IQuestionnaireWorker, QuestionnaireDeletionWorkerEncapsulator>();
        services.AddScoped<IQuestionnaireWorker, QuestionnaireEvaluationWorkerEncapsulator>();
        services.AddScoped<IQuestionnaireWorker, QuestionnaireSummaryRequestWorkerEncapsulator>();
        services.AddScoped<IQuestionnaireWorker, QuestionnaireUpdateRequestWorkerEncapsulator>();
    })
    // Pipeline/Middleware (IFunctionsWorkerApplicationBuilder overload)
    .ConfigureFunctionsWebApplication((IFunctionsWorkerApplicationBuilder app) =>
    {

        // Globális middleware-k ide:
        // app.UseMiddleware<YourExceptionMiddleware>();
        // app.UseWhen(ctx => true, branch => { /* branch middleware-k */ });
    })
    .Build();

await host.RunAsync();
