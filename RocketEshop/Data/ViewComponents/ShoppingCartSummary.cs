﻿using Microsoft.AspNetCore.Mvc;
using RocketEshop.Core.Models;

namespace RocketEshop.Data.ViewComponents
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
