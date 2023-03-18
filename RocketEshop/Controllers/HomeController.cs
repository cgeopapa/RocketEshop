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
        private static Filters Filters;

        public HomeController(IGamesService service)
        {
            this._gamesService = service;
            Filters ??= new Filters();
        }

        public async Task<IActionResult> Index()
        {
            return View(new HomeVM(_gamesService.FetchFilteredGamesList(Filters), HomeController.Filters));
        }

        [HttpPost]
        [ActionName("Search")]
        public IActionResult Index([Bind("QuickSearchFilter,Availability,Sorting,MinPrice,MaxPrice")] Filters filtersVm)
        {
            Filters = filtersVm;

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
