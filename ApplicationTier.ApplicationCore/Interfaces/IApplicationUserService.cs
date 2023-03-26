using RocketEshop.Core.Models;

namespace RocketEshop.Core.Interfaces;

public interface IApplicationUserService: IRepository<ApplicationUser, string>
{
    public List<ShoppingCartItem> GetUserShoppingCart(string id);

    public void AddItemToUserCart(Game game, string userId);

    public void RemoveItemFromUserCart(Game game, string userId);

    public void ClearUserShoppingCart(string userId);

    public decimal GetShoppingCartTotal(string userId);
}