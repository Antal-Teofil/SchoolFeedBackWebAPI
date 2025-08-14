using FeedBackApp.Backend.Infrastructure.Persistence;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);


var AccountEndpoint = Environment.GetEnvironmentVariable("AccountEndpoint");
var AccountKey = Environment.GetEnvironmentVariable("AccountKey");
var DatabaseName = Environment.GetEnvironmentVariable("DatabaseName");
builder.Services.AddDbContextFactory<AppDBContext>(opt =>
{
    var configuration = builder.Configuration;
    opt.UseCosmos(
        accountEndpoint: configuration["AccountEndpoint"],
        accountKey: configuration["AccountKey"],
        databaseName: configuration["DatabaseName"]
        );
});

// mapper transients validators

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

builder.Build().Run();