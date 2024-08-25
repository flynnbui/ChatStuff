using ChatStuff.Core.Entities;
using ChatStuff.Core.Interfaces;
using ChatStuff.Core.Result;
using ChatStuff.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ChatStuff.Infrastructure.Repositories;

public class FriendRepository : IFriendRepository
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ChatStuffUser> _userManager;
    private readonly SignInManager<ChatStuffUser> _signInManager;

    public FriendRepository(UserManager<ChatStuffUser> userManager, SignInManager<ChatStuffUser> signInManager,
        ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _userManager = userManager;
        _signInManager = signInManager;
    }
    
    public async Task<OperationResult<string>> SendFriendRequestAsync(string sourceUserName, string targetUserName)
        {
            // Find the source user
            var sourceUser = await _userManager.FindByNameAsync(sourceUserName).ConfigureAwait(false);

            // Find the target user
            var targetUser = await _userManager.FindByNameAsync(targetUserName).ConfigureAwait(false);

            if (sourceUser == null || targetUser == null)
            {
                return OperationResult<string>.Failure("Select valid users only");
            }
            
            // Check if the source user already has a pending request to the target user
            var existRequest = await _context.FriendRequests
                .FirstOrDefaultAsync(fr => fr.SourceUserName == sourceUserName && fr.TargetUserName == targetUserName);

            if (existRequest != null)
            {
                return OperationResult<string>.Failure("Friend request already exists");
            }
            
            var existSentRequest = await _context.FriendRequests
                .FirstOrDefaultAsync(fr => fr.SourceUserName == targetUserName && fr.TargetUserName == sourceUserName);

            if (existSentRequest != null)
            {
                return OperationResult<string>.Failure("Friend request from target user exists");
            }

            var alreadyFriends = await _context.Friends
                .FirstOrDefaultAsync(fr => (fr.FriendName1 == sourceUserName && fr.FriendName2 == targetUserName)
                                           || (fr.FriendName1 == targetUserName && fr.FriendName2 == sourceUserName));
            
            if (alreadyFriends != null)
            {
                return OperationResult<string>.Failure("Already friends");
            }
            
            var isBlocked = await _context.Blocks
                .FirstOrDefaultAsync(fr => (fr.SourceUserName == sourceUserName && fr.TargetUserName == targetUserName)
                                           || (fr.SourceUserName == targetUserName && fr.TargetUserName == sourceUserName));

            if (isBlocked != null)
            {
                return OperationResult<string>.Failure("Unexpected error has occured");
            }
            
            // Create a new friend request
            var newRequest = new FriendRequest
            {
                SourceUserName = sourceUserName,
                TargetUserName = targetUserName,
            };
            _context.FriendRequests.Add(newRequest);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return OperationResult<string>.Success("Friend request processed successfully.");
        }

        public async Task<OperationResult<string>> AcceptFriendRequestAsync(string sourceUserName, string targetUserName)
        {
            // Find the source user
            var sourceUser = await _userManager.FindByNameAsync(sourceUserName).ConfigureAwait(false);
            
            // Find the target user
            var targetUser = await _userManager.FindByNameAsync(targetUserName).ConfigureAwait(false);
            
            if (sourceUser == null || targetUser == null)
            {
                return OperationResult<string>.Failure("Select valid users only");
            }
            
            // Check if the source user has a pending request to the target user
            var existRequest = await _context.FriendRequests
                .FirstOrDefaultAsync(fr => fr.SourceUserName == sourceUserName && fr.TargetUserName == targetUserName);

            if (existRequest == null)
            {
                return OperationResult<string>.Failure("Friend request does not exist");
            }
            
            _context.FriendRequests.Remove(existRequest);
            
            var newFriend = new Friends
            {
                FriendName1 = sourceUserName,
                FriendName2 = targetUserName,
            };
            _context.Friends.Add(newFriend);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return OperationResult<string>.Success("Friend request accepted successfully.");
        }

        public async Task<OperationResult<string>> RemoveFriendAsync(string sourceUserName, string targetUserName) {
            // Find the source user
            var sourceUser = await _userManager.FindByNameAsync(sourceUserName).ConfigureAwait(false);

            // Find the target user
            var targetUser = await _userManager.FindByNameAsync(targetUserName).ConfigureAwait(false);
            
            if (sourceUser == null || targetUser == null)
            {
                return OperationResult<string>.Failure("Select valid users only");
            }
            
            // Check if the source user and target user are friends
            var alreadyFriends = await _context.Friends
                .FirstOrDefaultAsync(fr => (fr.FriendName1 == sourceUserName && fr.FriendName2 == targetUserName)
                                           || (fr.FriendName1 == targetUserName && fr.FriendName2 == sourceUserName));
            
            if (alreadyFriends == null)
            {
                return OperationResult<string>.Failure("Source and target are not friends");
            }
            

            // Remove the friend request and make Friend
            _context.Friends.Remove(alreadyFriends);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return OperationResult<string>.Success("Friend removed successfully.");
        }

        public async Task<OperationResult<ChatStuffUser>> FetchFriendAsync(string sourceUserName,
            string targetUserName)
        {
            // Find the source user
            var sourceUser = await _userManager.FindByNameAsync(sourceUserName).ConfigureAwait(false);

            // Find the target user
            var targetUser = await _userManager.FindByNameAsync(targetUserName).ConfigureAwait(false);

            if (sourceUser == null || targetUser == null)
            {
                return OperationResult<ChatStuffUser>.Failure("Select valid users only");
            }
            
            // Check if the source user and target user are friends
            var alreadyFriends = await _context.Friends
                .FirstOrDefaultAsync(fr => (fr.FriendName1 == sourceUserName && fr.FriendName2 == targetUserName)
                                           || (fr.FriendName1 == targetUserName && fr.FriendName2 == sourceUserName));
            
            if (alreadyFriends == null)
            {
                return OperationResult<ChatStuffUser>.Failure("Source and target are not friends");
            }

            return OperationResult<ChatStuffUser>.Success(targetUser);
        }
        
        public async Task<OperationResult<string>> BlockUserAsync(string sourceUserName, string targetUserName) {
            // Find the source user
            var sourceUser = await _userManager.FindByNameAsync(sourceUserName).ConfigureAwait(false);

            // Find the target user
            var targetUser = await _userManager.FindByNameAsync(targetUserName).ConfigureAwait(false);
            
            if (sourceUser == null || targetUser == null)
            {
                return OperationResult<string>.Failure("Select valid users only");
            }

            // Check if the target has been blocked
            var alreadyBlocked = await _context.Blocks
                .FirstOrDefaultAsync(fr => (fr.SourceUserName == sourceUserName && fr.TargetUserName == targetUserName));
            
            if (alreadyBlocked != null)
            {
                return OperationResult<string>.Failure("Target is already blocked");
            }
            
            var alreadyFriends = await _context.Friends
                .FirstOrDefaultAsync(fr => (fr.FriendName1 == sourceUserName && fr.FriendName2 == targetUserName)
                                           || (fr.FriendName1 == targetUserName && fr.FriendName2 == sourceUserName));
            if (alreadyFriends != null)
            {
                await RemoveFriendAsync(sourceUserName, targetUserName);
            }
            
            var newBlock = new Blocks
            {
                SourceUserName = sourceUserName,
                TargetUserName = targetUserName,
            };
            _context.Blocks.Add(newBlock);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return OperationResult<string>.Success("Target blocked successfully.");
        }
        
        public async Task<OperationResult<string>> UnblockUserAsync(string sourceUserName, string targetUserName) {
            // Find the source user
            var sourceUser = await _userManager.FindByNameAsync(sourceUserName).ConfigureAwait(false);

            // Find the target user
            var targetUser = await _userManager.FindByNameAsync(targetUserName).ConfigureAwait(false);
            
            if (sourceUser == null || targetUser == null)
            {
                return OperationResult<string>.Failure("Select valid users only");
            }

            // Check if the target has been blocked
            var alreadyBlocked = await _context.Blocks
                .FirstOrDefaultAsync(fr => (fr.SourceUserName == sourceUserName && fr.TargetUserName == targetUserName));
            
            if (alreadyBlocked == null)
            {
                return OperationResult<string>.Failure("Target is not blocked");
            }

            _context.Blocks.Remove(alreadyBlocked);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return OperationResult<string>.Success("Target unblocked successfully.");
        }
}