using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RocketEshop.Core.Interfaces;
using RocketEshop.Core.Models;

namespace RocketEshop.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class GenresController : Controller
    {
        // Service
        private readonly IGenresService _service;

        public GenresController(IGenresService service)
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
        public async Task<IActionResult> Create([Bind("Name,Description")] Genre genre)
        {
            try
            {
                await _service.AddAsync(genre);
                TempData["success"] = "Genre added successfully!";
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
            return await GetGenreDetails(id);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id,Name,Description")] Genre genre)
        {
            try
            {
                await _service.UpdateAsync(genre);
                TempData["success"] = "Genre updated successfully!";
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
            return await GetGenreDetails(id);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Genre genre)
        {
            //try
            //{
                await _service.DeleteAsync(genre);
                TempData["success"] = "Genre deleted successfully!";
            //}
            //catch (Exception)
            //{
            //    TempData["error"] = "There was an error.";
            //}
            return RedirectToAction(nameof(Index));
            
        }

        private async Task<IActionResult> GetGenreDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Genre? genre = await _service.GetByIdAsync(id.Value);
            if (genre == null)
            {
                return NotFound();
            }
            return View(genre);
        }
    }
}
