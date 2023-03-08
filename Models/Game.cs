using RocketEshop.Data.Base;
using RocketEshop.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RocketEshop.Models
{
    public class Game : IEntityBase
    {
        [Key]
        public int GameId { get; set; }

        [Display(Name = "Game Title")]
        [Required(ErrorMessage = "Game Title is required")]
        public string Title { get; set; }

        [Display(Name = "Game Price")]
        [Required(ErrorMessage = "Game Price is required")]
        public float Price { get; set; }

        [Display(Name = "Game Image URL")]
        [Required(ErrorMessage = "Game Image URL is required")]
        public string ImageUrl { get; set; }

        [Display(Name = "Game Quantity")]
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
    }
}
