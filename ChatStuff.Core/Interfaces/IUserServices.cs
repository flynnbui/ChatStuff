using ChatStuff.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace ChatStuff.Core.Interfaces
{
    public interface IUserServices
    {
        Task<IdentityResult> RegisterUserAsync(string username, string password);
        Task<SignInResult> LoginUserAsync(string username, string password);
        Task<ChatStuffUser> GetUserDetailsAsync(string userId);
    }
}
