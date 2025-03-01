namespace Portfolio.Domain.Contracts;

public class JwtSettings
{
    public const string JwtSettingsKey = "JwtSettings";
    public string? Key { get; set; }
    public string ValidIssuer { get; set; }
    public string ValidAudience { get; set; }
    public double ExpiresInMinutes { get; set; }
}