using ChatStuff.Core.Entities;
using ChatStuff.Core.Interfaces;
using ChatStuff.Core.Result;
using Microsoft.AspNetCore.Identity;

namespace ChatStuff.Core.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenClaimsService _tokenClaimsService;
        private readonly UserManager<ChatStuffUser> _userManager;

        public UserServices(IUserRepository userRepository, ITokenClaimsService tokenClaimsService, UserManager<ChatStuffUser> userManager)
        {
            _userRepository = userRepository;
            _tokenClaimsService = tokenClaimsService;
            _userManager = userManager;
        }

        public async Task<OperationResult<string>> RegisterUserAsync(string userName, string password)
        {
            var result = await _userRepository.RegisterAsync(userName, password).ConfigureAwait(false);
            Console.WriteLine(result);
            if (result.Succeeded)
            {
                var token = await _tokenClaimsService.GenerateJwtToken(userName);
                return OperationResult<string>.Success(token);
            }
            return OperationResult<string>.Failure("User registration failed");
        }

        public async Task<OperationResult<string>> LoginUserAsync(string userName, string password)
        {
            var result = await _userRepository.LoginAsync(userName, password).ConfigureAwait(false);
            if (result == SignInResult.Success)
            {
                var token = await _tokenClaimsService.GenerateJwtToken(userName);
                return OperationResult<string>.Success(token);
            }
            return OperationResult<string>.Failure("User login failed");
        }

        public async Task<ChatStuffUser> GetUserDetailsAsync(string userId)
        {
            return await _userRepository.GetUserAsync(userId).ConfigureAwait(false);
        }
    }
}
