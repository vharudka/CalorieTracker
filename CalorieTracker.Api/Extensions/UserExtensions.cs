using System.Security.Claims;

namespace CalorieTracker.Api.Extensions;

public static class UserExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        return Guid.Parse(user.FindFirstValue("id")!);
    }
}