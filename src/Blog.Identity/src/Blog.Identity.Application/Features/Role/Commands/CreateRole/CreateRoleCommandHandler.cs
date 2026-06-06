using Blog.Identity.Application.Contracts.Persistence.Common;
using Blog.Identity.Application.Exceptions;
using Blog.Identity.Domain.Entities.Role;
using MediatR;

namespace Blog.Identity.Application.Features.Role.Commands.CreateRole;

public sealed class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateRoleCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        RoleEntity role;

        if (await _unitOfWork.Role.AnyAsync(r => r.Name == request.Name, cancellationToken))
        {
            throw new ConflictException("Role already exists.");
        }

        role = new RoleEntity(request.Name, request.IsAdmin);

        await _unitOfWork.Role.CreateAsync(role, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return role.Id;
    }
}
