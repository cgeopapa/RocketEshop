using RocketEshop.Core.Interfaces;
using RocketEshop.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RocketEshop.Core.Models
{
    public class Game: IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Game Title")]
        [Required(ErrorMessage = "Game Title is required")]
        public string Title { get; set; }

        [Display(Name = "Game Price")]
        [Range(0.5, 500.0, ErrorMessage = "Price must be at between 0.50 and 500.0.")]
        [Required(ErrorMessage = "Game Price is required")]
        public float Price { get; set; }

        [Display(Name = "Game Image URL")]
        [Required(ErrorMessage = "Game Image URL is required")]
        public string ImageUrl { get; set; }

        [Display(Name = "Game Quantity")]
        [Range(0, 100000, ErrorMessage = "Quantity must be at least 0.")]
        [Required(ErrorMessage = "Game Quantity is required")]
        public int Quantity { get; set; }

        [Display(Name = "Game Rating")]
        [Required(ErrorMessage = "Game Rating is required")]
        public Rating Rating { get; set; }

        [Display(Name = "Release Date")]
        [Required(ErrorMessage = "Release Date is required")]
        public DateTime Release_Date { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Display(Name = "Genres")]
        public List<GameGenre> GameGenreLink { get; set; }
    }
}
