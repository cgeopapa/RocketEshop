using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RocketEshop.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        public int GameId { get; set; }
        [ForeignKey("GameId")]
        public virtual Game Game{ get; set; }

        public int UserId { get; set; }
        public int Quantity { get; set; }

    }
}
