using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RocketEshop.Core.Interfaces;
using RocketEshop.Infrastructure.Data.ViewModel;

namespace RocketEshop.Controllers;

public class ShoppingCartController: Controller
{
    private readonly IGamesService _gamesService;
    private readonly IApplicationUserService _applicationUserService;
    private readonly IOrdersService _ordersService;

    public ShoppingCartController(IGamesService gamesService, IApplicationUserService applicationUserService, IOrdersService ordersService)
    {
        _gamesService = gamesService;
        _applicationUserService = applicationUserService;
        _ordersService = ordersService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        string userId = GetUserId();
            
        var items = _applicationUserService.GetUserShoppingCart(userId);
        decimal total = _applicationUserService.GetShoppingCartTotal(userId);
            
        var response = new ShoppingCartVM(items, total);

        return View(response);
    }
    
    public async Task<IActionResult> AddItemToShoppingCart(int id)
    {
        string userId = GetUserId();

        var item = await _gamesService.GetByIdAsync(id);
        if (item == null)
        {
            return NotFound();
        }
        _applicationUserService.AddItemToUserCart(item, userId);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
    {
        string userId = GetUserId();

        var item = await _gamesService.GetByIdAsync(id);
        if (item == null)
        {
            return NotFound();
        }
        _applicationUserService.RemoveItemFromUserCart(item, userId);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> CompleteOrder()
    {
        string userId = GetUserId();
        string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);

        var items = _applicationUserService.GetUserShoppingCart(userId);
        if (items.Count == 0)
        {
            return View("EmptyCart");
        }
        else
        {

            await _ordersService.StoreOrderAsync(items, userId, userEmailAddress);
            _applicationUserService.ClearUserShoppingCart(userId);
            //if (items != null)
            //{
            //    return View("EmptyCart");

            //}
            return View("OrderCompleted");
        }
    }

        // PRIVATE - UTILS

        private string GetUserId()
    {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return userId;
    }
}