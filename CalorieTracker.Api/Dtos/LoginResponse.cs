namespace CalorieTracker.Api.Dtos;

public record LoginResponse(string Token, UserDto User);