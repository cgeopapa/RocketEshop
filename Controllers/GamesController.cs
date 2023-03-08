using Microsoft.AspNetCore.Mvc;
using RocketEshop.Models;
using RocketEshop.Data.Services;

namespace RocketEshop.Controllers
{
    public class GamesController : Controller
    {
        private readonly IGamesService _service;

        public GamesController(IGamesService service)
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Price,ImageUrl,Features,Quantity,Availability,Rating")] Game game)
        {
            await _service.AddAsync(game);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            return await GetGameDetails(id);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id,Title,Description,Price,ImageUrl,Features,Quantity,Availability,Rating")] Game game)
        {
            await _service.UpdateAsync(game);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            return await GetGameDetails(id);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
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
