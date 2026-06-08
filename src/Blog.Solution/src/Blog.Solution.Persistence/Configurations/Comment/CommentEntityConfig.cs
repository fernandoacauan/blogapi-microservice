using Blog.Solution.Domain.Entities.Author;
using Blog.Solution.Domain.Entities.Comment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Solution.Persistence.Configurations.Comment;

public sealed class CommentEntityConfig : IEntityTypeConfiguration<CommentEntity>
{
    public void Configure(EntityTypeBuilder<CommentEntity> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Body)
                .HasMaxLength(255)
                .IsRequired();

        builder.HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Likes)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "CommentsLikes",
                    l => l.HasOne<AuthorEntity>()
                            .WithMany()
                            .HasForeignKey("AuthorId")
                            .IsRequired(),
                    r => r.HasOne<CommentEntity>()
                            .WithMany()
                            .HasForeignKey("CommentId")
                            .IsRequired()
                );

        builder.Ignore(c => c.Events);
    }
}
