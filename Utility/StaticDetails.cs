namespace Utility;

public static class StaticDetails
{
    public enum APIType
    {
        GET,
        POST,
        PUT,
        DELETE
    }

    public const string Role_Admin = "Admin";
    public const string Role_Guest = "Guest";

    public static string SessionToken = "JWTToken";

}