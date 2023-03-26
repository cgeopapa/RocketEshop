using System.Globalization;
using RocketEshop.Core.Domain;
using RocketEshop.Core.Models;

namespace RocketEshop.Infrastructure.Data.ViewModel
{
    public class ShoppingCartVM
    {
        public List<ShoppingCartItemVM> ShoppingCart { get; set; }
        public decimal ShoppingCartTotal { get; set; }

        public ShoppingCartVM(List<ShoppingCartItem> items, decimal shoppingCartTotal)
        {
            this.ShoppingCart = new List<ShoppingCartItemVM>();
            foreach (ShoppingCartItem item in items)
            {
                ShoppingCart.Add(new ShoppingCartItemVM()
                {
                    Game = new GameVM(item.Game),
                    Amount = item.Amount
                });
            }
            ShoppingCartTotal = shoppingCartTotal;
            if (Equals(CultureInfo.CurrentUICulture, new CultureInfo("en-US")))
            {
                ShoppingCartTotal *= ConvertionRate.USD;
            }
        }
    }
}
