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
    public class ProductsControllerTest
    {
        [Fact]
        public async Task GetAllProducts_ReturnsOkResult_WithListOfProducts()
        {
            // Arrange
            var productList = new List<Product>();
            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(x => x.GetAllProduct()).ReturnsAsync(productList.AsQueryable());

            var controller = new ProductsController(productServiceMock.Object);

            // Act
            var result = await controller.GetAllProducts() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.IsType<List<Product>>(result.Value);
        }

        [Fact]
        public async Task GetProduct_ReturnsOkResult_WithProduct_WhenProductExists()
        {
            // Arrange
            long productId = 1;
            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(x => x.GetProduct(productId))
                              .ReturnsAsync(new Product { ProductId = productId });

            var controller = new ProductsController(productServiceMock.Object);

            // Act
            var result = await controller.GetProduct(productId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.IsType<Product>(result.Value);
            Assert.Equal(productId, ((Product)result.Value).ProductId);
        }

        [Fact]
        public async Task GetProduct_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            var product = new Product();
            long productId = 1;
            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(x => x.GetProduct(productId))
                              .ReturnsAsync(product);

            var controller = new ProductsController(productServiceMock.Object);

            // Act
            var result = await controller.GetProduct(productId) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task InsertOrUpdateProduct_ReturnsOkResult_WhenInsertOrUpdateSucceeds()
        {
            // Arrange
            var productToInsertOrUpdate = new Product();
            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(x => x.InsertOrUpdateProduct(productToInsertOrUpdate))
                              .ReturnsAsync(true);

            var controller = new ProductsController(productServiceMock.Object);

            // Act
            var result = await controller.InsertOrUpdateProduct(productToInsertOrUpdate) as OkResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task InsertOrUpdateProduct_ReturnsBadRequest_WhenInsertOrUpdateFails()
        {
            // Arrange
            var productToInsertOrUpdate = new Product();
            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(x => x.InsertOrUpdateProduct(productToInsertOrUpdate))
                              .ReturnsAsync(false);

            var controller = new ProductsController(productServiceMock.Object);

            // Act
            var result = await controller.InsertOrUpdateProduct(productToInsertOrUpdate) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Failed to insert or update the product.", result.Value);
        }

        [Fact]
        public async Task DeleteProduct_ReturnsOkResult_WhenDeleteSucceeds()
        {
            // Arrange
            long productId = 1;
            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(x => x.DeleteProduct(productId))
                              .ReturnsAsync(true);

            var controller = new ProductsController(productServiceMock.Object);

            // Act
            var result = await controller.DeleteProduct(productId) as OkResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task DeleteProduct_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            long productId = 1;
            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(x => x.DeleteProduct(productId))
                              .ReturnsAsync(false);

            var controller = new ProductsController(productServiceMock.Object);

            // Act
            var result = await controller.DeleteProduct(productId) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
        }
    }
}
