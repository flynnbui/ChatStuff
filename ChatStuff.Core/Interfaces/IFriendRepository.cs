using ChatStuff.Core.Entities;
using ChatStuff.Core.Result;

namespace ChatStuff.Core.Interfaces;

public interface IFriendRepository
{
    Task<OperationResult<string>> SendFriendRequestAsync(string sourceUserId, string targetUserId);
    Task<OperationResult<string>> AcceptFriendRequestAsync(string sourceUserId, string targetUserId);
    Task<OperationResult<string>> RemoveFriendAsync(string sourceUserId, string targetUserId);
    Task<OperationResult<ChatStuffUser>> FetchFriendAsync(string sourceUserName, string targetUserName);
    Task<OperationResult<string>> BlockUserAsync(string sourceUserId, string targetUserId);
    Task<OperationResult<string>> UnblockUserAsync(string sourceUserName, string targetUserName);
}