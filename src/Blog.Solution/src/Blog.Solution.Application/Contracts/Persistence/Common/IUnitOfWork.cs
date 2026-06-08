using System;
using Blog.Solution.Application.Contracts.Persistence.Author;

namespace Blog.Solution.Application.Contracts.Persistence.Common;

public interface IUnitOfWork
{
    IAuthorRepository Authors { get; }

    Task SaveChangesAsync(CancellationToken ct = default);
}
