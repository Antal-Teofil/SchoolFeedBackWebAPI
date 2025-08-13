using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace AzureEndPointReaction.Functions.QuestionnaireInterfaces
{
    public interface IQuestionnaireWorker
    {
        Task<HttpResponseData> ExecuteTaskAsync(HttpRequestData request, FunctionContext context, CancellationToken token);
    }
}
