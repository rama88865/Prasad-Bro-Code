using INMAR.Service.Interfaces;
using INMAR.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace INMAR.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await userService.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(long userId)
        {
            try
            {
                var user = await userService.GetUser(userId);

                if (user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return NotFound($"User with ID {userId} not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateUser([FromBody] Users user)
        {
            try
            {
                var result = await userService.InsertOrUpdateUser(user);
                if (result)
                {
                    return Ok("User inserted or updated successfully.");
                }
                else
                {
                    return BadRequest("Failed to insert or update user.");
                }
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, ex.Message.ToString());
            }
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(long userId)
        {
            try
            {
                var result = await userService.DeleteUser(userId);
                if (result)
                {
                    return Ok($"User with ID {userId} deleted successfully.");
                }
                else
                {
                    return NotFound($"User with ID {userId} not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }
    }
}
