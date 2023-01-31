using Client.Models;

namespace Client.Services;

public interface IBaseService
{
    APIResponse apiResponse { get; set; }
    Task<T> SendAsync<T>(APIRequest apiRequest);
}
