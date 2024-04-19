using Microsoft.AspNetCore.Mvc;

namespace UgbFront.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}