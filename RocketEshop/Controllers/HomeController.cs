using Microsoft.AspNetCore.Mvc;
using RocketEshop.Core.Domain;
using RocketEshop.Core.Interfaces;
using RocketEshop.Core.Models;
using RocketEshop.Infrastructure.Data.ViewModel;

namespace RocketEshop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGamesService _gamesService;
        private readonly IGenresService _genresService;
        private static string? quickSearchFilter = null;
        private static bool availability = false;

        public HomeController(IGamesService service, IGenresService genresService)
        {
            this._gamesService = service;
            this._genresService = genresService;
        }

        public async Task<IActionResult> Index()
        {
            Filters filters = new Filters(quickSearchFilter, availability);
            return View(new HomeVM(_gamesService.FetchFilteredGamesList(filters), filters));
        }

        [HttpPost]
        [ActionName("Search")]
        public IActionResult Index([Bind("QuickSearchFilter,Availability")] Filters filtersVm)
        {
            quickSearchFilter = filtersVm.QuickSearchFilter;
            availability = filtersVm.Availability;
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
