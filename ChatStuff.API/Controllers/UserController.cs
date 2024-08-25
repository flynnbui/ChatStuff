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
        private readonly IFriendServices _friendServices;

        public UserController(IUserServices userServices, IFriendServices friendServices,
            ITokenClaimsService tokenClaimsService)
        {
            _userServices = userServices;
            _friendServices = friendServices;
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

        [HttpPost("sendFriendRequest")]
        public async Task<ActionResult<string>> SendFriendRequest(string sourceUser, string targetUser)
        {
            var result = await _friendServices.SendFriendRequestAsync(sourceUser, targetUser).ConfigureAwait(false);
            if (!result.Succeeded)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.Data);
        }
        
        [HttpPut("acceptFriendRequest")]
        public async Task<ActionResult<string>> AcceptFriendRequest(string sourceUserName, string targetUserName)
        {
            var result = await _friendServices.AcceptFriendRequestAsync(sourceUserName, targetUserName).ConfigureAwait(false);
            if (!result.Succeeded)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.Data);
        }
        
        [HttpDelete("removeFriend")]
        public async Task<ActionResult<string>> RemoveFriend(string sourceUserName, string targetUserName)
        {
            var result = await _friendServices.RemoveFriendAsync(sourceUserName, targetUserName).ConfigureAwait(false);
            if (!result.Succeeded)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.Data);
        }

        [HttpGet("fetchFriend")]
        public async Task<ActionResult<ChatStuffUser>> FetchFriend(string sourceUserName, string targetUserName)
        {
            var result = await _friendServices.FetchFriendAsync(sourceUserName, targetUserName).ConfigureAwait(false);
            if (!result.Succeeded)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.Data);
        }

        [HttpPost("createBlock")]
        public async Task<ActionResult<string>> BlockUser(string sourceUserName, string targetUserName)
        {
            var result = await _friendServices.BlockUserAsync(sourceUserName, targetUserName).ConfigureAwait(false);
            if (!result.Succeeded)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.Data);
        }
        
        [HttpDelete("removeBlock")]
        public async Task<ActionResult<string>> RemoveBlockUser(string sourceUserName, string targetUserName)
        {
            var result = await _friendServices.UnblockUserAsync(sourceUserName, targetUserName).ConfigureAwait(false);
            if (!result.Succeeded)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.Data);
        }
    }
}
