using Blog.Identity.Domain.Constants.Role;
using Blog.Identity.Domain.Entities.Role;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Identity.Persistence.Configurations.Role;

public sealed class RoleEntityConfig : IEntityTypeConfiguration<RoleEntity>
{
    public void Configure(EntityTypeBuilder<RoleEntity> builder)
    {
        builder.HasKey(r => r.Id);

        builder.HasIndex(r => r.Name)
                .IsUnique();

        builder.Property(r => r.Name)
                .HasMaxLength(50)
                .IsRequired();
        
        builder.Property(r => r.IsAdmin)
                .IsRequired();

        builder.Ignore(r => r.Events);

        builder.HasData(new RoleEntity(RoleConstant.AdminName, true, new DateTime(1970, 01, 01, 0, 0, 0, DateTimeKind.Utc), new DateTime(1970, 01, 01, 0, 0, 0, DateTimeKind.Utc), RoleConstant.AdminId), 
                        new RoleEntity(RoleConstant.UserName, false, new DateTime(1970, 01, 01, 0, 0, 0, DateTimeKind.Utc), new DateTime(1970, 01, 01, 0, 0, 0, DateTimeKind.Utc), RoleConstant.UserId));
    }
}
