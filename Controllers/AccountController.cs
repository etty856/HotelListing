using HotelListing.Contracts;
using HotelListing.Models.User;
using HotelListing.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthManager _authManager;

        public AccountController(IAuthManager authManager)
        {
            _authManager = authManager;
        }

        //POST: api/Account/register
        //post is more secured way for accessing the database
        //Registering should be secured and that the data would be hidden
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Register([FromBody] ApiUserDto userDto)
        {
            var errors = await _authManager.Register(userDto);
            if (errors.Any())
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            return Ok();
        }

        //POST: api/Account/login
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
        {
            var isValidUser = await _authManager.Login(loginDto);
            if (!isValidUser)
            {
                return Unauthorized(); 
            }
            return Ok();
        }
    }
}

