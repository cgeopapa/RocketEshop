using RocketEshop.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace RocketEshop.Models
{
    public class Game
    {
        [Key]
        public int GameId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string ImageUrl { get; set; }
        public string Features { get; set; }
        public int Quantity { get; set; }
        public bool Availability { get; set; }
        public Rating Rating { get; set; }
    }
}
