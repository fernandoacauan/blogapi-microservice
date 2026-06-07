using System;
using Blog.Identity.Application.Abstractions.Token;
using Blog.Identity.Application.Abstractions.TokenInfo;
using Blog.Identity.Application.Contracts.Persistence.Common;
using Blog.Identity.Application.Exceptions;
using Blog.Identity.Domain.Entities.User;
using MediatR;

namespace Blog.Identity.Application.Features.User.Commands.DeleteUser;

public sealed class DeleteUserCommandByIdHandler : IRequestHandler<DeleteUserCommandById, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenInformation _tokenInformation;

    public DeleteUserCommandByIdHandler(IUnitOfWork unitOfWork, ITokenInformation tokenInformation)
    {
        _unitOfWork = unitOfWork;
        _tokenInformation = tokenInformation;
    }

    public async Task<Unit> Handle(DeleteUserCommandById request, CancellationToken cancellationToken)
    {
        Guid userId = _tokenInformation.GetId();
        bool isAdmin = _tokenInformation.IsAdmin();
        UserEntity user;

        if (request.Id != userId && !isAdmin)
        {
            throw new UnauthorizedAccessException("You need administrator permission to delete other users");
        }

        user = await _unitOfWork.User.GetByAsync(u => u.Id == request.Id, cancellationToken) 
            ?? throw new NotFoundException("User not found");

        _unitOfWork.User.Delete(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
