using System;
using Blog.Solution.Domain.Entities.Author;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Solution.Persistence.Configurations.Author;

public class AuthorEntityConfig : IEntityTypeConfiguration<AuthorEntity>
{
    public void Configure(EntityTypeBuilder<AuthorEntity> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Name)
                .HasMaxLength(80)
                .IsRequired();

        builder.Property(a => a.Surname)
                .HasMaxLength(100)
                .IsRequired();

        builder.HasIndex(a => a.Email);

        builder.Property(a => a.Email)
                .HasMaxLength(255)
                .IsRequired();

        builder.HasMany(a => a.Comments)
                .WithOne(c => c.Author)
                .HasForeignKey(c => c.AuthorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(a => a.Posts)
                .WithOne(p => p.Author)
                .HasForeignKey(p => p.AuthorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(a => a.Events);
    }
}
