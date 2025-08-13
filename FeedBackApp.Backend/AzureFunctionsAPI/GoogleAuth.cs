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
namespace AzureFunctionsAPI;

public class GoogleAuth
{
    private readonly ILogger<GoogleAuth> _logger;

    public GoogleAuth(ILogger<GoogleAuth> logger)
    {
        _logger = logger;
    }

    //List of students who can access the review page
    private String[] students = new String[] { "szrichard2004@gmail.com" };

    //Google OAuth id validation and JWT token providing
    [Function("LoginWithGoogle")]
    [OpenApiOperation(operationId: "LoginWithGoogle", tags: new[] { "Auth" })]
    [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(LoginRequest), Required = true, Description = "Google ID Token payload")]
    public async Task<HttpResponseData> LoginWithGoogle(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post",Route ="auth/google")] HttpRequestData req)
    {
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
            await badResp.WriteStringAsync("Invalid Google token");
            return badResp;
        }

        //Check if the admin is in the list of admins in the environment variable
        var adminEmailsEnv = Environment.GetEnvironmentVariable("AdminEmails") ?? "";
        var adminEmails = adminEmailsEnv.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        bool isAdmin = adminEmails.Contains(payload.Email, StringComparer.OrdinalIgnoreCase);

        //If not admin, and also not student
        if (!students.Contains(payload.Email, StringComparer.OrdinalIgnoreCase) && !isAdmin)
        {
            var notFoundResp = req.CreateResponse(System.Net.HttpStatusCode.Forbidden);
            await notFoundResp.WriteStringAsync("User not found");
            return notFoundResp;
        }

        var token = GenerateJwtToken(payload.Email,isAdmin);

        var response = req.CreateResponse(System.Net.HttpStatusCode.OK);

        // Set the cookie
        response.Headers.Add("Set-Cookie", 
            $"token={token}; HttpOnly; Secure; SameSite=Strict; Path=/; Max-Age=86400");

        await response.WriteAsJsonAsync(new
        {
            email = payload.Email,
            firstName = payload.GivenName,
            lastName = payload.FamilyName,
            role = isAdmin ? "Admin" : "Student"
        });

        return response;
    }


    //Generate a JWT token with the email and role of the person for future authorization
    private string GenerateJwtToken(string email,bool isAdmin)
    {
        string secretKey = Environment.GetEnvironmentVariable("JwtSecretKey");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, email),
            new Claim(ClaimTypes.Role, isAdmin?"Admin":"Student")
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

    //Scheme of a login request for easy deserialization
    public class LoginRequest
    {
        public string IdToken { get; set; }
    }
}