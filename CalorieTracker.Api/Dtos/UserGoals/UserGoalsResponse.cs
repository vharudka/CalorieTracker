namespace CalorieTracker.Api.Dtos.UserGoals;

public record UserGoalsResponse
(
    Guid UserId,
    int DailyCalorieLimit
);