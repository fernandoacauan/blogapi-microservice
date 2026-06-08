using Blog.Solution.Domain.Entities.Author;
using Blog.Solution.Domain.Entities.Post;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Solution.Persistence.Configurations.Post;

public sealed class PostEntityConfig : IEntityTypeConfiguration<PostEntity>
{
    public void Configure(EntityTypeBuilder<PostEntity> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Body)
                .HasMaxLength(4096)
                .IsRequired();

        builder.Property(p => p.Title)
                .HasMaxLength(50)
                .IsRequired();

        builder.HasMany(p => p.Likes)
                .WithMany()
                .UsingEntity<Dictionary<string,object>>(
                    "PostLike",
                    l => l.HasOne<AuthorEntity>()
                            .WithMany()
                            .HasForeignKey("AuthorId")
                            .IsRequired(),
                    r => r.HasOne<PostEntity>()
                            .WithMany()
                            .HasForeignKey("PostId")
                            .IsRequired()
                );

        builder.Ignore(p => p.Events);
    }
}
