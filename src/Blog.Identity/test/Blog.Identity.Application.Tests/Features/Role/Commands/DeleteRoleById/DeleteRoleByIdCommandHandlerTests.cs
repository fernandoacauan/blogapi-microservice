using System.Linq.Expressions;
using Blog.Identity.Application.Contracts.Persistence.Common;
using Blog.Identity.Application.Exceptions;
using Blog.Identity.Application.Features.Role.Commands.DeleteRoleById;
using Blog.Identity.Domain.Entities.Role;
using FluentAssertions;
using NSubstitute;

namespace Blog.Identity.Application.Tests.Features.Role.Commands.DeleteRoleById;

public class DeleteRoleByIdCommandHandlerTests
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly DeleteRoleByIdCommandHandler _handler;

    public DeleteRoleByIdCommandHandlerTests()
    {
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _handler = new(_unitOfWork);
    }

    [Fact]
    public async Task DeleteRoleById_RoleInexistent_ShouldThrowNotFound()
    {
        RoleEntity role = new RoleEntity("test", true);
        DeleteRoleByIdCommand deleteRole = new DeleteRoleByIdCommand(role.Id);
        Func<Task> func;

        _unitOfWork.Role.GetByAsync(Arg.Any<Expression<Func<RoleEntity,bool>>>(), Arg.Any<CancellationToken>())
                        .Returns(Task.FromResult<RoleEntity?>(null));

        func = async() => await _handler.Handle(deleteRole, CancellationToken.None);

        await func.Should().ThrowAsync<NotFoundException>()
                            .WithMessage("Role not found");

        await _unitOfWork.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task DeleteRoleById_ValidCommand_ShouldReturnNothing()
    {
        RoleEntity role = new RoleEntity("test", true);
        DeleteRoleByIdCommand deleteRole = new DeleteRoleByIdCommand(role.Id);

        _unitOfWork.Role.GetByAsync(Arg.Any<Expression<Func<RoleEntity,bool>>>(), Arg.Any<CancellationToken>())
                        .Returns(Task.FromResult<RoleEntity?>(role));

        await _handler.Handle(deleteRole, CancellationToken.None);

        _unitOfWork.Role.Received(1).Delete(role);

        await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }
}
