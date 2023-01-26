using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility;

public static class StaticDetails
{
    public enum FoodType
    {
        PIZZA,
        SALADS,
        STARTER
    }

    public const string Role_User_Admin = "Admin";
    public const string Role_User_Guest = "Guest";

    public static string SessionToken = "JWTToken";

}