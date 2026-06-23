namespace CalorieTracker.Api.Options;

public class PasswordValidationOptions
{
    public bool RequireUppercase { get; set; }
    public bool RequireLowercase { get; set; }
    public bool RequireDigit { get; set; }
    public bool RequireSpecial { get; set; }
    public int MinimumLength { get; set; }
}