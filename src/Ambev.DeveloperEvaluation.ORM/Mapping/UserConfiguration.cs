using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(u => u.Username).IsRequired().HasMaxLength(50);
        builder.Property(u => u.Password).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Phone).HasMaxLength(20);
        builder.Property(u => u.CreatedAt).IsRequired();
        builder.Property(u => u.UpdatedAt).IsRequired(false);

        builder.Property(u => u.Status)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(u => u.Role)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.HasData(
            new User
            {
                Id = Guid.NewGuid(),
                Username = "admin",
                Password = "$2a$11$uXrL7R90QQ4G1bILyQjFNeoFZZPXG2hDtW37kp2fN.5Gpc5Zg1Qv2",
                Phone = "51981344567",
                Email = "admin@gmail.com",
                Status = Domain.Enums.UserStatus.Active,
                Role = Domain.Enums.UserRole.Admin,
            });
    }
}
