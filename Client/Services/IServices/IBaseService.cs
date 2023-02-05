using Client.Models;

namespace Client.Services.IServices;

public interface IBaseService
{
    Task<T> SendAsync<T>(APIRequest apiRequest);
}
