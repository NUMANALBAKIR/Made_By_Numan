
namespace Client.Services.IServices;

public interface IAppUserService
{
    Task<T> GetAsync<T>(string appUserId, string token);
}
