using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Google.Apis.Auth;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace AzureFunctionsAPI
{
    public class GoogleAuth
    {
        private readonly ILogger<GoogleAuth> _logger;

        public GoogleAuth(ILogger<GoogleAuth> logger)
        {
            _logger = logger;
        }

        private string[] students = new string[] { "pistikejancsika741@gmail.com" };

        [Function("LoginWithGoogle")]
    [OpenApiOperation(operationId: "LoginWithGoogle", tags: new[] { "Auth" })]
    [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(LoginRequest), Required = true, Description = "Google ID Token payload")]
        public async Task<HttpResponseData> LoginWithGoogle(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", "options", Route = "auth/google")] HttpRequestData req)
        {
            // Get origin
            var origin = req.Headers.TryGetValues("Origin", out var origins) ? origins.FirstOrDefault() : null;

            // Handle preflight request
            if (req.Method.Equals("OPTIONS", StringComparison.OrdinalIgnoreCase))
            {
                var preflight = req.CreateResponse(System.Net.HttpStatusCode.NoContent);
                if (!string.IsNullOrEmpty(origin))
                {
                    preflight.Headers.Add("Access-Control-Allow-Origin", origin);
                    preflight.Headers.Add("Access-Control-Allow-Methods", "POST, OPTIONS");
                    preflight.Headers.Add("Access-Control-Allow-Headers", "Content-Type");
                    preflight.Headers.Add("Access-Control-Allow-Credentials", "true");
                }
                return preflight;
            }

            // Read POST body
            var body = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<LoginRequest>(body);
            GoogleJsonWebSignature.Payload payload;

            try
            {
                payload = await GoogleJsonWebSignature.ValidateAsync(data.IdToken, new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { Environment.GetEnvironmentVariable("GoogleClientId") }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Invalid Google token");
                var badResp = req.CreateResponse(System.Net.HttpStatusCode.Unauthorized);
                if (!string.IsNullOrEmpty(origin))
                {
                    badResp.Headers.Add("Access-Control-Allow-Origin", origin);
                    badResp.Headers.Add("Access-Control-Allow-Credentials", "true");
                }
                await badResp.WriteStringAsync("Invalid Google token");
                return badResp;
            }

            // Check if student or admin
            var adminEmailsEnv = Environment.GetEnvironmentVariable("AdminEmails") ?? "";
            var adminEmails = adminEmailsEnv.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            bool isAdmin = adminEmails.Contains(payload.Email, StringComparer.OrdinalIgnoreCase);

            if (!students.Contains(payload.Email, StringComparer.OrdinalIgnoreCase) && !isAdmin)
            {
                var notFoundResp = req.CreateResponse(System.Net.HttpStatusCode.Forbidden);
                if (!string.IsNullOrEmpty(origin))
                {
                    notFoundResp.Headers.Add("Access-Control-Allow-Origin", origin);
                    notFoundResp.Headers.Add("Access-Control-Allow-Credentials", "true");
                }
                await notFoundResp.WriteStringAsync("User not found");
                return notFoundResp;
            }

            var token = GenerateJwtToken(payload.Email, isAdmin);

            var response = req.CreateResponse(System.Net.HttpStatusCode.OK);

            // Set CORS headers for credentials
            if (!string.IsNullOrEmpty(origin))
            {
                response.Headers.Add("Access-Control-Allow-Origin", origin);
                response.Headers.Add("Access-Control-Allow-Credentials", "true");
            }

            // Set HttpOnly cookie
            response.Headers.Add("Set-Cookie",
                $"token={token}; HttpOnly; SameSite=None; Secure; Path=/; Max-Age=86400");

            await response.WriteAsJsonAsync(new
            {
                email = payload.Email,
                firstName = payload.GivenName,
                lastName = payload.FamilyName,
                role = isAdmin ? "Admin" : "Student"
            });

            return response;
        }

        private string GenerateJwtToken(string email, bool isAdmin)
        {
            string secretKey = Environment.GetEnvironmentVariable("JwtSecretKey");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, email),
                new Claim(ClaimTypes.Role, isAdmin ? "Admin" : "Student")
            };

            var token = new JwtSecurityToken(
                issuer: "SchoolFeedbackWebAPI",
                audience: "SchoolFeedbackWebAPI",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(24),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public class LoginRequest
        {
            public string IdToken { get; set; }
        }
    }
}