using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using RocketEshop.Core.Interfaces;

namespace RocketEshop.Core.Models
{
    public class ApplicationUser : IdentityUser, IEntity<string>
    {
        [Display(Name = "Full name")]
        [StringLength(255)]
        public string FullName { get; set; } = "";

        [Display(Name = "Shopping Cart")]
        public List<ShoppingCartItem> ShoppingCart { get; set; } = new List<ShoppingCartItem>();
    }
}
