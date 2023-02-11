using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Areas.Guest.Controllers;

[Area("Guest"), Authorize]
public class BankController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
