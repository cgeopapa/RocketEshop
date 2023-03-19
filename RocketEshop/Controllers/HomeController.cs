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
        private static string? _quickFilter;

        public HomeController(IGamesService service)
        {
            this._gamesService = service;
        }

        public IActionResult Index()
        {
            return View(new HomeVM(
                _gamesService.FetchLatestReleasedGames(3),
                _gamesService.FetchGoodRatedGames(3))
            );
        }
    }
}
