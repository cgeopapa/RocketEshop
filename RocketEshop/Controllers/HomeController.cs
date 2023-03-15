using Microsoft.AspNetCore.Mvc;
using RocketEshop.Core.Interfaces;
using RocketEshop.Core.Models;

namespace RocketEshop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGamesService _gamesService;
        private static string? quickSearchFilter = null;

        public HomeController(IGamesService service)
        {
            _gamesService = service;
        }

        // GET: Games
        //Display all Games
        public async Task<IActionResult> Index()
        {
            if(quickSearchFilter == null)
            {
                return View(await _gamesService.GetAllAsync());
            } 
            else
            {
                return View(_gamesService.GetByQuickSearchFilter(quickSearchFilter));
            }
        }

        [HttpPost]
        [ActionName("Search")]
        public IActionResult Index([Bind("QuickSearchFilter")] string QuickSearchFilter)
        {
            quickSearchFilter = QuickSearchFilter;
            return RedirectToAction(nameof(Index));
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
            Game? game = await _gamesService.GetByIdAsync(id.Value);
            if (game == null)
            {
                return NotFound();
            }
            return View(game);
        }
    }
}
