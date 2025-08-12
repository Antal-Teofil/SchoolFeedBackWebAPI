

using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;
using System.Net;

namespace FeedBackApp.Backend.Infrastructure.Middleware
{
    public class AdminOnlyMiddleware : IFunctionsWorkerMiddleware
    {
        public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
        {
            var functionName = context.FunctionDefinition.Name;

            var key = Environment.GetEnvironmentVariable("ADMIN_KEY");
            if (string.IsNullOrEmpty(key) || !IsAdmin(key))
            {
                var req = await context.GetHttpRequestDataAsync();
                var res = req.CreateResponse(HttpStatusCode.Forbidden);
                res.WriteString("ADMIN ONLY!");

                context.GetInvocationResult().Value = res;
                return;
            }
            await next(context);
        }

        //private bool IsAdmin(string key)
        //{

        //}
    }
}
