using INMAR.Service.Models;

namespace INMAR.Service.Interfaces
{
    public interface ICartItemService
    {
        Task<bool> AddOrUpdateCartItem(CartItem cartItem);
        Task<bool> DeleteCartItem(CartItem cartItem);
        Task<IEnumerable<CartItem>> GetCartItems();
        Task<IEnumerable<CartItem>> GetCartItem(long cartItemId);
        Task<IEnumerable<CartItem>> GetCartItemsByUserId(long userId);
        Task<IEnumerable<CartItem>> GetCartItemsByUserId(long userId, long cartItemId);
    }
}
