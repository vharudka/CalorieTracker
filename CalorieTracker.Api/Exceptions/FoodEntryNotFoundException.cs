namespace CalorieTracker.Api.Exceptions;

public class FoodEntryNotFoundException : Exception
{
    public FoodEntryNotFoundException(Guid userId)
        : base($"Food entry not found for user {userId}") { }
}