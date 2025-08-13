using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using FeedBackApp.Backend.Infrastructure.Middleware.Utils;

namespace FeedBackApp.Backend.Infrastructure.Middleware
{
    public class StudentOnlyMiddleware : IFunctionsWorkerMiddleware
    {
        public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
        {
            var httpRequestData = await context.GetHttpRequestDataAsync();

            if (httpRequestData == null)
            {
                await next(context);
                return;
            }

            if (!httpRequestData.Headers.TryGetValues("Authorization", out var authHeaders))
            {
                await ReturnForbidden.ExecuteAsync(context, httpRequestData);
                return;
            }

            var bearerToken = authHeaders.FirstOrDefault();
            if (string.IsNullOrWhiteSpace(bearerToken) || !bearerToken.StartsWith("Bearer "))
            {
                await ReturnForbidden.ExecuteAsync(context, httpRequestData);
                return;
            }

            var token = bearerToken.Substring("Bearer ".Length).Trim();

            if (!JwtRoleValidator.IsStudent(token))
            {
                await ReturnForbidden.ExecuteAsync(context, httpRequestData);
                return;
            }

            await next(context);
        }
    }
}