using RocketEshop.Infrastructure.Core.Models;
using RocketEshop.Infrastructure.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;
using RocketEshop.Core.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace RocketEshop.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IGamesService _gamesService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrdersService _ordersService;

        public OrdersController(IGamesService gamesService, ShoppingCart shoppingCart, IOrdersService ordersService)
        {
            _gamesService = gamesService;
            _shoppingCart = shoppingCart;
            _ordersService = ordersService;

        }

        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);

            var orders = await _ordersService.GetOrdersByUserIdAndRoleAsync(userId,userRole);
            return View(orders);
        }

        public IActionResult ShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            if (items.Count <= 0) return RedirectToAction("Index" , "Home");

            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(response);
        }

        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            var item = await _gamesService.GetByIdAsync(id);
            item.Quantity--;
            if (item != null || item.Quantity !<= 0)
            {
                _shoppingCart.AddItemToCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = await _gamesService.GetByIdAsync(id);
            item.Quantity++;
            if (item != null || item.Quantity! <= 0)
            {
                _shoppingCart.RemoveItemFromCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> CompleteOrder()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);

            await _ordersService.StoreOrderAsync(items, userId, userEmailAddress);
            await _shoppingCart.ClearShoppingCartAsync();

            return View("OrderCompleted");
        }

    }
}       