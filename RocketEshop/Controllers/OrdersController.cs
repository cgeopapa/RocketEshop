using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RocketEshop.Core.Interfaces;

namespace RocketEshop.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IGamesService _gamesService;
        private readonly IApplicationUserService _applicationUserService;
        private readonly IOrdersService _ordersService;

        public OrdersController(IGamesService gamesService, IApplicationUserService applicationUserService, IOrdersService ordersService)
        {
            _gamesService = gamesService;
            _applicationUserService = applicationUserService;
            _ordersService = ordersService;
        }

        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);

            var orders = await _ordersService.GetOrdersByUserIdAndRoleAsync(userId,userRole);
            return View(orders);
        }
    }
}       