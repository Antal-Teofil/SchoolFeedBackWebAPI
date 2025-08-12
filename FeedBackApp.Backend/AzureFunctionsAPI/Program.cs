using FeedBackApp.Backend.Infrastructure.Persistence;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDBContext>(options =>
{
    var configuration = builder.Configuration;

    options.UseCosmos(
        accountEndpoint:configuration["AccountEndpoint"],
        accountKey: configuration["AccountKey"],
        databaseName: configuration["DatabaseName"]);
});

// mapper transients validators

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

builder.Build().Run();