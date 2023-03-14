using RocketEshop.Core.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace RocketEshop.Core.Models
{
    public class Genre: IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Genre Name")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50)]
        public string Name { get; set; } = "";

        [Display(Name = "Description")]
        [StringLength(200)]
        public string? Description { get; set; }

        [Display(Name = "Games")]
        public List<GameGenre> Games { get; set; }
    }
}
