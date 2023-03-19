﻿using Microsoft.AspNetCore.Mvc;
using RocketEshop.Core.Domain;
using RocketEshop.Core.Interfaces;
using RocketEshop.Infrastructure.Data.ViewModel;

namespace RocketEshop.Controllers
{
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
            return View(new BrowseVM(_gamesService.FetchFilteredGamesList(Filters), BrowseController.Filters));
        }

        [HttpPost]
        [ActionName("Search")]
        public async Task<IActionResult> Index([Bind("QuickSearchFilter,Availability,Sorting,MinPrice,MaxPrice")] Filters filtersVm)
        {
            Filters = filtersVm;
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ActionName("QuickSearch")]
        public async Task<IActionResult> Index([Bind("QuickSearchFilter")] string quickSearchFilter)
        {
            Filters.QuickSearchFilter = quickSearchFilter;
            return RedirectToAction(nameof(Index));
        }
    }
}
