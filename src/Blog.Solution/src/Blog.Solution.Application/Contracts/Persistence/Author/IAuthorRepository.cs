using System;
using Blog.Solution.Application.Contracts.Persistence.Common;
using Blog.Solution.Domain.Entities.Author;
using Blog.Solution.Domain.Entities.Common;

namespace Blog.Solution.Application.Contracts.Persistence.Author;

public interface IAuthorRepository : IBaseRepository<AuthorEntity>
{

}
