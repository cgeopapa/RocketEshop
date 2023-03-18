using Microsoft.AspNetCore.Mvc;
using RocketEsgop.Infrastructure.Core.Models;
using RocketEshop.Core.Models;

namespace RocketEsgop.Infrastructure.Data.ViewComponents
{
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly ShoppingCart _shoppingCart;
        public ShoppingCartSummary(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public Task<IViewComponentResult> InvokeAsync()
        {
            var items = _shoppingCart.GetShoppingCartItems();

            return Task.FromResult<IViewComponentResult>(View("default" , items.Count));            
        }
    }
}
