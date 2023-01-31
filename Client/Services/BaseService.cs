using Client.Models;

namespace Client.Services;

public class BaseService : IBaseService
{
    public IHttpClientFactory httpClient { get; set; }
    public BaseService(IHttpClientFactory httpClientFactory)
    {
        responseModel = new();
        httpClient = httpClientFactory;
    }

    public APIResponse responseModel { get; set; }
    public Task<T> SendAsync<T>(APIRequest apiRequest)
    {
        throw new NotImplementedException();
    }
}
