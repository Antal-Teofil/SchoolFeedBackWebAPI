﻿using Application.Services;
using Application.Services.Interfaces;
using Application.Validation;
using Azure.Core.Serialization;
using AzureEndPointReaction.Functions.Questionnaires;
using FeedBackApp.Backend.Infrastructure.Middleware;
using FeedBackApp.Backend.Infrastructure.Middleware.Utils;
using FeedBackApp.Backend.Infrastructure.Persistence;
using FeedBackApp.Backend.Infrastructure.Persistence.Repository;
using FeedBackApp.Core.Repositories;
using FluentValidation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

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

        services.AddDbContext<AppDBContext>(options =>
        {
            var connectionString = Environment.GetEnvironmentVariable("ConnectionString");
            options.UseCosmos(
                connectionString : connectionString,
                databaseName: "SchoolDatabase"
            );
        });

        // DI regisztrációid
        // services.AddScoped<IMyService, MyService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IEvaluationService, EvaluationService>();
        services.AddScoped<IQuestionnaireRepository, QuestionnaireRepository>();
        services.AddScoped<IQuestionnaireService, QuestionnaireService>();
        services.AddScoped<IQuestionnaireWorker, QuestionnaireCompilerWorkerEncapsulator>();
        services.AddScoped<IQuestionnaireWorker, QuestionnaireDeletionWorkerEncapsulator>();
        services.AddScoped<IQuestionnaireWorker, QuestionnaireEvaluationWorkerEncapsulator>();
        services.AddScoped<IQuestionnaireWorker, QuestionnaireSummaryRequestWorkerEncapsulator>();
        services.AddScoped<IQuestionnaireWorker, QuestionnaireUpdateRequestWorkerEncapsulator>();

        services.AddValidatorsFromAssemblyContaining<CreateSurveyMetadataValidator>();

        services.AddSingleton<AdminOnlyMiddleware>();
        services.AddSingleton<StudentOnlyMiddleware>();
        services.AddSingleton<MiddlewareSelector>();
    })
    // Pipeline/Middleware (IFunctionsWorkerApplicationBuilder overload)
    .ConfigureFunctionsWebApplication((IFunctionsWorkerApplicationBuilder app) =>
    {
        app.UseMiddleware<MiddlewareSelector>();
        // Globális middleware-k ide:
        // app.UseMiddleware<YourExceptionMiddleware>();
        // app.UseWhen(ctx => true, branch => { /* branch middleware-k */ });
    })
    .Build();

// add creation permission for cosmosDb
using (var scope = host.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDBContext>();
    await dbContext.Database.EnsureCreatedAsync();
}

await host.RunAsync();
