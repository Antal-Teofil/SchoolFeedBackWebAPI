using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace SchoolFeedbackWebApiFunctions;

public class Endpoints
{
    private readonly ILogger<Endpoints> _logger;

    public Endpoints(ILogger<Endpoints> logger)
    {
        _logger = logger;
    }

    [Function("GetSubjectsByGrade")]
    public Task<HttpResponseData> GetSubjectsByGrade(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "grades/{gradeId}/subjects")] HttpRequestData req,
        string gradeId)
    {
        throw (new NotImplementedException());
    }

    [Function("GetTeachersBySubject")]
    public Task<HttpResponseData> GetTeachersBySubject(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "subjects/{subjectId}/teachers")] HttpRequestData req,
        string subjectId)
    {
        throw (new NotImplementedException());
    }

    [Function("SaveReview")]
    public Task<HttpResponseData> SaveReview(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "reviews")] HttpRequestData req)
    {
        throw (new NotImplementedException());
    }

    [Function("GetSubjectsByTeacher")]
    public Task<HttpResponseData> GetSubjectsByTeacher(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "teachers/{teacherId}/subjects")] HttpRequestData req,
        string teacherId)
    {
        throw (new NotImplementedException());
    }

    [Function("GetReviewsBySubject")]
    public Task<HttpResponseData> GetReviewsBySubject(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "subjects/{subjectId}/reviews")] HttpRequestData req,
        string subjectId)
    {
        throw (new NotImplementedException());
    }

    [Function("GetAllReviews")]
    public Task<HttpResponseData> GetAllReviews(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "reviews")] HttpRequestData req)
    {
        throw (new NotImplementedException());
    }

    [Function("GetReviewStats")]
    public Task<HttpResponseData> GetReviewStats(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "reviews/stats")] HttpRequestData req)
    {
        throw (new NotImplementedException());
    }

}