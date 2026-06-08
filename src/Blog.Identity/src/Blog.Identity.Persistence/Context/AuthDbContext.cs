using Blog.Identity.Domain.Entities.Role;
using Blog.Identity.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace Blog.Identity.Persistence.Context;

public sealed class AuthDbContext : DbContext
{
    public DbSet<UserEntity> User { get; set; }
    public DbSet<RoleEntity> Role { get; set; }

    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthDbContext).Assembly);
    }
}
