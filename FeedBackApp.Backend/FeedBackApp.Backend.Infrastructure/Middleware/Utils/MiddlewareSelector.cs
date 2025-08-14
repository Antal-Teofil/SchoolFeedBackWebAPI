using FeedBackApp.Backend.Infrastructure.Middleware;
using FeedBackApp.Backend.Infrastructure.Middleware.Utils;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using System.Reflection;

namespace FeedBackApp.Backend.Infrastructure.Middleware.Utils
public class MiddlewareSelector : IFunctionsWorkerMiddleware
{
    private readonly AdminOnlyMiddleware _adminOnly;
    private readonly StudentOnlyMiddleware _studentOnly;

    public MiddlewareSelector(AdminOnlyMiddleware adminOnly, StudentOnlyMiddleware studentOnly)
    {
        _adminOnly = adminOnly;
        _studentOnly = studentOnly;
    }

    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        var entryPoint = context.FunctionDefinition.EntryPoint;
        var lastDot = entryPoint.LastIndexOf('.');
        var className = entryPoint[..lastDot];
        var methodName = entryPoint[(lastDot + 1)..];

        var type = Type.GetType(className);
        var method = type?.GetMethod(methodName);

        if (method != null)
        {
            if (method.GetCustomAttribute<RequireAdminAttribute>() != null)
            {
                await _adminOnly.Invoke(context, next);
                return;
            }

            if (method.GetCustomAttribute<RequireStudentAttribute>() != null)
            {
                await _studentOnly.Invoke(context, next);
                return;
            }
        }

        // No role attribute — just continue
        await next(context);
    }
}
