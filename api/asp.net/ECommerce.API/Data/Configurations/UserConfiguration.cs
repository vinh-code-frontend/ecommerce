using ECommerce.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.API.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.Property(u => u.Username)
                .HasMaxLength(50);

            builder.Property(u => u.HashedPassword)
                .HasMaxLength(255);

            builder.Property(u => u.FulName)
                .HasMaxLength(100);

            builder.Property(u => u.Email)
                .HasMaxLength(100);

            builder.Property(u => u.PhoneNumber)
                .HasMaxLength(50);

            builder.Property(u => u.Provider)
                .HasConversion<string>().HasMaxLength(20);

            builder.Property(u => u.Role)
                .HasConversion<string>().HasMaxLength(20);

            builder.Property(u => u.Status)
                .HasConversion<string>().HasMaxLength(20);
        }
    }
}
