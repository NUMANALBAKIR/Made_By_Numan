using Client.Models;
using Client.Models.BankDTOs;
using Client.Services.IServices;

namespace Client.Services;

public class BankAccountService : BaseService, IBankAccountService
{
    private string _APIUrl;

    public BankAccountService(IConfiguration configuration, IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
        _APIUrl = configuration.GetValue<string>("ServiceUrls:APIUrl");
    }

    public Task<T> GetAsync<T>(string appUserId, string token)
    {
        return SendAsync<T>(new APIRequest()
        {
            ApiType = Utility.StaticDetails.APIType.GET,
            Url = _APIUrl + "/api/BankAccountsAPI/" + appUserId,
            Token = token
        });
    }

    public Task<T> CreateAsync<T>(BankAccountCreateDTO dto, string token)
    {
        return SendAsync<T>(new APIRequest()
        {
            ApiType = Utility.StaticDetails.APIType.POST,
            Url = _APIUrl + "/api/BankAccountsAPI",
            Token = token,
            Data = dto
        });
    }

    public Task<T> UpdateAsync<T>(BankAccountUpdateDTO dto, string token)
    {
        return SendAsync<T>(new APIRequest()
        {
            ApiType = Utility.StaticDetails.APIType.PUT,
            Url = _APIUrl + "/api/BankAccountsAPI/" + dto.BankAccountId,
            Token = token,
            Data = dto
        });
    }


    public Task<T> GetAllAsync<T>(string token)
    {
        return SendAsync<T>(new APIRequest()
        {
            ApiType = Utility.StaticDetails.APIType.GET,
            Url = _APIUrl + "/api/BankAccountsAPI",
            Token = token
        });
    }
    public Task<T> DeleteAsync<T>(int id, string token)
    {
        return SendAsync<T>(new APIRequest()
        {
            ApiType = Utility.StaticDetails.APIType.DELETE,
            Url = _APIUrl + "/api/BankAccountsAPI/" + id,
            Token = token,
        });
    }
}
