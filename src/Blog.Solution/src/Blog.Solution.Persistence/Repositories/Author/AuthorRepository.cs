using Blog.Solution.Application.Contracts.Persistence.Author;
using Blog.Solution.Domain.Entities.Author;
using Blog.Solution.Persistence.Context.BlogDbContext;
using Blog.Solution.Persistence.Repositories.Common;

namespace Blog.Solution.Persistence.Repositories.Author;

public sealed class AuthorRepository : BaseRepository<AuthorEntity>, IAuthorRepository
{
    public AuthorRepository(BlogDbContext context) : base(context)
    {
    }
}
