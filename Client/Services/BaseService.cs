using Client.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Utility;

namespace Client.Services;

public class BaseService : IBaseService
{
    private readonly IHttpClientFactory httpClient;
    public BaseService(IHttpClientFactory httpClientFactory)
    {
        apiResponse = new(); // ???
        httpClient = httpClientFactory;
    }

    public APIResponse apiResponse { get; set; }
    public async Task<T> SendAsync<T>(APIRequest apiRequest)
    {
        try
        {
            // Request processing
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

            var client = httpClient.CreateClient("clientAPI");
            if (string.IsNullOrEmpty(apiRequest.Token) == false)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiRequest.Token);
            }

            // Response processing
            HttpResponseMessage responseMessage = await client.SendAsync(requestMessage);
            var responseString = await responseMessage.Content.ReadAsStringAsync();

            try
            {
                APIResponse APIResponse = JsonConvert.DeserializeObject<APIResponse>(responseString);

                if (APIResponse != null && (responseMessage.StatusCode == HttpStatusCode.BadRequest
                    || responseMessage.StatusCode == HttpStatusCode.NotFound))
                {
                    APIResponse.StatusCode = HttpStatusCode.BadRequest;
                    APIResponse.IsSuccess = false;
                    var res = JsonConvert.SerializeObject(APIResponse);
                    var returnObj = JsonConvert.DeserializeObject<T>(res);
                    return returnObj;
                }
            }
            catch (Exception ex)
            {
                var exceptionResponse = JsonConvert.DeserializeObject<T>(responseString);
                return exceptionResponse;
            }
            var apiResponse = JsonConvert.DeserializeObject<T>(responseString);
            return apiResponse;
        }
        catch (Exception ex)
        {
            var dto = new APIResponse
            {
                ErrorMessages = new List<string> { Convert.ToString(ex.Message) },
                IsSuccess = false
            };
            var res = JsonConvert.SerializeObject(dto);
            var APIResponse = JsonConvert.DeserializeObject<T>(res);
            return APIResponse;
        }
    }
}
