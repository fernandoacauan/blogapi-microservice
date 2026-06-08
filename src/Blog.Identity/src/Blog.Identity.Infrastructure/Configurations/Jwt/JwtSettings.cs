namespace Blog.Identity.Infrastructure.Configurations.Jwt;

public sealed class JwtSettings
{
    public required string Key { get; set; }
    public required string Issuer { get; set; }
    public required string Audience { get; set; }
}
