using Blog.Identity.Application.Contracts.Persistence.Common;
using Blog.Identity.Domain.Entities.User;

namespace Blog.Identity.Application.Contracts.Persistence.User;

public interface IUserRepository : IBaseRepository<UserEntity>
{

}
