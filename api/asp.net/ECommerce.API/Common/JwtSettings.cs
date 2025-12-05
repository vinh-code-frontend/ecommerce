namespace ECommerce.API.Common
{
    public class JwtSettings
    {
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public string? SecretKey { get; set; }
        public int ExpiresMinutes { get; set; }
        public int RefreshTokenExpiresDays { get; set; }
    }
}
