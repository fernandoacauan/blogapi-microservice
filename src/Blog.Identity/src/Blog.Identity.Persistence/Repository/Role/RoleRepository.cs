using Blog.Identity.Application.Contracts.Persistence.Role;
using Blog.Identity.Domain.Entities.Role;
using Blog.Identity.Persistence.Context;
using Blog.Identity.Persistence.Repository.Common;

namespace Blog.Identity.Persistence.Repository.Role;

public sealed class RoleRepository : BaseRepository<RoleEntity>, IRoleRepository
{
    public RoleRepository(AuthDbContext context) : base(context)
    {
    }
}
