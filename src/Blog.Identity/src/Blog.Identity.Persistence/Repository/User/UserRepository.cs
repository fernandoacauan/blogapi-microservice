using Blog.Identity.Application.Contracts.Persistence.User;
using Blog.Identity.Domain.Entities.User;
using Blog.Identity.Persistence.Context;
using Blog.Identity.Persistence.Repository.Common;

namespace Blog.Identity.Persistence.Repository.User;

public sealed class UserRepository : BaseRepository<UserEntity>, IUserRepository
{
    public UserRepository(AuthDbContext context) : base(context)
    {
    }
}
