using System;
using Blog.Solution.Application.Contracts.Persistence.Common;
using Blog.Solution.Domain.Entities.Author;
using MediatR;

namespace Blog.Solution.Application.Features.Author.Commands.CreateAuthorCommand;

public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateAuthorCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        AuthorEntity author;

        author = new AuthorEntity(request.Id, request.Name, request.Surname, request.Email);

        await _unitOfWork.Authors.CreateAsync(author, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return author.Id;
    }
}
