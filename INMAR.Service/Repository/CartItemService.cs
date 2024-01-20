using INMAR.Service.DdContextConfiguration;
using INMAR.Service.Interfaces;
using INMAR.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace INMAR.Service.Repository
{
    public class CartItemService : ICartItemService
    {
        private readonly ApplicationDBContext dbContext;

        public CartItemService(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> AddOrUpdateCartItem(CartItem cartItem)
        {
            var existingCartItem = await dbContext.cartItems.FirstOrDefaultAsync(ci => ci.ProductId == cartItem.ProductId && ci.UserId == cartItem.UserId);

            if (existingCartItem == null)
            {
                dbContext.cartItems.Add(cartItem);
            }
            else
            {
                existingCartItem.Quantity += cartItem.Quantity;
            }

            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCartItem(CartItem cartItem)
        {
            var existingCartItem = await dbContext.cartItems.FirstOrDefaultAsync(ci => ci.ProductId == cartItem.ProductId && ci.UserId == cartItem.UserId);

            if (existingCartItem != null)
            {
                dbContext.cartItems.Remove(existingCartItem);
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<CartItem>> GetCartItems()
        {
            var cartItems = await dbContext.cartItems.Where(x => x.Quantity > 0).ToListAsync();
            return cartItems;
        }

        public async Task<IEnumerable<CartItem>> GetCartItem(long cartItemId)
        {
            var cartItem = await dbContext.cartItems
                .Where(ci => ci.CartItemId == cartItemId && ci.Quantity > 0)
                .ToListAsync();

            return cartItem;
        }

        public async Task<IEnumerable<CartItem>> GetCartItemsByUserId(long userId)
        {
            var cartItems = await dbContext.cartItems.Where(ci => ci.UserId == userId &&  ci.Quantity > 0).ToListAsync();

            return cartItems;
        }

        public async Task<IEnumerable<CartItem>> GetCartItemsByUserId(long userId, long cartItemId)
        {
            var cartItems = await dbContext.cartItems.Where(ci => ci.UserId == userId && ci.CartItemId == cartItemId && ci.Quantity > 0)
                .ToListAsync();

            return cartItems;
        }
    }

}
