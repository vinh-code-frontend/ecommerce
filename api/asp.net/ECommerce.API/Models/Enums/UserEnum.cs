namespace ECommerce.API.Models.Enums
{
    public enum AuthProvider
    {
        LOCAL,
        GOOGLE,
        FACEBOOK,
        TWITTER
    }
    public enum RoleEnum
    {
        ADMIN,
        CUSTOMER,
        STAFF,
        SELLER
    }
    public enum UserStatus
    {
        ACTIVE,
        INACTIVE,
        BANNED,
        DELETED
    }
}
