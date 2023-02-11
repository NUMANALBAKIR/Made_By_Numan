using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Client.Models.User;

public class AppUser : IdentityUser
{
    [Required, Display(Name = "User Name")]
    public string Name { get; set; }
    [Required]
    public string Address { get; set; }
}
