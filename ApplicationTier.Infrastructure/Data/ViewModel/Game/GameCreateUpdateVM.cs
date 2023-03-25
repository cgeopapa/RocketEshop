﻿using RocketEshop.Core.Enums;
using RocketEshop.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace RocketEshop.Infrastructure.Data.ViewModel
{
    public class GameCreateUpdateVM
    {
        public int? Id { get; set; }

        [Display(Name = "Game Title")]
        [Required(ErrorMessage = "Game Title is required")]
        public string Title { get; set; }

        [Display(Name = "Game Price")]
        [Range(0.5, 500.0, ErrorMessage = "Price must be at between 0.50 and 500.0.")]
        [Required(ErrorMessage = "Game Price is required")]
        public string Price { get; set; }

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
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Display(Name = "Genres")]
        public List<int> Genres { get; set; }


        public GameCreateUpdateVM()
        {
        }

        public GameCreateUpdateVM(Game game)
        {
            Id = game.Id;
            Title = game.Title;
            Price = game.Price.ToString();
            ImageUrl = game.ImageUrl;
            Quantity = game.Quantity;
            Rating = game.Rating;
            ReleaseDate = game.Release_Date;
            Description = game.Description;

            Genres = new List<int>();
            foreach (GameGenre genre in game.GameGenreLink)
            {
                Genres.Add(genre.Genre.Id);
            }
        }
    }
}