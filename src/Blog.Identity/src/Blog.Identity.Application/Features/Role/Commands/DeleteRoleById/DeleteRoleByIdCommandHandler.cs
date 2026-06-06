using Blog.Identity.Application.Contracts.Persistence.Common;
using Blog.Identity.Application.Exceptions;
using Blog.Identity.Domain.Entities.Role;
using MediatR;

namespace Blog.Identity.Application.Features.Role.Commands.DeleteRoleById;

public sealed class DeleteRoleByIdCommandHandler : IRequestHandler<DeleteRoleByIdCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteRoleByIdCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteRoleByIdCommand request, CancellationToken cancellationToken)
    {
        RoleEntity? role = await _unitOfWork.Role.GetByAsync(r => r.Id == request.Id, cancellationToken);

        if (role == null)
        {
            throw new NotFoundException("Role not found");
        }

        _unitOfWork.Role.Delete(role);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
