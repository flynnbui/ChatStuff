namespace ChatStuff.Core.Result;

public class SendFriendRequestResult
{
    // Indicates whether the friend request operation was successful
    public bool Succeeded { get; set; }

    // Contains any relevant data about the operation (e.g., friend request ID)
    public object Data { get; set; }

    // Contains error messages if the operation failed
    public string ErrorMessage { get; set; }

    // Factory methods for creating success and failure results
    public static SendFriendRequestResult Success(object data = null)
    {
        return new SendFriendRequestResult
        {
            Succeeded = true,
            Data = data
        };
    }

    public static SendFriendRequestResult Failure(string errorMessage)
    {
        return new SendFriendRequestResult
        {
            Succeeded = false,
            ErrorMessage = errorMessage
        };
    }
}