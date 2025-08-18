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

    [TestCase("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiQWRtaW4iLCJuYmYiOjE3NTU1MTU1NTEsImV4cCI6MjUzNDAyMjkzNjAwLCJpYXQiOjE3NTU1MTU1NTIsImlzcyI6IlNjaG9vbEZlZWRiYWNrV2ViQVBJIiwiYXVkIjoiU2Nob29sRmVlZGJhY2tXZWJBUEkifQ.-wVwUOHOsGRk_tfgq-gR-NRblvPWSVOPorHsVxMsChE")]
    [TestCase("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiQWRtaW4iLCJuYmYiOjE3NTU1MTU1OTQsImV4cCI6MjUzNDAyMjkzNjAwLCJpYXQiOjE3NTU1MTU1OTQsImlzcyI6IlNjaG9vbEZlZWRiYWNrV2ViQVBJIiwiYXVkIjoiU2Nob29sRmVlZGJhY2tXZWJBUEkifQ.2u5k-3e5ECOlPHOfCYsUETKAW5d1j54wklpGFa483jc")]
    [TestCase("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiQWRtaW4iLCJuYmYiOjE3NTU1MTU2MTMsImV4cCI6MjUzNDAyMjkzNjAwLCJpYXQiOjE3NTU1MTU2MTMsImlzcyI6IlNjaG9vbEZlZWRiYWNrV2ViQVBJIiwiYXVkIjoiU2Nob29sRmVlZGJhY2tXZWJBUEkifQ.VZjHsmuG9ELeLI3oz0FfFphvo7XRiLzSkWUne71fXf4")]
    public void IsAdmin_WithAdminToken_ReturnsTrue(string token)
    {
        JwtRoleValidator.IsAdmin(token).Should().BeTrue();
    }

    [TestCase("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiQWRtaW4iLCJuYmYiOjE3NTU1MTU1NTEsImV4cCI6MjUzNDAyMjkzNjAwLCJpYXQiOjE3NTU1MTU1NTIsImlzcyI6IlNjaG9vbEZlZWRiYWNrV2ViQVBJIiwiYXVkIjoiU2Nob29sRmVlZGJhY2tXZWJBUEkifQ.-wVwUOHOsGRk_tfgq-gR-NRblvPWSVOPorHsVxMsChE")]
    [TestCase("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiQWRtaW4iLCJuYmYiOjE3NTU1MTU1OTQsImV4cCI6MjUzNDAyMjkzNjAwLCJpYXQiOjE3NTU1MTU1OTQsImlzcyI6IlNjaG9vbEZlZWRiYWNrV2ViQVBJIiwiYXVkIjoiU2Nob29sRmVlZGJhY2tXZWJBUEkifQ.2u5k-3e5ECOlPHOfCYsUETKAW5d1j54wklpGFa483jc")]
    [TestCase("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiQWRtaW4iLCJuYmYiOjE3NTU1MTU2MTMsImV4cCI6MjUzNDAyMjkzNjAwLCJpYXQiOjE3NTU1MTU2MTMsImlzcyI6IlNjaG9vbEZlZWRiYWNrV2ViQVBJIiwiYXVkIjoiU2Nob29sRmVlZGJhY2tXZWJBUEkifQ.VZjHsmuG9ELeLI3oz0FfFphvo7XRiLzSkWUne71fXf4")]
    public void IsStudent_WithAdminToken_ReturnsFalse(string token)
    {
        JwtRoleValidator.IsStudent(token).Should().BeFalse();
    }

    [TestCase("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiU3R1ZGVudCIsIm5iZiI6MTc1NTUxNTY2MCwiZXhwIjoyNTM0MDIyOTM2MDAsImlhdCI6MTc1NTUxNTY2MCwiaXNzIjoiU2Nob29sRmVlZGJhY2tXZWJBUEkiLCJhdWQiOiJTY2hvb2xGZWVkYmFja1dlYkFQSSJ9.6R2EPGihlR0zqAyb3TuFxM85FSPMehX5tfZOtpEKrfo")]
    [TestCase("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiU3R1ZGVudCIsIm5iZiI6MTc1NTUxNTY3NCwiZXhwIjoyNTM0MDIyOTM2MDAsImlhdCI6MTc1NTUxNTY3NCwiaXNzIjoiU2Nob29sRmVlZGJhY2tXZWJBUEkiLCJhdWQiOiJTY2hvb2xGZWVkYmFja1dlYkFQSSJ9.Uim6tpfGlhJP9k9X0xmp4EyVeABGh8wISsu6u4S3LR0")]
    [TestCase("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiU3R1ZGVudCIsIm5iZiI6MTc1NTUxNTY4NywiZXhwIjoyNTM0MDIyOTM2MDAsImlhdCI6MTc1NTUxNTY4NywiaXNzIjoiU2Nob29sRmVlZGJhY2tXZWJBUEkiLCJhdWQiOiJTY2hvb2xGZWVkYmFja1dlYkFQSSJ9.vd84IuZ8fAHzNrywDsU-G6VC9vgvR80AA205cFzgNc4")]
    public void IsStudent_WithStudentToken_ReturnsTrue(string token)
    {
        JwtRoleValidator.IsStudent(token).Should().BeTrue();
    }

    [TestCase("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiU3R1ZGVudCIsIm5iZiI6MTc1NTUxNTY2MCwiZXhwIjoyNTM0MDIyOTM2MDAsImlhdCI6MTc1NTUxNTY2MCwiaXNzIjoiU2Nob29sRmVlZGJhY2tXZWJBUEkiLCJhdWQiOiJTY2hvb2xGZWVkYmFja1dlYkFQSSJ9.6R2EPGihlR0zqAyb3TuFxM85FSPMehX5tfZOtpEKrfo")]
    [TestCase("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiU3R1ZGVudCIsIm5iZiI6MTc1NTUxNTY3NCwiZXhwIjoyNTM0MDIyOTM2MDAsImlhdCI6MTc1NTUxNTY3NCwiaXNzIjoiU2Nob29sRmVlZGJhY2tXZWJBUEkiLCJhdWQiOiJTY2hvb2xGZWVkYmFja1dlYkFQSSJ9.Uim6tpfGlhJP9k9X0xmp4EyVeABGh8wISsu6u4S3LR0")]
    [TestCase("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiU3R1ZGVudCIsIm5iZiI6MTc1NTUxNTY4NywiZXhwIjoyNTM0MDIyOTM2MDAsImlhdCI6MTc1NTUxNTY4NywiaXNzIjoiU2Nob29sRmVlZGJhY2tXZWJBUEkiLCJhdWQiOiJTY2hvb2xGZWVkYmFja1dlYkFQSSJ9.vd84IuZ8fAHzNrywDsU-G6VC9vgvR80AA205cFzgNc4")]
    public void IsAdmin_WithStudentToken_ReturnsFalse(string token)
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
