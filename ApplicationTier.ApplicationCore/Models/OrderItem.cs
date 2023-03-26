using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RocketEshop.Core.Interfaces;

namespace RocketEshop.Core.Models
{
    public class OrderItem: IEntity<int>
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Amount { get; set; }
        public decimal Price { get; set; }

        public int GameId { get; set; }
        [ForeignKey("GameId")]
        public Game Game { get; set; }

        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }

    }
}
