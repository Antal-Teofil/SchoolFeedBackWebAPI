using System.Text.Json;
using Azure.Core.Serialization;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;

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
        // services.AddScoped<IQuestionnaireService, QuestionnaireService>();
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
