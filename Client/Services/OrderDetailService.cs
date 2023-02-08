using Client.Models;
using Client.Models.OrderFoodDTOs;
using Client.Services.IServices;

namespace Client.Services;

public class OrderDetailService : BaseService, IOrderDetailService
{
    private string _APIUrl;

    public OrderDetailService(IConfiguration configuration, IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
        _APIUrl = configuration.GetValue<string>("ServiceUrls:APIUrl");
    }

    public Task<T> CreateAsync<T>(OrderDetailCreateDTO dto, string token)
    {
        return SendAsync<T>(new APIRequest()
        {
            ApiType = Utility.StaticDetails.APIType.POST,
            Url = _APIUrl + "/api/OrderDetailsAPI",
            Token = token,
            Data = dto
        });
    }

    public Task<T> DeleteAsync<T>(int id, string token)
    {
        return SendAsync<T>(new APIRequest()
        {
            ApiType = Utility.StaticDetails.APIType.DELETE,
            Url = _APIUrl + "/api/OrderDetailsAPI/" + id,
            Token = token,
        });
    }

    public Task<T> GetAllAsync<T>(string token)
    {
        return SendAsync<T>(new APIRequest()
        {
            ApiType = Utility.StaticDetails.APIType.GET,
            Url = _APIUrl + "/api/OrderDetailsAPI",
            Token = token
        });
    }

    public Task<T> GetAsync<T>(int id, string token)
    {
        return SendAsync<T>(new APIRequest()
        {
            ApiType = Utility.StaticDetails.APIType.GET,
            Url = _APIUrl + "/api/OrderDetailsAPI/" + id,
            Token = token
        });
    }

    public Task<T> UpdateAsync<T>(OrderDetailUpdateDTO dto, string token)
    {
        return SendAsync<T>(new APIRequest()
        {
            ApiType = Utility.StaticDetails.APIType.PUT,
            Url = _APIUrl + "/api/OrderDetailsAPI/" + dto.OrderDetailId,
            Token = token,
            Data = dto
        });
    }

}
