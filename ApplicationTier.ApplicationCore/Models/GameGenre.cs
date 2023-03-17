using RocketEshop.Core.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RocketEshop.Core.Models
{
    public class GameGenre: IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Game Game { get; set; }

        public Genre Genre { get; set; }
    }
}
