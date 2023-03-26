using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RocketEshop.Core.Interfaces;

namespace RocketEshop.Core.Models
{
    public class GameGenre: IEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Game Game { get; set; }

        public Genre Genre { get; set; }
    }
}
