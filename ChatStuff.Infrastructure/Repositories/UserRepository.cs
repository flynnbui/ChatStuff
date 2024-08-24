using ChatStuff.Core.Entities;
using ChatStuff.Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ChatStuff.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ChatStuffUser> _userManager;
        private readonly SignInManager<ChatStuffUser> _signInManager;

        public UserRepository(UserManager<ChatStuffUser> userManager, SignInManager<ChatStuffUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> RegisterAsync(string username, string password)
        {
            return await _userManager.CreateAsync (new ChatStuffUser() { UserName = username}, password).ConfigureAwait(false);
        }

        public async Task<SignInResult> LoginAsync(string username, string password)
        {
            return await _signInManager.PasswordSignInAsync(username, password,isPersistent: false,lockoutOnFailure: false).ConfigureAwait(false);
        }

        public async Task<ChatStuffUser> GetUserAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId).ConfigureAwait(false);
        }
    }
}
