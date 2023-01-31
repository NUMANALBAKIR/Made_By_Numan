using Client.Models;

namespace Client.Services;

public interface IBaseService
{
    APIResponse responseModel { get; set; }
    Task<T> SendAsync<T>(APIRequest apiRequest);
}
