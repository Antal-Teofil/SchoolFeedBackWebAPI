using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FeedBackApp.Tests
{
    public static class TestTokenGenerator
    {
        public static string GenerateTestToken(string role, DateTime? expires = null)
        {
            var secretKey = Environment.GetEnvironmentVariable("JwtSecretKey");
            if (string.IsNullOrEmpty(secretKey))
                throw new InvalidOperationException("JWT secret key not set in environment variables.");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var notBefore = DateTime.UtcNow;
            var expiration = expires ?? notBefore.AddHours(1);
            if(expires != null)
            {
                notBefore = expires.Value.AddHours(-1);
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, role) }),
                NotBefore = notBefore,
                Expires = expiration,
                Issuer = "SchoolFeedbackWebAPI",
                Audience = "SchoolFeedbackWebAPI",
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
