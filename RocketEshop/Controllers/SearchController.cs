using Microsoft.AspNetCore.Mvc;
using RocketEshop.Core.Domain;
using RocketEshop.Core.Interfaces;
using RocketEshop.Infrastructure.Data.ViewModel;

namespace RocketEshop.Controllers
{
    public class SearchController : Controller
    {
        private readonly IGamesService _gamesService;
        private static Filters Filters;

        public SearchController(IGamesService service)
        {
            this._gamesService = service;
            Filters ??= new Filters();
        }

        public async Task<IActionResult> Index()
        {
            return View(new HomeVM(_gamesService.FetchFilteredGamesList(Filters), SearchController.Filters));
        }

        [HttpPost]
        [ActionName("Search")]
        public async Task<IActionResult> Index([Bind("QuickSearchFilter,Availability,Sorting,MinPrice,MaxPrice")] Filters filtersVm)
        {
            Filters = filtersVm;
            return RedirectToAction(nameof(Index));
        }
    }
}
