using Client.Models;
using Client.Models.Bank;
using Client.Services.IServices;

namespace Client.Services;

public class TransactionService : BaseService, ITransactionService
{
    private string _APIUrl;

    public TransactionService(IConfiguration configuration, IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
        _APIUrl = configuration.GetValue<string>("ServiceUrls:APIUrl");
    }

    public Task<T> CreateAsync<T>(TransactionCreateDTO dto, string token)
    {
        return SendAsync<T>(new APIRequest()
        {
            ApiType = Utility.StaticDetails.APIType.POST,
            Url = _APIUrl + "/api/TransactionsAPI",
            Token = token,
            Data = dto
        });
    }

    public Task<T> DeleteAsync<T>(int id, string token)
    {
        return SendAsync<T>(new APIRequest()
        {
            ApiType = Utility.StaticDetails.APIType.DELETE,
            Url = _APIUrl + "/api/TransactionsAPI/" + id,
            Token = token,
        });
    }

    //get all Transactions of this appUser
    public Task<T> GetAllAsync<T>(string appUserId, string token)
    {
        return SendAsync<T>(new APIRequest()
        {
            ApiType = Utility.StaticDetails.APIType.GET,
            Url = _APIUrl + "/api/TransactionsAPI?appUserId=" + appUserId,
            Token = token
        });
    }

    public Task<T> GetAsync<T>(int id, string token)
    {
        return SendAsync<T>(new APIRequest()
        {
            ApiType = Utility.StaticDetails.APIType.GET,
            Url = _APIUrl + "/api/TransactionsAPI/" + id,
            Token = token
        });
    }

    public Task<T> UpdateAsync<T>(TransactionUpdateDTO dto, string token)
    {
        return SendAsync<T>(new APIRequest()
        {
            ApiType = Utility.StaticDetails.APIType.PUT,
            Url = _APIUrl + "/api/TransactionsAPI/" + dto.TransactionId,
            Token = token,
            Data = dto
        });
    }

}
