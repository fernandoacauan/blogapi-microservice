using System;
using Blog.Solution.Application.Contracts.Persistence.Author;
using Blog.Solution.Application.Contracts.Persistence.Common;
using Blog.Solution.Persistence.Context.BlogDbContext;
using Blog.Solution.Persistence.Repositories.Author;

namespace Blog.Solution.Persistence.Repositories.Common;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly BlogDbContext _context;
    private readonly Lazy<IAuthorRepository> _authors;

    public UnitOfWork(BlogDbContext context)
    {
        _context = context;
        _authors = new(() => new AuthorRepository(_context));
    }

    public IAuthorRepository Authors => _authors.Value;

    public async Task SaveChangesAsync(CancellationToken ct = default)
    {
        await _context.SaveChangesAsync(ct);
    }
}
