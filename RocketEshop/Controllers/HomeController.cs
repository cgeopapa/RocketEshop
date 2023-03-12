using Microsoft.AspNetCore.Mvc;
using RocketEshop.Core.Interfaces;
using RocketEshop.Core.Models;

namespace RocketEshop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGamesService _service;

        public HomeController(IGamesService service)
        {
            _service = service;
        }

        // GET: Games
        //Display all Games
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllAsync());
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            return await GetGameDetails(id);
        }

        private async Task<IActionResult> GetGameDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Game? game = await _service.GetByIdAsync(id.Value);
            if (game == null)
            {
                return NotFound();
            }
            return View(game);
        }
    }
}
