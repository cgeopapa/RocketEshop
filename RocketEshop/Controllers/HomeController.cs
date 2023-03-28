using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RocketEshop.Core.Interfaces;
using RocketEshop.Infrastructure.Data.ViewModel;

namespace RocketEshop.Controllers
{
    [AllowAnonymous]
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

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}
