using Microsoft.EntityFrameworkCore;
using RocketEshop.Core.Interfaces;
using RocketEshop.Core.Models;
using RocketEshop.Infrastructure.Repositories;

namespace RocketEshop.Infrastructure.Services;

public class ApplicationUserService: EntityBaseRepository<ApplicationUser, string>, IApplicationUserService
{
    private readonly AppDbContext context;
    
    public ApplicationUserService(AppDbContext context) : base(context)
    {
        this.context = context;
    }

    public List<ShoppingCartItem> GetUserShoppingCart(string userId)
    {
        return context.Users.Include(user => user.ShoppingCart).ThenInclude(cart => cart.Game)
            .First(user => user.Id == userId).ShoppingCart;
    }

    public void AddItemToUserCart(Game game, string userId)
    {
        var cart = GetUserShoppingCart(userId);
        var shoppingCartItem = cart.FirstOrDefault(item => item.Game.Id == game.Id);
        
        if (shoppingCartItem == null)
        {
            shoppingCartItem = new ShoppingCartItem()
            {
                Game = game,
                Amount = 1
            };
            cart.Add(shoppingCartItem);
        }
        else
        {
            shoppingCartItem.Amount++;
        }
        game.Quantity--;
        context.SaveChanges();
    }

    public void RemoveItemFromUserCart(Game game, string userId)
    {
        var cart = GetUserShoppingCart(userId);
        var shoppingCartItem = cart.FirstOrDefault(item => item.Game.Id == game.Id);
        
        if (shoppingCartItem != null)
        {
            if (shoppingCartItem.Amount > 1)
            {
                shoppingCartItem.Amount--;
            }
            else
            {
                cart.Remove(shoppingCartItem);
            }
        }
        game.Quantity++;
        context.SaveChanges();
    }

    public void ClearUserShoppingCart(string userId)
    {
        var cart = GetUserShoppingCart(userId);
        cart.Clear();
        context.SaveChanges();
    }

    public decimal GetShoppingCartTotal(string userId)
    {
        var cart = GetUserShoppingCart(userId);
        return cart.Select(item => item.Game.Price * item.Amount).Sum();
    }

    public int GetShoppingCartItemCount(string? userId)
    {
        return userId == null ? 0 : context.Users.Include(user => user.ShoppingCart).First(user => user.Id == userId).ShoppingCart.Count;
    }
}
