using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace Application.Services.Interfaces
{
    public interface IQuestionnaireWorker
    {
        Task<HttpResponseData> ExecuteTaskAsync(HttpRequestData request, FunctionContext context);
    }
}
