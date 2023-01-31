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

    public const string Role_User_Admin = "Admin";
    public const string Role_User_Guest = "Guest";

    public static string SessionToken = "JWTToken";

}