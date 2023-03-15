using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RocketEshop.Core.Interfaces;
using RocketEshop.Core.Models;
using RocketEshop.Dtos.Game;

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
            GameUpdateRequestDto game = new GameUpdateRequestDto(await GetGameOptions());
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
            GameUpdateRequestDto gameUpdateRequestDto = new GameUpdateRequestDto(game, await GetGameOptions());
            return View(gameUpdateRequestDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id,Title,Description,Price,ImageUrl,Quantity,ReleaseDate,Rating,Genres")] GameUpdateRequestDto gameUpdateRequestDto)
        {
            try
            {
                Game game = await gameEntityFromGameUpdateRequestDto(gameUpdateRequestDto);
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
            GameUpdateRequestDto gameCreateUpdateRequestDto = new GameUpdateRequestDto(game);
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

        private async Task<SelectList> GetGameOptions()
        {
            IEnumerable<Genre> genres = await _genresService.GetAllAsync();
            return new SelectList(genres, "Id", "Name");
        }

        private async Task<Game> gameEntityFromGameUpdateRequestDto(GameUpdateRequestDto gameUpdateRequestDto)
        {
            Game game = new Game();
            game.Id = gameUpdateRequestDto.Id;
            game.Title = gameUpdateRequestDto.Title;
            game.Price = gameUpdateRequestDto.Price;
            game.ImageUrl = gameUpdateRequestDto.ImageUrl;
            game.Quantity = gameUpdateRequestDto.Quantity;
            game.Rating = gameUpdateRequestDto.Rating;
            game.Release_Date = gameUpdateRequestDto.ReleaseDate;
            game.Description = gameUpdateRequestDto.Description;

            game.Genres = new List<Genre>();
            foreach (int genreId in gameUpdateRequestDto.Genres)
            {
                Genre? genre = await _genresService.GetByIdAsync(genreId) ?? throw new Exception("cannot find genre with id " + genreId);
                game.Genres.Add(genre);
            }
            return game;
        }
    }
}
