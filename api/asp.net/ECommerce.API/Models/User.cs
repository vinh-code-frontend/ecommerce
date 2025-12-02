using ECommerce.API.Models.Enums;

namespace ECommerce.API.Models
{

    public class User
    {
        public Guid Id { get; set; }
        public string? Username { get; set; }
        public string? HashedPassword { get; set; }
        public string? FulName { get; set; }

        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? AvatarUrl { get; set; }
        public AuthProvider Provider { get; set; } = AuthProvider.LOCAL;
        public string? ProviderId { get; set; }

        public RoleEnum Role { get; set; } = RoleEnum.CUSTOMER;
        public UserStatus Status { get; set; } = UserStatus.INACTIVE;

        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; } = null;
    }
}
