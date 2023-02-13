﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Client.Models.User;

public class AppUserDTO : IdentityUser
{
    public string Name { get; set; }
    public string Address { get; set; }
}