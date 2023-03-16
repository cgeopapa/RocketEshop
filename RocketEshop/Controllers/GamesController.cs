using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RocketEshop.Core.Interfaces;
using RocketEshop.Core.Models;
using RocketEshop.Model;
using RocketEshop.Model.Games;

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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _gamesService.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            Create game = new Create(await GetGameOptions());
            return View(game);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Title,Description,Price,ImageUrl,Quantity,ReleaseDate,Rating,Genres")] GameViewModel gameViewModel)
        {
            try
            {
                Core.Models.Game game = await gameEntityFromGameCreateRequestDto(gameViewModel);
                await _gamesService.AddAsync(game);
                TempData["success"] = "GameViewModel added successfully!";
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
            Edit gameUpdateRequestDto = new Edit(game, await GetGameOptions());
            return View(gameUpdateRequestDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id,Title,Description,Price,ImageUrl,Quantity,ReleaseDate,Rating,Genres")] GameViewModel gameViewModel)
        {
            try
            {
                Game game = await gameEntityFromGameUpdateRequestDto(gameViewModel);
                await _gamesService.UpdateAsync(game);
                TempData["success"] = "Game updated successfully!";
            }
            catch (Exception e)
            {

                TempData["error"] = e.Message;
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
            return View(game);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Core.Models.Game game)
        {
            //try
            //{
                await _gamesService.DeleteAsync(game);
                TempData["success"] = "GameViewModel deleted successfully!";
            //}
            //catch (Exception)
            //{
            //    TempData["error"] = "There was an error.";
            //}
            return RedirectToAction(nameof(Index));
            
        }

        // PRIVATE - UTILS

        private async Task<Core.Models.Game?> GetGameDetails(int? id)
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

        private async Task<Game> gameEntityFromGameUpdateRequestDto(GameViewModel gameUpdateRequestDto)
        {
            Game game = new Game();
            game.Id = gameUpdateRequestDto.Id ?? throw new Exception("A game with no Id was given");
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

        private async Task<Game> gameEntityFromGameCreateRequestDto(GameViewModel gameDto)
        {
            Game game = new Game();
            game.Title = gameDto.Title;
            game.Price = gameDto.Price;
            game.ImageUrl = gameDto.ImageUrl;
            game.Quantity = gameDto.Quantity;
            game.Rating = gameDto.Rating;
            game.Release_Date = gameDto.ReleaseDate;
            game.Description = gameDto.Description;

            game.Genres = new List<Genre>();
            foreach (int genreId in gameDto.Genres)
            {
                Genre? genre = await _genresService.GetByIdAsync(genreId) ?? throw new Exception("cannot find genre with id " + genreId);
                game.Genres.Add(genre);
            }
            return game;
        }
    }
}
