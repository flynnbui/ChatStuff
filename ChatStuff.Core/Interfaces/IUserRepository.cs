using ChatStuff.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace ChatStuff.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<IdentityResult> RegisterAsync(string username, string password);
        Task<SignInResult> LoginAsync(string username, string password);
        Task<ChatStuffUser> GetUserAsync(string userId);
    }
}
