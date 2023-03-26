using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RocketEshop.Core.Interfaces;

namespace RocketEshop.Core.Models
{
    public class ShoppingCartItem: IEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Game Game { get; set; } = new Game();

        public int Amount { get; set; }
    }
}
