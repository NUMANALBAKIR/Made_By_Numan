using Client.Models;
using Client.Models.OrderFoodDTOs;
using Client.Services.IServices;

namespace Client.Services;

public class OrderHeaderService : BaseService, IOrderHeaderService
{
    private string _APIUrl;

    public OrderHeaderService(IConfiguration configuration, IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
        _APIUrl = configuration.GetValue<string>("ServiceUrls:APIUrl");
    }

    public Task<T> CreateAsync<T>(OrderHeaderCreateDTO dto, string token)
    {
        return SendAsync<T>(new APIRequest()
        {
            ApiType = Utility.StaticDetails.APIType.POST,
            Url = _APIUrl + "/api/OrderHeadersAPI",
            Token = token,
            Data = dto
        });
    }

    public Task<T> DeleteAsync<T>(int id, string token)
    {
        return SendAsync<T>(new APIRequest()
        {
            ApiType = Utility.StaticDetails.APIType.DELETE,
            Url = _APIUrl + "/api/OrderHeadersAPI/" + id,
            Token = token,
        });
    }

    public Task<T> GetAllAsync<T>(string token)
    {
        return SendAsync<T>(new APIRequest()
        {
            ApiType = Utility.StaticDetails.APIType.GET,
            Url = _APIUrl + "/api/OrderHeadersAPI",
            Token = token
        });
    }

    public Task<T> GetAsync<T>(int id, string token)
    {
        return SendAsync<T>(new APIRequest()
        {
            ApiType = Utility.StaticDetails.APIType.GET,
            Url = _APIUrl + "/api/OrderHeadersAPI/" + id,
            Token = token
        });
    }

    public Task<T> UpdateAsync<T>(OrderHeaderUpdateDTO dto, string token)
    {
        return SendAsync<T>(new APIRequest()
        {
            ApiType = Utility.StaticDetails.APIType.PUT,
            Url = _APIUrl + "/api/OrderHeadersAPI/" + dto.OrderHeaderId,
            Token = token,
            Data = dto
        });
    }

}
