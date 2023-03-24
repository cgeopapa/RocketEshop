﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RocketEshop.Core.Interfaces;
using RocketEshop.Core.Models;
using System.Globalization;
using RocketEshop.Infrastructure.Data.ViewModel;
using Microsoft.AspNetCore.Authorization;
using RocketEshop.Data.Static;
using System.Data;
using CsvHelper;

namespace RocketEshop.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
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

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _gamesService.GetAllAsync());
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            Game? game = await GetGameDetails(id);
            if (game == null)
            {
                return NotFound();
            }
            GameVM gameVm = new GameVM(game);
            return View(gameVm);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            TempData["genreOptions"] = await GetGameOptions();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Title,Description,Price,ImageUrl,Quantity,ReleaseDate,Rating,Genres")] GameCreateUpdateVM gameVm)
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
            GameCreateUpdateVM gameVm = new GameCreateUpdateVM(game);
            TempData["genreOptions"] = await GetGameOptions();
            return View(gameVm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id,Title,Description,Price,ImageUrl,Quantity,ReleaseDate,Rating,Genres")] GameCreateUpdateVM gameVm)
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
        public async Task<IActionResult> DeleteConfirmed(Game game)
        {
            await _gamesService.DeleteAsync(game);
            TempData["success"] = "GameViewModel deleted successfully!";
            return RedirectToAction(nameof(Index));
            
        }

        [HttpGet]
        public IActionResult CsvInsert(List<Game> games = null)
        {
            games = games == null ? new List<Game>() : games;
            return View(games);
        }

        [HttpPost]
        public IActionResult CsvInsert(IFormFile file, [FromServices] Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            #region Upload CSV
            string fileName = $"{hostingEnvironment.WebRootPath}\\files\\{file.FileName}";
            using (FileStream fileStream = System.IO.File.Create(fileName))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }
            #endregion

            var games = this.GetGamesList(file.FileName);
            return View(games);
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

        private async Task<Game> gameEntityFromUpdateGameVm(GameCreateUpdateVM gameVm)
        {
            Game game = new Game();
            game.Id = gameVm.Id ?? throw new Exception("A game with no Id was given");
            game.Title = gameVm.Title;
            game.Price = float.Parse(gameVm.Price, CultureInfo.InvariantCulture);
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

        private async Task<Game> gameEntityFromGameCreateRequestDto(GameCreateUpdateVM gameVm)
        {
            Game game = new Game();
            game.Title = gameVm.Title;
            game.Price = float.Parse(gameVm.Price, CultureInfo.InvariantCulture);
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

        private List<Game> GetGamesList(string fileName)
        {
            List<Game> games = new List<Game>();

            #region Read CSV
            var path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}" + "\\" + fileName;
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var game = csv.GetRecord<Game>();
                    games.Add(game);
                }
            }
            #endregion

            #region Create CSV
            path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\FilesTo"}";
            using (var write = new StreamWriter(path + "\\NewFile.csv"))
            using (var csv = new CsvWriter(write, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(games);
            }
            #endregion

            return games;
        }
    }
}
