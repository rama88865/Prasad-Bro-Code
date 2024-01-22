using INMAR.Service.Controllers;
using INMAR.Service.Interfaces;
using INMAR.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INMAR.Service.Test
{
    public class CartItemsControllerTest
    {
        [Fact]
        public async Task AddOrUpdateCartItem_ReturnsOkResult_WhenAddOrUpdateSucceeds()
        {
            // Arrange
            var cartItemToInsertOrUpdate = new CartItem();
            var cartItemServiceMock = new Mock<ICartItemService>();
            cartItemServiceMock.Setup(x => x.AddOrUpdateCartItem(cartItemToInsertOrUpdate))
                               .ReturnsAsync(true);

            var controller = new CartItemsController(cartItemServiceMock.Object);

            // Act
            var result = await controller.AddOrUpdateCartItem(cartItemToInsertOrUpdate) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.True((bool)result.Value);
        }

        [Fact]
        public async Task AddOrUpdateCartItem_ReturnsOkResult_WhenAddOrUpdateFails()
        {
            // Arrange
            var cartItemToInsertOrUpdate = new CartItem();
            var cartItemServiceMock = new Mock<ICartItemService>();
            cartItemServiceMock.Setup(x => x.AddOrUpdateCartItem(cartItemToInsertOrUpdate))
                               .ReturnsAsync(false);

            var controller = new CartItemsController(cartItemServiceMock.Object);

            // Act
            var result = await controller.AddOrUpdateCartItem(cartItemToInsertOrUpdate) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.False((bool)result.Value);
        }

        [Fact]
        public async Task DeleteCartItem_ReturnsOkResult_WhenDeleteSucceeds()
        {
            // Arrange
            var cartItemToDelete = new CartItem();
            var cartItemServiceMock = new Mock<ICartItemService>();
            cartItemServiceMock.Setup(x => x.DeleteCartItem(cartItemToDelete))
                               .ReturnsAsync(true);

            var controller = new CartItemsController(cartItemServiceMock.Object);

            // Act
            var result = await controller.DeleteCartItem(cartItemToDelete) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.True((bool)result.Value);
        }

        [Fact]
        public async Task DeleteCartItem_ReturnsOkResult_WhenDeleteFails()
        {
            // Arrange
            var cartItemToDelete = new CartItem();
            var cartItemServiceMock = new Mock<ICartItemService>();
            cartItemServiceMock.Setup(x => x.DeleteCartItem(cartItemToDelete))
                               .ReturnsAsync(false);

            var controller = new CartItemsController(cartItemServiceMock.Object);

            // Act
            var result = await controller.DeleteCartItem(cartItemToDelete) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.False((bool)result.Value);
        }

        [Fact]
        public async Task GetCartItems_ReturnsOkResult_WithListOfCartItems()
        {
            // Arrange
            var cartItemList = new List<CartItem>();
            var cartItemServiceMock = new Mock<ICartItemService>();
            cartItemServiceMock.Setup(x => x.GetCartItems()).ReturnsAsync(cartItemList);

            var controller = new CartItemsController(cartItemServiceMock.Object);

            // Act
            var result = await controller.GetCartItems() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.IsType<List<CartItem>>(result.Value);
            Assert.Equal(cartItemList, (List<CartItem>)result.Value);
        }

        [Fact]
        public async Task GetCartItem_ReturnsOkResult_WithCartItem_WhenCartItemExists()
        {
            // Arrange
            long cartItemId = 1;
            var cartItem = new List<CartItem>();
            var cartItemServiceMock = new Mock<ICartItemService>();
            cartItemServiceMock.Setup(x => x.GetCartItem(cartItemId))
                               .ReturnsAsync(cartItem);

            var controller = new CartItemsController(cartItemServiceMock.Object);

            // Act
            var result = await controller.GetCartItem(cartItemId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.IsType<CartItem>(result.Value);
            Assert.Equal(cartItemId, ((CartItem)result.Value).CartItemId);
        }

        [Fact]
        public async Task GetCartItem_ReturnsNotFound_WhenCartItemDoesNotExist()
        {
            // Arrange
            long cartItemId = 1;
            var cartItem = new List<CartItem>();
            var cartItemServiceMock = new Mock<ICartItemService>();
            cartItemServiceMock.Setup(x => x.GetCartItem(cartItemId))
                               .ReturnsAsync(cartItem);

            var controller = new CartItemsController(cartItemServiceMock.Object);

            // Act
            var result = await controller.GetCartItem(cartItemId) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task GetCartItemsByUserId_ReturnsOkResult_WithListOfCartItems()
        {
            // Arrange
            long userId = 1;
            var cartItemList = new List<CartItem>();
            var cartItemServiceMock = new Mock<ICartItemService>();
            cartItemServiceMock.Setup(x => x.GetCartItemsByUserId(userId))
                               .ReturnsAsync(cartItemList);

            var controller = new CartItemsController(cartItemServiceMock.Object);

            // Act
            var result = await controller.GetCartItemsByUserId(userId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.IsType<List<CartItem>>(result.Value);
        }

        [Fact]
        public async Task GetCartItemsByUserIdAndCartItemId_ReturnsOkResult_WithListOfCartItems()
        {
            // Arrange
            long userId = 1;
            long cartItemId = 1;
            var cartItemList = new List<CartItem>();
            var cartItemServiceMock = new Mock<ICartItemService>();
            cartItemServiceMock.Setup(x => x.GetCartItemsByUserId(userId, cartItemId))
                               .ReturnsAsync(cartItemList);

            var controller = new CartItemsController(cartItemServiceMock.Object);

            // Act
            var result = await controller.GetCartItemsByUserId(userId, cartItemId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.IsType<List<CartItem>>(result.Value);
        }
    }
}
