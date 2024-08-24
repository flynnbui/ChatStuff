using ChatStuff.Core.Entities;
using ChatStuff.Core.Result;
using Microsoft.AspNetCore.Identity;

namespace ChatStuff.Core.Interfaces
{
    public interface IUserServices
    {
        Task<OperationResult<string>> RegisterUserAsync(string userName, string password);
        Task<OperationResult<string>> LoginUserAsync(string userName, string password);
        Task<ChatStuffUser> GetUserDetailsAsync(string userId);
    }
}
