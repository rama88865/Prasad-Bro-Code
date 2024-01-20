using INMAR.Service.Interfaces;
using INMAR.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace INMAR.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;
        public AccountController(IAuthenticationService _authenticationService)
        {
            this.authenticationService = _authenticationService;
        }

        [HttpPost]
        [Route("Authenticate")]
        public async Task<IActionResult> Authenticate(UserModel model)
        {
            var appuser = await authenticationService.Authenticate(model.Username, model.phone);
          
            if (appuser == null)
                return Unauthorized();

            return Ok(appuser);
        }
    }
}
