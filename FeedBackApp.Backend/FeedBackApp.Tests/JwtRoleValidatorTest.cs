using FeedBackApp.Backend.Infrastructure.Middleware.Utils;
using FluentAssertions;

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
        JwtRoleValidator.IsAdmin(token).Should().BeTrue();
        JwtRoleValidator.IsStudent(token).Should().BeFalse();
    }
    
    [Fact]
    public void IsStudent_WithStudentRole_ReturnsTrue()
    {
        var token = TestTokenGenerator.GenerateTestToken("Student");
        JwtRoleValidator.IsAdmin(token).Should().BeFalse();
        JwtRoleValidator.IsStudent(token).Should().BeTrue();
    }

    [Fact]
    public void HasRole_WithInvalidToken_ReturnsFalse() 
    {
        JwtRoleValidator.HasRole("invalid-token", "Admin").Should().BeFalse();
        JwtRoleValidator.HasRole("invalid-token", "Student").Should().BeFalse();
    }

    [Fact]
    public void HasRole_WithExpiredToken_ReturnsFalse()
    {
        var token = TestTokenGenerator.GenerateTestToken("admin",DateTime.UtcNow.AddMinutes(-10));
        JwtRoleValidator.HasRole(token, "Admin").Should().BeFalse();
        JwtRoleValidator.HasRole(token, "Student").Should().BeFalse();
    }

}
