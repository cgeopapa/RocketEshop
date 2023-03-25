using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RocketEshop.Core.Domain;
using RocketEshop.Core.Interfaces;
using RocketEshop.Infrastructure.Data.ViewModel;

namespace RocketEshop.Controllers
{
    [AllowAnonymous]
    public class BrowseController : Controller
    {
        private readonly IGamesService _gamesService;
        private static Filters Filters;

        public BrowseController(IGamesService service)
        {
            this._gamesService = service;
            Filters ??= new Filters();
        }

        public async Task<IActionResult> Index()
        {
            return View(new BrowseVM(_gamesService.FetchFilteredGamesList(Filters), Filters));
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
