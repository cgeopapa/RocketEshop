using System.Globalization;
using System.Text.Json.Nodes;
using CsvHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using RocketEshop.Core.Interfaces;
using RocketEshop.Core.Models;
using RocketEshop.Infrastructure.Data.ViewModel;

namespace RocketEshop.Controllers
{
    [Authorize("AdminOnly")]
    public class GamesController : Controller
    {
        // Service
        private readonly IGamesService _gamesService;
        private readonly IGenresService _genresService;
        
        private readonly RequestLocalizationOptions localizationOptions;

        private static List<GameCSVRecordVM> gamesFromCSV;

        public GamesController(IGamesService service, IGenresService genresService, RequestLocalizationOptions localizationOptions)
        {
            _gamesService = service;
            _genresService = genresService;
            this.localizationOptions = localizationOptions;
        }

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
                Game game = await gameEntityFromGameCreateRequestDto(gameVm);
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

        // CSV IMPORT
        
        [HttpGet]
        public IActionResult CsvInsert(List<GameCSVRecordVM>? games = null)
        {
            if (games == null)
            {
                games = new List<GameCSVRecordVM>();
            }
            return View(games);
        }

        [HttpPost]
        public IActionResult CsvInsert(IFormFile file, [FromServices] Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            string fileName = $"{hostingEnvironment.WebRootPath}\\uploads\\{file.FileName}";
            using (FileStream fileStream = System.IO.File.Create(fileName))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }

            try
            {
                gamesFromCSV = this.GetGamesList(file.FileName);
            }
            catch(Exception e)
            {
                TempData["error"] = e.Message;
            }
            return View(gamesFromCSV);
        }

        [HttpPost]
        public IActionResult CsvInsertConfirm()
        {
            List<Game> gameEntities = new List<Game>();
            foreach (GameCSVRecordVM gameCsvRecordVm in gamesFromCSV)
            {
                Game game = new Game();
                game.Title = gameCsvRecordVm.Title;
                game.Description = gameCsvRecordVm.Description;
                game.ReleaseDate = gameCsvRecordVm.ReleaseDate.ToDateTime(new TimeOnly(0, 0));
                game.ImageUrl = gameCsvRecordVm.ImageUrl;
                game.Price = gameCsvRecordVm.Price;
                game.Quantity = gameCsvRecordVm.Quantity;
                game.Rating = gameCsvRecordVm.Rating;

                game.GameGenreLink = new List<GameGenre>();
                foreach (string genreName in gameCsvRecordVm.Genres)
                {
                    Genre? genre = _genresService.FetchGenreByName(genreName);
                    if (genre != null)
                    {
                        GameGenre gameGenre = new GameGenre();
                        gameGenre.Game = game;
                        gameGenre.Genre = genre;
                        game.GameGenreLink.Add(gameGenre);
                    }
                }

                gameEntities.Add(game);
            }
            
            _gamesService.BulkUploadGames(gameEntities);
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

        private async Task<Game> gameEntityFromUpdateGameVm(GameCreateUpdateVM gameVm)
        {
            Game game = new Game();
            game.Id = gameVm.Id ?? throw new Exception("A game with no Id was given");
            game.Title = gameVm.Title;
            game.Price = gameVm.Price;
            game.ImageUrl = gameVm.ImageUrl;
            game.Quantity = gameVm.Quantity;
            game.Rating = gameVm.Rating;
            game.ReleaseDate = gameVm.ReleaseDate;
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
            game.Price = gameVm.Price;
            game.ImageUrl = gameVm.ImageUrl;
            game.Quantity = gameVm.Quantity;
            game.Rating = gameVm.Rating;
            game.ReleaseDate = gameVm.ReleaseDate;
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

        private List<GameCSVRecordVM> GetGamesList(string fileName)
        {
            List<GameCSVRecordVM> games = new List<GameCSVRecordVM>();
            var path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\uploads"}" + "\\" + fileName;
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                csv.Context.RegisterClassMap<GameCSVRecordVMMap>();

                string[] headers = { "Title", "Price", "ImageUrl", "Quantity", "Rating", "ReleaseDate", "Description", "Genres" };
                string[]? csvHeaders = csv.HeaderRecord;
                if (csvHeaders == null)
                {
                    throw new Exception("The CAV file must contain headers");
                }
                if (!headers.SequenceEqual(csvHeaders))
                {
                    throw new Exception("The given CSV headers are not correct");
                }

                int i = 1;
                while (csv.Read())
                {
                    try
                    {
                        GameCSVRecordVM? game = csv.GetRecord<GameCSVRecordVM>();
                        if (game != null)
                        {
                            games.Add(game);
                        }

                        i++;
                    }
                    catch (Exception)
                    {
                        games.Clear();
                        throw new Exception($"There was an problematic value at row {i}");
                    }
                }
            }
            return games;
        }

        // Not used due to extremely high latency
        private async Task<dynamic> getEURtoUSDConvertionRate()
        {
            var http = new HttpClient();
            var query = QueryHelpers.AddQueryString("https://api.apilayer.com/exchangerates_data/convert", "from", "EUR");
            query = QueryHelpers.AddQueryString(query, "to", "USD");
            query = QueryHelpers.AddQueryString(query, "amount", "1");
            http.DefaultRequestHeaders.Add("apikey", "NjD3D4PMM2zpNdfnxKfOpZo5ro80NQRs");

            string? res = await http.GetStringAsync(query);

            var json = JsonNode.Parse(res)?.AsObject();
            if (json == null)
            {
                return 1m;
            }

            string? result = json["result"]?.ToString();
            return result == null ? 1m : decimal.Parse(result);
        }
    }
}
