using FeedBackApp.Backend.Infrastructure.Middleware.Utils;
using FluentAssertions;
namespace Tests;

[TestFixture]
public class JwtRoleValidatorTest
{

    [SetUp]
    public void Setup()
    {
        Environment.SetEnvironmentVariable("JwtSecretKey", "this-is-a-very-long-test-secret-key-1234");
    }

    [TestCase("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiQWRtaW4iLCJuYmYiOjE3NTU0OTcxMTksImV4cCI6MTc1NTUwMDcyMCwiaWF0IjoxNzU1NDk3MTIwLCJpc3MiOiJTY2hvb2xGZWVkYmFja1dlYkFQSSIsImF1ZCI6IlNjaG9vbEZlZWRiYWNrV2ViQVBJIn0.NZdGpSQ0_FPk9bytaXi-tS6sIWouDhm1aWunmNiSBUA")]
    [TestCase("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiQWRtaW4iLCJuYmYiOjE3NTU0OTc0ODEsImV4cCI6MTc1NTUwMTA4MSwiaWF0IjoxNzU1NDk3NDgxLCJpc3MiOiJTY2hvb2xGZWVkYmFja1dlYkFQSSIsImF1ZCI6IlNjaG9vbEZlZWRiYWNrV2ViQVBJIn0.0DEm46xdtjZLK_7eJM92wkURYPNOL-c9C9_FgCsuIHg")]
    [TestCase("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiQWRtaW4iLCJuYmYiOjE3NTU0OTc0OTgsImV4cCI6MTc1NTUwMTA5OCwiaWF0IjoxNzU1NDk3NDk4LCJpc3MiOiJTY2hvb2xGZWVkYmFja1dlYkFQSSIsImF1ZCI6IlNjaG9vbEZlZWRiYWNrV2ViQVBJIn0.pm3Dc_8M7-baCc9nw8fqI7KyQOPC93PSnf3YI49GCBc")]
    public void IsAdmin_WithAdminToken_ReturnsTrue(string token)
    {
        JwtRoleValidator.IsAdmin(token).Should().BeTrue();
    }

    [TestCase("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiQWRtaW4iLCJuYmYiOjE3NTU0OTcxMTksImV4cCI6MTc1NTUwMDcyMCwiaWF0IjoxNzU1NDk3MTIwLCJpc3MiOiJTY2hvb2xGZWVkYmFja1dlYkFQSSIsImF1ZCI6IlNjaG9vbEZlZWRiYWNrV2ViQVBJIn0.NZdGpSQ0_FPk9bytaXi-tS6sIWouDhm1aWunmNiSBUA")]
    [TestCase("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiQWRtaW4iLCJuYmYiOjE3NTU0OTc0ODEsImV4cCI6MTc1NTUwMTA4MSwiaWF0IjoxNzU1NDk3NDgxLCJpc3MiOiJTY2hvb2xGZWVkYmFja1dlYkFQSSIsImF1ZCI6IlNjaG9vbEZlZWRiYWNrV2ViQVBJIn0.0DEm46xdtjZLK_7eJM92wkURYPNOL-c9C9_FgCsuIHg")]
    [TestCase("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiQWRtaW4iLCJuYmYiOjE3NTU0OTc0OTgsImV4cCI6MTc1NTUwMTA5OCwiaWF0IjoxNzU1NDk3NDk4LCJpc3MiOiJTY2hvb2xGZWVkYmFja1dlYkFQSSIsImF1ZCI6IlNjaG9vbEZlZWRiYWNrV2ViQVBJIn0.pm3Dc_8M7-baCc9nw8fqI7KyQOPC93PSnf3YI49GCBc")]
    public void IsStudent_WithAdminToken_ReturnsFalse(string token)
    {
        JwtRoleValidator.IsStudent(token).Should().BeFalse();
    }

    [TestCase("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiU3R1ZGVudCIsIm5iZiI6MTc1NTQ5NzQ5OCwiZXhwIjoxNzU1NTAxMDk4LCJpYXQiOjE3NTU0OTc0OTgsImlzcyI6IlNjaG9vbEZlZWRiYWNrV2ViQVBJIiwiYXVkIjoiU2Nob29sRmVlZGJhY2tXZWJBUEkifQ.OXbcNs-jOEK6YpDoDN8C2iwfa2BIaJxw21Y62w4YLUE")]
    [TestCase("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiU3R1ZGVudCIsIm5iZiI6MTc1NTQ5NzU1MSwiZXhwIjoxNzU1NTAxMTUxLCJpYXQiOjE3NTU0OTc1NTEsImlzcyI6IlNjaG9vbEZlZWRiYWNrV2ViQVBJIiwiYXVkIjoiU2Nob29sRmVlZGJhY2tXZWJBUEkifQ.jLLiv9fhtz5xn8kkGZh0qn7aBWnTyCKDpW6qT1E6EZo")]
    [TestCase("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiU3R1ZGVudCIsIm5iZiI6MTc1NTQ5NzU2OSwiZXhwIjoxNzU1NTAxMTY5LCJpYXQiOjE3NTU0OTc1NjksImlzcyI6IlNjaG9vbEZlZWRiYWNrV2ViQVBJIiwiYXVkIjoiU2Nob29sRmVlZGJhY2tXZWJBUEkifQ.8rAXWBa1Un_izH9KtivYHgO_Y0Ejoj6qDxsfq6ZCgLg")]
    public void IsStudent_WithStudentToken_ReturnsTrue(string token)
    {
        JwtRoleValidator.IsStudent(token).Should().BeTrue();
    }

