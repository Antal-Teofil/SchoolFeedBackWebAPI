using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;

namespace FeedBackApp.Backend.Infrastructure.Middleware.Utils
{
    public static class ReturnForbidden
    {
        public static Task ExecuteAsync(FunctionContext context, HttpRequestData req)
        {
            var response = req.CreateResponse(HttpStatusCode.Forbidden);
            response.WriteString("Forbidden");
            context.GetInvocationResult().Value = response;
            return Task.CompletedTask;
        }
    }
}