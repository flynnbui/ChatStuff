using ChatStuff.Core.Entities;
using ChatStuff.Core.Interfaces;
using ChatStuff.Core.Result;

namespace ChatStuff.Core.Services;

public class FriendService : IFriendServices
{
    private readonly IFriendRepository _friendRepository;
    private readonly ITokenClaimsService _tokenClaimsService;

    public FriendService(IFriendRepository friendRepository, ITokenClaimsService tokenClaimsService)
    {
        _friendRepository = friendRepository;
        _tokenClaimsService = tokenClaimsService;
    }
    
    public async Task<OperationResult<string>> SendFriendRequestAsync(string sourceUserId, string targetUserId)
    {
        return await _friendRepository.SendFriendRequestAsync(sourceUserId, targetUserId).ConfigureAwait(false);
    }
        
    public async Task<OperationResult<string>> AcceptFriendRequestAsync(string sourceUserId, string targetUserId)
    {
        return await _friendRepository.AcceptFriendRequestAsync(sourceUserId, targetUserId).ConfigureAwait(false);
    }
        
    public async Task<OperationResult<string>> RemoveFriendAsync(string sourceUserId, string targetUserId)
    {
        return await _friendRepository.RemoveFriendAsync(sourceUserId, targetUserId).ConfigureAwait(false);
    }

    public async Task<OperationResult<ChatStuffUser>> FetchFriendAsync(string sourceUserName, string targetUserName)
    {
        return await _friendRepository.FetchFriendAsync(sourceUserName, targetUserName).ConfigureAwait(false);
    }

    public async Task<OperationResult<string>> BlockUserAsync(string sourceUserId, string targetUserId)
    {
        return await _friendRepository.BlockUserAsync(sourceUserId, targetUserId).ConfigureAwait(false);
    }
    
    public async Task<OperationResult<string>> UnblockUserAsync(string sourceUserId, string targetUserId)
    {
        return await _friendRepository.UnblockUserAsync(sourceUserId, targetUserId).ConfigureAwait(false);
    }
}