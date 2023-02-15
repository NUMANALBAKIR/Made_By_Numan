using Client.Models;
using Client.Services.IServices;

namespace Client.Services;

public class AppUserService : BaseService, IAppUserService
{
    private string _APIUrl;

    public AppUserService(IConfiguration configuration, IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
        _APIUrl = configuration.GetValue<string>("ServiceUrls:APIUrl");
    }

    public Task<T> GetAsync<T>(string appUserId, string token)
    {
        return SendAsync<T>(new APIRequest()
        {
            ApiType = Utility.StaticDetails.APIType.GET,
            Url = _APIUrl + "/api/AppUsersAPI?appUserId=" + appUserId,
            Token = token
        });
    }


}
