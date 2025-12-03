namespace ECommerce.API.Models
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTimeOffset ExpiresAt { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedByIp { get; set; } = string.Empty;
        public DateTimeOffset? RevokedAt { get; set; }
        public string? RevokedByIp { get; set; }
        public string? ReplacedByToken { get; set; }
        // Foreign key
        public Guid UserId { get; set; }
        // Navigation property
        public User User { get; set; } = null!;
    }
}
