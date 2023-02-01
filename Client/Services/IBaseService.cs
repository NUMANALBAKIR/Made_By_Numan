using Client.Models;

namespace Client.Services;

public interface IBaseService
{
    Task<T> SendAsync<T>(APIRequest apiRequest);
}
