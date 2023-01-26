using Microsoft.AspNetCore.Mvc;

namespace Client.Areas.Guest.Controllers;

[Area("Guest")]
public class OrderPizzaController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
