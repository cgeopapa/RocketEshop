using System.ComponentModel.DataAnnotations;

namespace RocketEshop.Infrastructure.Data.ViewModel;

public class ShoppingCartItemVM
{
    [Display(Name = "Game")]
    public GameVM Game { get; set; }
    
    [Display(Name = "Amount")]
    public int Amount { get; set; }
}