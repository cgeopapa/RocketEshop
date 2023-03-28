using Microsoft.AspNetCore.Mvc;
using RocketEshop.Core.Interfaces;
using System.Security.Claims;

namespace RocketEshop.Infrastructure.Data.ViewComponents
{
    public class ShoppingCartIcon : ViewComponent
    {

        private readonly IApplicationUserService _userService;

        public ShoppingCartIcon(IApplicationUserService userService)
        {
            _userService = userService;
        }

        public IViewComponentResult Invoke()
        {
            var UserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            int item = _userService.GetShoppingCartItemCount(UserId);
            return View(item);
        }

    }
}
