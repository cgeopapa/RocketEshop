using Microsoft.AspNetCore.Mvc;

namespace RocketEshop.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
