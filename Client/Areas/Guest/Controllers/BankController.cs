using Microsoft.AspNetCore.Mvc;

namespace Client.Areas.Guest.Controllers;

[Area("Guest")]
public class BankController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
