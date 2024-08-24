using ChatStuff.Core.Entities;
using ChatStuff.Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ChatStuff.Core.Services
{
    public class UserService : IUserServices
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<ChatStuffUser> GetUserDetailsAsync(string userId)
        {
            return _userRepository.GetUserAsync(userId);
        }

        public Task<SignInResult> LoginUserAsync(string username, string password)
        {
            return _userRepository.LoginAsync(username, password);
        }

        public Task<IdentityResult> RegisterUserAsync(string username, string password)
        {
            return _userRepository.RegisterAsync(username, password);
        }
    }
}
