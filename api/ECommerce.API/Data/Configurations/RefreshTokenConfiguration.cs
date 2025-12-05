using ECommerce.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Data.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshTokens");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Token)
                   .HasColumnType("NVARCHAR(MAX)")
                   .IsRequired();

            builder.Property(x => x.ExpiresAt)
                   .HasColumnType("DATETIMEOFFSET")
                   .IsRequired();

            builder.Property(x => x.CreatedAt)
                   .HasColumnType("DATETIMEOFFSET")
                   .IsRequired();

            builder.Property(x => x.CreatedByIp)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(x => x.RevokedAt)
                   .HasColumnType("DATETIMEOFFSET")
                   .IsRequired(false);

            builder.Property(x => x.RevokedByIp)
                   .HasMaxLength(50)
                   .IsRequired(false);

            builder.Property(x => x.ReplacedByToken)
                   .HasColumnType("NVARCHAR(MAX)")
                   .IsRequired(false);

            // Relation: 1 user - many refresh tokens
            builder.HasOne(x => x.User)
                   .WithMany(u => u.RefreshTokens)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Optional: Index for performance
            // builder.HasIndex(x => x.UserId);
        }
    }
}
