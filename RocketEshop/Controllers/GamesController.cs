using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RocketEshop.Core.Interfaces;
using RocketEshop.Core.Models;
using RocketEshop.Model;

namespace RocketEshop.Controllers
{
    public class GamesController : Controller
    {
        // Service
        private readonly IGamesService _gamesService;
        private readonly IGenresService _genresService;

        public GamesController(IGamesService service, IGenresService genresService)
        {
            _gamesService = service;
            _genresService = genresService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _gamesService.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            GameCreateUpdateRequestDto game = new GameCreateUpdateRequestDto(await GetGameOptions());
            return View(game);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Title,Description,Price,ImageUrl,Quantity,Release_Date,Rating")] Game game)
        {
            try
            {
                await _gamesService.AddAsync(game);
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
            Game? game = await GetGameDetails(id);
            if(game == null)
            {
                return NotFound();
            }
            GameCreateUpdateRequestDto gameCreateUpdateRequestDto = new GameCreateUpdateRequestDto(game, await GetGameOptions());
            return View(gameCreateUpdateRequestDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id,Title,Description,Price,ImageUrl,Quantity,Release_Date,Rating,Genres")] Game game)
        {
            try
            {
                await _gamesService.UpdateAsync(game);
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
            Game? game = await GetGameDetails(id);
            if (game == null)
            {
                return NotFound();
            }
            GameCreateUpdateRequestDto gameCreateUpdateRequestDto = new GameCreateUpdateRequestDto(game);
            return View(gameCreateUpdateRequestDto);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Game game)
        {
            //try
            //{
                await _gamesService.DeleteAsync(game);
                TempData["success"] = "Game deleted successfully!";
            //}
            //catch (Exception)
            //{
            //    TempData["error"] = "There was an error.";
            //}
            return RedirectToAction(nameof(Index));
            
        }

        // PRIVATE - UTILS

        private async Task<Game?> GetGameDetails(int? id)
        {
            if (id == null)
            {
                return null;
            }
            return await _gamesService.GetByIdAsync(id.Value);
        }

        private async Task<List<SelectListItem>> GetGameOptions()
        {
            List<SelectListItem> genreOptions = new List<SelectListItem>();
            IEnumerable<Genre> genres = await _genresService.GetAllAsync();
            foreach (var genre in genres)
            {
                genreOptions.Add(new SelectListItem(genre.Name, genre.Id.ToString()));
            }
            return genreOptions;
        }
    }
}
