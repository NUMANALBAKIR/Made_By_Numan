using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace API.Models.AppUser;

public class AppUser : IdentityUser
{
    [Required, Display(Name = "User Name")]
    public string Name { get; set; }
    public string Address { get; set; }
}
