using Microsoft.Azure.Functions.Worker.Http;

namespace Application.Services.Interfaces
{
    public interface IQuestionnaireWorker
    {
        Task<HttpResponseData> ExecuteTaskAsync(HttpRequestData request, Guid id);
    }
}
