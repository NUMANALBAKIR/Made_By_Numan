using Microsoft.AspNetCore.Identity;

namespace API.Models.User;

public class AppUserDTO : IdentityUser
{
    public string Name { get; set; }
    public string Address { get; set; }
}
