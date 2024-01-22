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
    public class UsersControllerTest
    {
        [Fact]
        public async Task GetAllUsers_ReturnsOkResult_WithListOfUsers()
        {
            // Arrange
            var userlist = new List<Users>();
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(x => x.GetAllUsers()).ReturnsAsync(userlist.AsQueryable());

            var controller = new UsersController(userServiceMock.Object);

            // Act
            var result = await controller.GetAllUsers() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.IsType<List<Users>>(result.Value);
        }

        [Fact]
        public async Task GetUser_ReturnsOkResult_WithUser_WhenUserExists()
        {
            // Arrange
            long userId = 1;
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(x => x.GetUser(userId)).ReturnsAsync(new Users { UserId = userId });

            var controller = new UsersController(userServiceMock.Object);

            // Act
            var result = await controller.GetUser(userId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.IsType<Users>(result.Value);
            Assert.Equal(userId, ((Users)result.Value).UserId);
        }

        [Fact]
        public async Task GetUser_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            long userId = 1;
            Users userResponce = new Users();
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(x => x.GetUser(userId)).ReturnsAsync((userResponce));

            var controller = new UsersController(userServiceMock.Object);

            // Act
            var result = await controller.GetUser(userId) as NotFoundObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
            Assert.Equal($"User with ID {userId} not found.", result.Value);
        }

        [Fact]
        public async Task InsertOrUpdateUser_ReturnsOkResult_WhenInsertOrUpdateSucceeds()
        {
            // Arrange
            var userToInsertOrUpdate = new Users();
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(x => x.InsertOrUpdateUser(userToInsertOrUpdate))
                           .ReturnsAsync(true);

            var controller = new UsersController(userServiceMock.Object);

            // Act
            var result = await controller.InsertOrUpdateUser(userToInsertOrUpdate) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal("User inserted or updated successfully.", result.Value);
        }

        [Fact]
        public async Task InsertOrUpdateUser_ReturnsBadRequest_WhenInsertOrUpdateFails()
        {
            // Arrange
            var userToInsertOrUpdate = new Users();
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(x => x.InsertOrUpdateUser(userToInsertOrUpdate))
                           .ReturnsAsync(false);

            var controller = new UsersController(userServiceMock.Object);

            // Act
            var result = await controller.InsertOrUpdateUser(userToInsertOrUpdate) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Failed to insert or update user.", result.Value);
        }

        [Fact]
        public async Task DeleteUser_ReturnsOkResult_WhenDeleteSucceeds()
        {
            // Arrange
            long userId = 1;
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(x => x.DeleteUser(userId))
                           .ReturnsAsync(true);

            var controller = new UsersController(userServiceMock.Object);

            // Act
            var result = await controller.DeleteUser(userId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal($"User with ID {userId} deleted successfully.", result.Value);
        }

        [Fact]
        public async Task DeleteUser_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            long userId = 1;
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(x => x.DeleteUser(userId))
                           .ReturnsAsync(false);

            var controller = new UsersController(userServiceMock.Object);

            // Act
            var result = await controller.DeleteUser(userId) as NotFoundObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
            Assert.Equal($"User with ID {userId} not found.", result.Value);
        }
    }
}
