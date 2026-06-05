namespace CalorieTracker.Api.Exceptions;

public class UserGoalsNotFoundException : Exception
{
    public UserGoalsNotFoundException(Guid userId)
        : base($"User goals not found for user {userId}") { }
}