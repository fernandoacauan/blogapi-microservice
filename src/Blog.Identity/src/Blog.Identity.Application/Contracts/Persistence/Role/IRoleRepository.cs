using System;
using Blog.Identity.Application.Contracts.Persistence.Common;
using Blog.Identity.Domain.Entities.Role;

namespace Blog.Identity.Application.Contracts.Persistence.Role;

public interface IRoleRepository : IBaseRepository<RoleEntity>
{

}
