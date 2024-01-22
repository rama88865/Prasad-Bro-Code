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
    public class AccountControllerTests
    {
        [Fact]
        public async Task Authenticate_ReturnsUnauthorized_WhenAuthenticationFails()
        {
            // Arrange
            var authenticationServiceMock = new Mock<IAuthenticationService>();
            var appUser = new AuthResponse();
            authenticationServiceMock.Setup(x => x.Authenticate(It.IsAny<string>(), It.IsAny<string>()))
                                     .ReturnsAsync((appUser)); 

            var controller = new AccountController(authenticationServiceMock.Object);

            // Act
            var userModel = new UserModel { Username = "testuser", phone = "1234567890" };
            var result = await controller.Authenticate(userModel) as UnauthorizedResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(401, result.StatusCode);
        }

        [Fact]
        public async Task Authenticate_ReturnsOkObjectResult_WhenAuthenticationSucceeds()
        {
            // Arrange
            var authenticatedUser = new AuthResponse();

            var authenticationServiceMock = new Mock<IAuthenticationService>();
            authenticationServiceMock.Setup(x => x.Authenticate(It.IsAny<string>(), It.IsAny<string>()))
                                     .ReturnsAsync(authenticatedUser);

            var controller = new AccountController(authenticationServiceMock.Object);

            // Act
            var userModel = new UserModel { Username = "testuser", phone = "1234567890" };
            var result = await controller.Authenticate(userModel) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Same(authenticatedUser, result.Value); 
        }
    }
}
