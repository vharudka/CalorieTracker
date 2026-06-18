namespace CalorieTracker.Api.Exceptions;

public class FoodCacheNotFoundException : Exception
{
    public FoodCacheNotFoundException(string barcode)
        : base($"Food cache not found for {barcode}") { }
}