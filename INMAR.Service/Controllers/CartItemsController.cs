using INMAR.Service.Interfaces;
using INMAR.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace INMAR.Service.Controllers
{
    [Route("api/cartItems")]
    [ApiController]
    [Authorize]
    public class CartItemsController : ControllerBase
    {
        private readonly ICartItemService cartItemService;

        public CartItemsController(ICartItemService cartItemService)
        {
            this.cartItemService = cartItemService;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateCartItem([FromBody] CartItem cartItem)
        {
            var result = await cartItemService.AddOrUpdateCartItem(cartItem);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCartItem([FromBody] CartItem cartItem)
        {
            var result = await cartItemService.DeleteCartItem(cartItem);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetCartItems()
        {
            var cartItems = await cartItemService.GetCartItems();
            return Ok(cartItems);
        }

        [HttpGet("{cartItemId}")]
        public async Task<IActionResult> GetCartItem(long cartItemId)
        {
            var cartItem = await cartItemService.GetCartItem(cartItemId);
            return Ok(cartItem);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetCartItemsByUserId(long userId)
        {
            var cartItems = await cartItemService.GetCartItemsByUserId(userId);
            return Ok(cartItems);
        }

        [HttpGet("user/{userId}/{cartItemId}")]
        public async Task<IActionResult> GetCartItemsByUserId(long userId, long cartItemId)
        {
            var cartItems = await cartItemService.GetCartItemsByUserId(userId, cartItemId);

            return Ok(cartItems);
        }
    }

}
