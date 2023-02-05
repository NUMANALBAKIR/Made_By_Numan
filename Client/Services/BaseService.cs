using Client.Models;
using Client.Services.IServices;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Utility;

namespace Client.Services;

public class BaseService : IBaseService
{
    private readonly IHttpClientFactory _httpClient;
    public BaseService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory;
    }

    // benefit of return-type T is, it can return ApiResponse or ExceptionResponse.
    public async Task<T> SendAsync<T>(APIRequest apiRequest)
    {
        try
        {
            // Request processing =>
            HttpRequestMessage requestMessage = new();
            requestMessage.Headers.Add("Accept", "application/json");
            requestMessage.RequestUri = new Uri(apiRequest.Url);
            if (apiRequest.Data == null)
            {
                requestMessage.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                    Encoding.UTF8, "application/json");
            }
            switch (apiRequest.ApiType)
            {
                case StaticDetails.APIType.GET:
                    requestMessage.Method = HttpMethod.Get;
                    break;
                case StaticDetails.APIType.POST:
                    requestMessage.Method = HttpMethod.Post;
                    break;
                case StaticDetails.APIType.PUT:
                    requestMessage.Method = HttpMethod.Put;
                    break;
                case StaticDetails.APIType.DELETE:
                    requestMessage.Method = HttpMethod.Delete;
                    break;
            }

            var client = _httpClient.CreateClient("clientAPI");
            if (string.IsNullOrEmpty(apiRequest.Token) == false)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiRequest.Token);
            }


            // Response processing =>
            HttpResponseMessage responseMessage = await client.SendAsync(requestMessage);
            var responseString = await responseMessage.Content.ReadAsStringAsync();

            try
            {
                // responseString => Deserialize to APIResponse
                APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(responseString);

                // there's a response, but it's not what we want.
                if (apiResponse != null &&
                    (responseMessage.StatusCode == HttpStatusCode.BadRequest || responseMessage.StatusCode == HttpStatusCode.NotFound))
                {
                    //BadRequest;
                    apiResponse.IsSuccess = false;
                    // APIResponse => Serialize to json => Deserialize to T
                    var jsonApiResponse = JsonConvert.SerializeObject(apiResponse);
                    var tApiResponse = JsonConvert.DeserializeObject<T>(jsonApiResponse);
                    return tApiResponse;
                }
            }
            catch (Exception ex)
            {
                // responseString => Deserialize to T
                var exceptionResponse = JsonConvert.DeserializeObject<T>(responseString);
                return exceptionResponse;
            }

            // responseString => Deserialize to T
            var tApiResponse1 = JsonConvert.DeserializeObject<T>(responseString);
            return tApiResponse1;
        }
        catch (Exception ex)
        {
            APIResponse apiResponse = new()
            {
                ErrorMessage = Convert.ToString(ex),
                IsSuccess = false
            };
            // APIResponse => Serialize to json => Deserialize to T
            var jsonApiResponse = JsonConvert.SerializeObject(apiResponse);
            var tApiResponse2 = JsonConvert.DeserializeObject<T>(jsonApiResponse);
            return tApiResponse2;
        }
    }
}
