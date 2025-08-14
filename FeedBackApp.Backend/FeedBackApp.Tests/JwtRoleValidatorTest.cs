using FeedBackApp.Backend.Infrastructure.Middleware.Utils;

namespace FeedBackApp.Tests;

public class JwtRoleValidatorTest
{

    public JwtRoleValidatorTest() {
        Environment.SetEnvironmentVariable("JwtSecretKey", "this-is-a-very-long-test-secret-key-1234");
    }

    [Fact]
    public void IsAdmin_WithAdminRole_ReturnsTrue()
    {
        var token = TestTokenGenerator.GenerateTestToken("Admin");
        Assert.True(JwtRoleValidator.IsAdmin(token));
        Assert.False(JwtRoleValidator.IsStudent(token));
    }
    
    [Fact]
    public void IsStudent_WithStudentRole_ReturnsTrue()
    {
        var token = TestTokenGenerator.GenerateTestToken("Student");
        Assert.False(JwtRoleValidator.IsAdmin(token));
        Assert.True(JwtRoleValidator.IsStudent(token));
    }

    [Fact]
    public void HasRole_WithInvalidToken_ReturnsFalse() 
    {
        Assert.False(JwtRoleValidator.HasRole("invalid-token", "Admin"));
        Assert.False(JwtRoleValidator.HasRole("invalid-token", "Student"));
    }

    [Fact]
    public void HasRole_WithExpiredToken_ReturnsFalse()
    {
        var token = TestTokenGenerator.GenerateTestToken("admin",DateTime.UtcNow.AddMinutes(-10));
        Assert.False(JwtRoleValidator.HasRole(token, "Admin"));
        Assert.False(JwtRoleValidator.HasRole(token, "Student"));
    }

}
