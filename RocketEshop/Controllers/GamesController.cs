using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using RocketEshop.Core.Interfaces;
using RocketEshop.Core.Models;

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

        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Price,ImageUrl,Quantity,Rating")] Game game)
        {
            try
            {
                await _service.AddAsync(game);
                TempData["success"] = "Game added successfully!";
            }
            catch(Exception)
            {
                TempData["error"] = "There was an error.";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            return await GetGameDetails(id);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id,Title,Description,Price,ImageUrl,Quantity,Rating")] Game game)
        {
            try
            {
                await _service.UpdateAsync(game);
                TempData["success"] = "Game updated successfully!";
            }
            catch (Exception)
            {
                TempData["error"] = "There was an error.";
            }
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
            try
            {
                await _service.DeleteAsync(id);
                TempData["success"] = "Game deleted successfully!";
            }
            catch (Exception)
            {
                TempData["error"] = "There was an error.";
            }
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
