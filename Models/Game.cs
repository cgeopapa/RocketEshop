using RocketEshop.Data.Enums;

namespace RocketEshop.Models
{
    public class Game
    {
        public int gameId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public float price { get; set; }
        public string imgUrl { get; set; }
        public string features { get; set; }
        public int quantity { get; set; }
        public bool availability { get; set; }
        public Rating rating { get; set; }
    }
}
