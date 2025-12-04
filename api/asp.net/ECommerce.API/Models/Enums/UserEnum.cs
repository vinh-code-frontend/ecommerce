namespace ECommerce.API.Models.Enums
{
    public enum AuthProvider
    {
        Local,
        Google,
        Facebook
    }
    public enum RoleEnum
    {
        Admin,
        Customer,
        Staff,
        Seller
    }
    public enum UserStatus
    {
        Active,
        Inactive,
        Banned,
        Deleted
    }
}
