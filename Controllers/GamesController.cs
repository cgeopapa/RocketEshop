using Microsoft.AspNetCore.Mvc;
using RocketEshop.Data;
using RocketEshop.Data.Services;

namespace RocketEshop.Controllers
{
    public class GamesController : Controller
    {
        // Service
        private readonly IGamesService _service;

        public GamesController(IGamesService service)
        {
            _service = service;
        }

        //Display all Games
        public async Task<IActionResult> Index()
        {
            var allGames = await _service.GetAllAsync();
            return View(allGames);
        }

        // Noris einai ta alla

    }
}