    [TestCase("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiU3R1ZGVudCIsIm5iZiI6MTc1NTQ5NzQ5OCwiZXhwIjoxNzU1NTAxMDk4LCJpYXQiOjE3NTU0OTc0OTgsImlzcyI6IlNjaG9vbEZlZWRiYWNrV2ViQVBJIiwiYXVkIjoiU2Nob29sRmVlZGJhY2tXZWJBUEkifQ.OXbcNs-jOEK6YpDoDN8C2iwfa2BIaJxw21Y62w4YLUE")]
    [TestCase("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiU3R1ZGVudCIsIm5iZiI6MTc1NTQ5NzU1MSwiZXhwIjoxNzU1NTAxMTUxLCJpYXQiOjE3NTU0OTc1NTEsImlzcyI6IlNjaG9vbEZlZWRiYWNrV2ViQVBJIiwiYXVkIjoiU2Nob29sRmVlZGJhY2tXZWJBUEkifQ.jLLiv9fhtz5xn8kkGZh0qn7aBWnTyCKDpW6qT1E6EZo")]
    [TestCase("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiU3R1ZGVudCIsIm5iZiI6MTc1NTQ5NzU2OSwiZXhwIjoxNzU1NTAxMTY5LCJpYXQiOjE3NTU0OTc1NjksImlzcyI6IlNjaG9vbEZlZWRiYWNrV2ViQVBJIiwiYXVkIjoiU2Nob29sRmVlZGJhY2tXZWJBUEkifQ.8rAXWBa1Un_izH9KtivYHgO_Y0Ejoj6qDxsfq6ZCgLg")]
    public void IsAdmin_WithStudentToken_ReturnsTrue(string token)
    {
        JwtRoleValidator.IsAdmin(token).Should().BeFalse();
    }

    [TestCase("invalid-token")]
    [TestCase("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiU3R1ZGVudCIsIm5iZiI6MTc1NTQ5NDI0MiwiZXhwIjoxNzU1NDk3ODQyLCJpYXQiOjE3NTU0OTQyNDIsImlzcyI6IlNjaG9vbEZlZWRiYWNrV2ViQVBJIiwiYXVkIjosaiU2Nob29sRmVlZGJhY2tXZWJBUEkifQ.Y9jQmr1-TVGu_NcIXDToSUiXP6eZbsYpfGgBhMeY8zc")]
    [TestCase("")]
    public void HasRole_WithInvalidToken_ReturnsFalse(string token)
    {
        JwtRoleValidator.HasRole(token, "Admin").Should().BeFalse();
        JwtRoleValidator.HasRole(token, "Student").Should().BeFalse();
    }

    [TestCase("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiYWRtaW4iLCJuYmYiOjE3NTU0ODk5NTIsImV4cCI6MTc1NTQ5MzU1MiwiaWF0IjoxNzU1NDk0MTUyLCJpc3MiOiJTY2hvb2xGZWVkYmFja1dlYkFQSSIsImF1ZCI6IlNjaG9vbEZlZWRiYWNrV2ViQVBJIn0.MdZKhWBzapV3yA2ECeJLWxPgBXUxYL7YBG5LLClnZ5Y")]
    [TestCase("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiYWRtaW4iLCJuYmYiOjE3NTU0ODk5NzEsImV4cCI6MTc1NTQ5MzU3MSwiaWF0IjoxNzU1NDk0MTcxLCJpc3MiOiJTY2hvb2xGZWVkYmFja1dlYkFQSSIsImF1ZCI6IlNjaG9vbEZlZWRiYWNrV2ViQVBJIn0.rZ5UE-51VxJdEcwzikgktXtoTtCmEFDJH5YoI4eaUzI")]
    [TestCase("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiYWRtaW4iLCJuYmYiOjE3NTU0ODk5OTAsImV4cCI6MTc1NTQ5MzU5MCwiaWF0IjoxNzU1NDk0MTkwLCJpc3MiOiJTY2hvb2xGZWVkYmFja1dlYkFQSSIsImF1ZCI6IlNjaG9vbEZlZWRiYWNrV2ViQVBJIn0.5SnH7fb12SFWinLEOTIqy6BKyXMgYxAZKlUTrMDNBA0")]
    public void HasRole_WithExpiredToken_ReturnsFalse(string token)
    {
        JwtRoleValidator.HasRole(token, "Admin").Should().BeFalse();
        JwtRoleValidator.HasRole(token, "Student").Should().BeFalse();
    }

}
