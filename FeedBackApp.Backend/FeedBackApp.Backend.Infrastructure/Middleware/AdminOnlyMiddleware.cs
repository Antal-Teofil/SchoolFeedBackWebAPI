using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace FeedBackApp.Backend.Infrastructure.Middleware
{
    public class AdminOnlyMiddleware : IFunctionsWorkerMiddleware
    {
        public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
        {
            var httpRequestData = await context.GetHttpRequestDataAsync();
            
            // non http
            if (httpRequestData == null) 
            {
                await next(context);
                return;
            }

            // no auth header
            if (!httpRequestData.Headers.TryGetValues("Authorization", out var authHeaders))
            {
                await ReturnForbiddenAsync(context, httpRequestData);
                return;
            }

            // no jwt bearer
            var bearerToken = authHeaders.FirstOrDefault();
            if (string.IsNullOrWhiteSpace(bearerToken) || !bearerToken.StartsWith("Bearer "))
            {
                await ReturnForbiddenAsync(context, httpRequestData);
                return;
            }

            var token = bearerToken.Substring("Bearer ".Length).Trim();

            // check admin role
            if (!IsAdmin(token))
            {
                await ReturnForbiddenAsync(context, httpRequestData);
                return;
            }

            await next(context);
        }

        private bool IsAdmin(string token)
        {
            var secretKey = Environment.GetEnvironmentVariable("JwtSecretKey");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = "SchoolFeedbackWebAPI",
                    ValidAudience = "SchoolFeedbackWebAPI",
                    IssuerSigningKey = key,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out _);

                var roleClaim = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
                return roleClaim?.Value == "Admin";
            }
            catch
            {
                return false;
            }
        }

        private static async Task ReturnForbiddenAsync(FunctionContext context, HttpRequestData req)
        {
            var response = req.CreateResponse(HttpStatusCode.Forbidden);
            await response.WriteStringAsync("Forbidden");
            context.GetInvocationResult().Value = response;
        }
    }
}
