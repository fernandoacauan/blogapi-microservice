using Blog.Identity.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Identity.Persistence.Configurations.User;

public sealed class UserEntityConfig : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(u => u.Id);

        builder.HasIndex(u => u.Email)
                .IsUnique();

        builder.Property(u => u.Name)
                .HasMaxLength(80)
                .IsRequired();

        builder.Property(u => u.Surname)
                .HasMaxLength(100)
                .IsRequired();

        builder.Property(u => u.Email)
                .HasMaxLength(255)
                .IsRequired();

        builder.Property(u => u.HashedPassword)
                .HasMaxLength(255)
                .IsRequired();

        builder.HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(u => u.Events);
    }
}
