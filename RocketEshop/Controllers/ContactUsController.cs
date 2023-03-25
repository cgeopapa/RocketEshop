using Microsoft.AspNetCore.Mvc;

namespace RocketEshop.Controllers
{
    public class ContactUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
