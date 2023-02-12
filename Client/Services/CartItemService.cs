using Client.Models;
using Client.Models.OrderFoodDTOs;
using Client.Services.IServices;

namespace Client.Services;

public class CartItemService : BaseService, ICartItemService
{
    private string _APIUrl;

    public CartItemService(IConfiguration configuration, IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
        _APIUrl = configuration.GetValue<string>("ServiceUrls:APIUrl");
    }

    public Task<T> CreateAsync<T>(CartItemCreateDTO dto, string token)
    {
        return SendAsync<T>(new APIRequest()
        {
            ApiType = Utility.StaticDetails.APIType.POST,
            Url = _APIUrl + "/api/CartItemsAPI",
            Token = token,
            Data = dto
        });
    }

    public Task<T> DeleteAsync<T>(int id, string token)
    {
        return SendAsync<T>(new APIRequest()
        {
            ApiType = Utility.StaticDetails.APIType.DELETE,
            Url = _APIUrl + "/api/CartItemsAPI/" + id,
            Token = token,
        });
    }

    public Task<T> GetAllAsync<T>(string appUserId, string token)
    {
        return SendAsync<T>(new APIRequest()
        {
            ApiType = Utility.StaticDetails.APIType.GET,
            Url = _APIUrl + "/api/CartItemsAPI" + "?appUserId=" + appUserId,
            Token = token
        });
    }

    public Task<T> GetAsync<T>(int foodId, string appUserId, string token)
    {
        return SendAsync<T>(new APIRequest()
        {
            ApiType = Utility.StaticDetails.APIType.GET,
            Url = _APIUrl + "/api/CartItemsAPI/" + foodId + "?appUserId=" + appUserId,
            Token = token
        });
    }

    public Task<T> UpdateAsync<T>(CartItemUpdateDTO dto, string token)
    {
        return SendAsync<T>(new APIRequest()
        {
            ApiType = Utility.StaticDetails.APIType.PUT,
            Url = _APIUrl + "/api/CartItemsAPI/" + dto.CartItemId,
            Token = token,
            Data = dto
        });
    }

}
