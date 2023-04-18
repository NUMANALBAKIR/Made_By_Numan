using API.Models;

namespace API;

public static class Factory
{
    public static IAPIResponse NewAPIResponse()
    {
        return new APIResponse();
    }

}
