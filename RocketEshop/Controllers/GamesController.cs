﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RocketEshop.Core.Interfaces;
using RocketEshop.Core.Models;
using RocketEshop.Infrastructure.Data.ViewModel;

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
            TempData["genreOptions"] = await GetGameOptions();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Title,Description,Price,ImageUrl,Quantity,ReleaseDate,Rating,Genres")] GameVM gameVm)
        {
            try
            {
                Core.Models.Game game = await gameEntityFromGameCreateRequestDto(gameVm);
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
            GameVM gameVm = new GameVM(game);
            TempData["genreOptions"] = await GetGameOptions();
            return View(gameVm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id,Title,Description,Price,ImageUrl,Quantity,ReleaseDate,Rating,Genres")] GameVM gameVm)
        {
            try
            {
                Game game = await gameEntityFromUpdateGameVm(gameVm);
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

        private async Task<Game> gameEntityFromUpdateGameVm(GameVM gameVm)
        {
            Game game = new Game();
            game.Id = gameVm.Id ?? throw new Exception("A game with no Id was given");
            game.Title = gameVm.Title;
            game.Price = gameVm.Price;
            game.ImageUrl = gameVm.ImageUrl;
            game.Quantity = gameVm.Quantity;
            game.Rating = gameVm.Rating;
            game.Release_Date = gameVm.ReleaseDate;
            game.Description = gameVm.Description;

            game.GameGenreLink = new List<GameGenre>();
            foreach (int genreId in gameVm.Genres)
            {
                Genre? genre = await _genresService.GetByIdAsync(genreId) ?? throw new Exception("cannot find genre with id " + genreId);
                
                GameGenre gameGenre = new GameGenre();
                gameGenre.Game = game;
                gameGenre.Genre = genre;

                game.GameGenreLink.Add(gameGenre);
            }
            return game;
        }

        private async Task<Game> gameEntityFromGameCreateRequestDto(GameVM gameVm)
        {
            Game game = new Game();
            game.Title = gameVm.Title;
            game.Price = gameVm.Price;
            game.ImageUrl = gameVm.ImageUrl;
            game.Quantity = gameVm.Quantity;
            game.Rating = gameVm.Rating;
            game.Release_Date = gameVm.ReleaseDate;
            game.Description = gameVm.Description;

            game.GameGenreLink = new List<GameGenre>();
            foreach (int genreId in gameVm.Genres)
            {
                Genre? genre = await _genresService.GetByIdAsync(genreId) ?? throw new Exception("cannot find genre with id " + genreId);

                GameGenre gameGenre = new GameGenre();
                gameGenre.Game = game;
                gameGenre.Genre = genre;

                game.GameGenreLink.Add(gameGenre);
            }
            return game;
        }
    }
}
