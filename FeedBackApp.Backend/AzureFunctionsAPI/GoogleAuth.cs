using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Google.Apis.Auth;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
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


    //Google OAuth id validation and JWT token providing
    [Function("LoginWithGoogle")]
    public async Task<HttpResponseData> LoginWithGoogle(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
    {
        var body = await new StreamReader(req.Body).ReadToEndAsync();
        var data = JsonConvert.DeserializeObject<LoginRequest>(body);
        GoogleJsonWebSignature.Payload payload;
        try
        {
            payload = await GoogleJsonWebSignature.ValidateAsync(data.IdToken, new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new[] { "499857664830-2bdarnf13t5g3j8t3e7pgv46it7tba1b.apps.googleusercontent.com" }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Invalid Google token");
            var badResp = req.CreateResponse(System.Net.HttpStatusCode.Unauthorized);
            await badResp.WriteStringAsync("Invalid Google token");
            return badResp;
        }

        var user = await GetUserFromDatabase(payload.Email);
        if (user == null)
        {
            var notFoundResp = req.CreateResponse(System.Net.HttpStatusCode.Forbidden);
            await notFoundResp.WriteStringAsync("User not found");
            return notFoundResp;
        }

        var token = GenerateJwtToken(user);

        var response = req.CreateResponse(System.Net.HttpStatusCode.OK);
        await response.WriteStringAsync(JsonConvert.SerializeObject(new { token }));
        return response;
    }

    //Placeholder while the database is not accessible
    private Task<UserRecord> GetUserFromDatabase(string email)
    {
        var users = new List<UserRecord>
        {
            new UserRecord { Email = "szrichard2004@gmail.com", Role = "Student" },
            new UserRecord { Email = "teacher@example.com", Role = "Teacher" }
        };

        return Task.FromResult(users.FirstOrDefault(u => u.Email == email));
    }

    //Generate a JWT token with the email and role of the person for future authorization
    private string GenerateJwtToken(UserRecord user)
    {
        string secretKey = Environment.GetEnvironmentVariable("JwtSecretKey");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var token = new JwtSecurityToken(
            issuer: "SchoolFeedbackWebAPI",
            audience: "SchoolFeedbackWebAPI",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    //Scheme of a login request
    public class LoginRequest
    {
        public string IdToken { get; set; }
    }

    public class UserRecord
    {
        public string Email { get; set; }
        public string Role { get; set; }
    }
}