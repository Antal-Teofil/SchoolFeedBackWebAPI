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

    [TestCase("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiQWRtaW4iLCJuYmYiOjE3NTU0OTQwMDgsImV4cCI6MTc1NTQ5NzYwOCwiaWF0IjoxNzU1NDk0MDA4LCJpc3MiOiJTY2hvb2xGZWVkYmFja1dlYkFQSSIsImF1ZCI6IlNjaG9vbEZlZWRiYWNrV2ViQVBJIn0.DjCUcWLMPbptopXaLAQzjgSI7cJBAbsn43DESKZTaUM")]
    [TestCase("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiQWRtaW4iLCJuYmYiOjE3NTU0OTQwMzksImV4cCI6MTc1NTQ5NzYzOSwiaWF0IjoxNzU1NDk0MDM5LCJpc3MiOiJTY2hvb2xGZWVkYmFja1dlYkFQSSIsImF1ZCI6IlNjaG9vbEZlZWRiYWNrV2ViQVBJIn0.qOy7VRoYcfZMvsEcsK4KYb3013HvpPjc5SIjEi6tl1g")]
    [TestCase("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiQWRtaW4iLCJuYmYiOjE3NTU0OTQyMTMsImV4cCI6MTc1NTQ5NzgxMywiaWF0IjoxNzU1NDk0MjEzLCJpc3MiOiJTY2hvb2xGZWVkYmFja1dlYkFQSSIsImF1ZCI6IlNjaG9vbEZlZWRiYWNrV2ViQVBJIn0.Po8SFloX25gE3UDOBNgukv6hWCy8L4AhZg2HNPywBmE")]
    public void IsAdmin_WithAdminToken_ReturnsTrue(string token)
    {
        JwtRoleValidator.IsAdmin(token).Should().BeTrue();
    }

    [TestCase("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiQWRtaW4iLCJuYmYiOjE3NTU0OTQwMDgsImV4cCI6MTc1NTQ5NzYwOCwiaWF0IjoxNzU1NDk0MDA4LCJpc3MiOiJTY2hvb2xGZWVkYmFja1dlYkFQSSIsImF1ZCI6IlNjaG9vbEZlZWRiYWNrV2ViQVBJIn0.DjCUcWLMPbptopXaLAQzjgSI7cJBAbsn43DESKZTaUM")]
    [TestCase("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiQWRtaW4iLCJuYmYiOjE3NTU0OTQwMzksImV4cCI6MTc1NTQ5NzYzOSwiaWF0IjoxNzU1NDk0MDM5LCJpc3MiOiJTY2hvb2xGZWVkYmFja1dlYkFQSSIsImF1ZCI6IlNjaG9vbEZlZWRiYWNrV2ViQVBJIn0.qOy7VRoYcfZMvsEcsK4KYb3013HvpPjc5SIjEi6tl1g")]
    [TestCase("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiQWRtaW4iLCJuYmYiOjE3NTU0OTQyMTMsImV4cCI6MTc1NTQ5NzgxMywiaWF0IjoxNzU1NDk0MjEzLCJpc3MiOiJTY2hvb2xGZWVkYmFja1dlYkFQSSIsImF1ZCI6IlNjaG9vbEZlZWRiYWNrV2ViQVBJIn0.Po8SFloX25gE3UDOBNgukv6hWCy8L4AhZg2HNPywBmE")]
    public void IsStudent_WithAdminToken_ReturnsFalse(string token)
    {
        JwtRoleValidator.IsStudent(token).Should().BeFalse();
    }

    [TestCase("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiU3R1ZGVudCIsIm5iZiI6MTc1NTQ5NDA4OSwiZXhwIjoxNzU1NDk3Njg5LCJpYXQiOjE3NTU0OTQwODksImlzcyI6IlNjaG9vbEZlZWRiYWNrV2ViQVBJIiwiYXVkIjoiU2Nob29sRmVlZGJhY2tXZWJBUEkifQ.Kog3T5YuVzdp98DIhj7GtzjqlpKjH26QCimVoRUKcEs")]
    [TestCase("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiU3R1ZGVudCIsIm5iZiI6MTc1NTQ5NDEyMCwiZXhwIjoxNzU1NDk3NzIwLCJpYXQiOjE3NTU0OTQxMjAsImlzcyI6IlNjaG9vbEZlZWRiYWNrV2ViQVBJIiwiYXVkIjoiU2Nob29sRmVlZGJhY2tXZWJBUEkifQ.50m-cq83ZR-_UCwrscS_408C8NUbkibSJlTiQpwpzrA")]
    [TestCase("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiU3R1ZGVudCIsIm5iZiI6MTc1NTQ5NDI0MiwiZXhwIjoxNzU1NDk3ODQyLCJpYXQiOjE3NTU0OTQyNDIsImlzcyI6IlNjaG9vbEZlZWRiYWNrV2ViQVBJIiwiYXVkIjoiU2Nob29sRmVlZGJhY2tXZWJBUEkifQ.Y9jQmr1-TVGu_NcIXDToSUiXP6eZbsYpfGgBhMeY8zc")]
    public void IsStudent_WithStudentToken_ReturnsTrue(string token)
    {
        JwtRoleValidator.IsStudent(token).Should().BeTrue();
    }

    [TestCase("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiU3R1ZGVudCIsIm5iZiI6MTc1NTQ5NDA4OSwiZXhwIjoxNzU1NDk3Njg5LCJpYXQiOjE3NTU0OTQwODksImlzcyI6IlNjaG9vbEZlZWRiYWNrV2ViQVBJIiwiYXVkIjoiU2Nob29sRmVlZGJhY2tXZWJBUEkifQ.Kog3T5YuVzdp98DIhj7GtzjqlpKjH26QCimVoRUKcEs")]
    [TestCase("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiU3R1ZGVudCIsIm5iZiI6MTc1NTQ5NDEyMCwiZXhwIjoxNzU1NDk3NzIwLCJpYXQiOjE3NTU0OTQxMjAsImlzcyI6IlNjaG9vbEZlZWRiYWNrV2ViQVBJIiwiYXVkIjoiU2Nob29sRmVlZGJhY2tXZWJBUEkifQ.50m-cq83ZR-_UCwrscS_408C8NUbkibSJlTiQpwpzrA")]
    [TestCase("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiU3R1ZGVudCIsIm5iZiI6MTc1NTQ5NDI0MiwiZXhwIjoxNzU1NDk3ODQyLCJpYXQiOjE3NTU0OTQyNDIsImlzcyI6IlNjaG9vbEZlZWRiYWNrV2ViQVBJIiwiYXVkIjoiU2Nob29sRmVlZGJhY2tXZWJBUEkifQ.Y9jQmr1-TVGu_NcIXDToSUiXP6eZbsYpfGgBhMeY8zc")]
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
