using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CsvHelper.Configuration.Attributes;
using RocketEshop.Core.Enums;
using RocketEshop.Core.Interfaces;

namespace RocketEshop.Core.Models
{
    public class Game: IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Index(0)]
        public int Id { get; set; }

        [Display(Name = "Game Title")]
        [Required(ErrorMessage = "Game Title is required")]
        [Index(1)]
        public string Title { get; set; }

        [Display(Name = "Game Price")]
        [Range(0.5, 500.0, ErrorMessage = "Price must be at between 0.50 and 500.0.")]
        [Required(ErrorMessage = "Game Price is required")]
        [Index(2)]
        public float Price { get; set; }

        [Display(Name = "Game Image URL")]
        [Required(ErrorMessage = "Game Image URL is required")]
        [Index(3)]
        public string ImageUrl { get; set; }

        [Display(Name = "Game Quantity")]
        [Range(0, 100000, ErrorMessage = "Quantity must be at least 0.")]
        [Required(ErrorMessage = "Game Quantity is required")]
        [Index(4)]
        public int Quantity { get; set; }

        [Display(Name = "Game Rating")]
        [Required(ErrorMessage = "Game Rating is required")]
        [Index(5)]
        public Rating Rating { get; set; }

        [Display(Name = "Release Date")]
        [Required(ErrorMessage = "Release Date is required")]
        [Index(6)]
        public DateTime Release_Date { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required")]
        [Index(7)]
        public string Description { get; set; }

        [Display(Name = "Genres")]
        public List<GameGenre> GameGenreLink { get; set; }
    }
}
