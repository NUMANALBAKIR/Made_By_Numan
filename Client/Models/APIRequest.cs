using static Utility.StaticDetails;

namespace Client.Models;

public class APIRequest
{
    public APIType ApiType { get; set; } = APIType.GET;
    public string Url { get; set; }
    public string Token { get; set; }

    public object Data { get; set; }
}
