using CsvHelper.Configuration;
using RocketEshop.Core.Enums;

namespace RocketEshop.Infrastructure.Data.ViewModel;

public class GameCSVRecordVM
{
    public string Title { get; set; }
    public float Price { get; set; }
    public string ImageUrl { get; set; }
    public int Quantity { get; set; }
    public Rating Rating { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string Description { get; set; }
    public string[] Genres { get; set; }
}

public class GameCSVRecordVMMap : ClassMap<GameCSVRecordVM>
{
    public GameCSVRecordVMMap()
    {
        Map(game => game.Title);
        Map(game => game.Price);
        Map(game => game.ImageUrl);
        Map(game => game.Quantity);
        Map(game => game.Rating);
        Map(game => game.ReleaseDate);
        Map(game => game.Description);
        Map(game => game.Genres)
            .Convert(args => args.Row["Genres"].Split(";"));
    }
}