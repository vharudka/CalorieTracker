namespace CalorieTracker.Api.Helpers;

public class CacheKeys
{
    public static string UserGoals(Guid userId)
        => $"usergoals:{userId}";

    public static string FoodCacheKey(string barcode)
        => $"food:cache:{barcode}";
}