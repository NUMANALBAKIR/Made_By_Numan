﻿using Microsoft.AspNetCore.Mvc;

namespace Client.Areas.Guest.Controllers;

[Area("Guest")]
public class OrderFoodController : Controller
{
    public IActionResult Index()
    {
        return View();
    }


    public IActionResult Cart()
    {
        return View();
    }
}
