namespace CalorieTracker.Api.Settings;

public class CacheOptions
{
    public TimeSpan FoodCacheExpiration { get; set; }
    public TimeSpan UserGoalsCacheExpiration { get; set; }
}