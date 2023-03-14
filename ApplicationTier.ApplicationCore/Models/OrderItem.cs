using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RocketEshop.Core.Models
{
    public class OrderItem
    {

        [Key]
        public int Id { get; set; }

        public int Amount { get; set; }
        public double Price { get; set; }

        public int GameId { get; set; }
        [ForeignKey("GameId")]
        public Game Game { get; set; }

        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }

    }
}
