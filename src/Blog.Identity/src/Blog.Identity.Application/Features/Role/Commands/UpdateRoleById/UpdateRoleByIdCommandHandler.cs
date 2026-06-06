using Blog.Identity.Application.Contracts.Persistence.Common;
using Blog.Identity.Application.Exceptions;
using Blog.Identity.Domain.Entities.Role;
using MediatR;

namespace Blog.Identity.Application.Features.Role.Commands.UpdateRoleById;

public sealed class UpdateRoleByIdCommandHandler : IRequestHandler<UpdateRoleByIdCommand,Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateRoleByIdCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateRoleByIdCommand request, CancellationToken cancellationToken)
    {
        RoleEntity? role = await _unitOfWork.Role.GetByAsync(r => r.Id == request.Id, cancellationToken);

        if (role == null)
        {
            throw new NotFoundException("Role not found");
        }

        if (role.Name != request.Name && await _unitOfWork.Role.AnyAsync(r => r.Name == request.Name, cancellationToken))
        {
            throw new ConflictException("Role name already exists.");
        }

        role.SetName(request.Name);
        role.SetAdmin(request.IsAdmin);

        _unitOfWork.Role.Update(role);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
