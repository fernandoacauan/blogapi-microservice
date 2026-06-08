using System;
using Blog.Solution.Domain.Entities.Author;
using Blog.Solution.Domain.Entities.Comment;
using Blog.Solution.Domain.Entities.Post;
using Microsoft.EntityFrameworkCore;

namespace Blog.Solution.Persistence.Context.BlogDbContext;

public sealed class BlogDbContext : DbContext
{
    public DbSet<AuthorEntity> Authors { get; set; }
    public DbSet<CommentEntity> Comments { get; set; }
    public DbSet<PostEntity> Posts { get; set; }

    public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BlogDbContext).Assembly);
    }

}
