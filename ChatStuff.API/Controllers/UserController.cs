using ChatStuff.Core.Entities;
using ChatStuff.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ChatStuff.Core.DTOs;

namespace ChatStuff.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices,
            ITokenClaimsService tokenClaimsService)
        {
            _userServices = userServices;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDTO user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userServices.LoginUserAsync(user.UserName, user.Password).ConfigureAwait(false);
            if (!result.Succeeded)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.Data);
        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(UserDTO user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userServices.RegisterUserAsync(user.UserName, user.Password).ConfigureAwait(false);
            if (!result.Succeeded)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.Data);
        }

        [HttpGet()]
        public async Task<ActionResult<ChatStuffUser>> GetUserDetails(string userId)
        {
            var user = await _userServices.GetUserDetailsAsync(userId).ConfigureAwait(false);
            if (user == null)
            {
                return NotFound("User not found");
            }
            return Ok(user);
        }
    }
}
