using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

//[Area("Guest")]
public class ChartController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

}
