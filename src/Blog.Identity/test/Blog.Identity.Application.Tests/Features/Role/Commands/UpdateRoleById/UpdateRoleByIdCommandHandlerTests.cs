using System.Linq.Expressions;
using Blog.Identity.Application.Contracts.Persistence.Common;
using Blog.Identity.Application.Exceptions;
using Blog.Identity.Application.Features.Role.Commands.UpdateRoleById;
using Blog.Identity.Domain.Entities.Role;
using FluentAssertions;
using NSubstitute;

namespace Blog.Identity.Application.Tests.Features.Role.Commands.UpdateRoleById;

public class UpdateRoleByIdCommandHandlerTests
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UpdateRoleByIdCommandHandler _handler;

    public UpdateRoleByIdCommandHandlerTests()
    {
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _handler = new(_unitOfWork);
    }

    [Fact]
    public async Task UpdateRoleById_RoleInexistent_ShouldThrowNotFound()
    {
        Func<Task> func;
        UpdateRoleByIdCommand update = new UpdateRoleByIdCommand("testt", true);

        _unitOfWork.Role.GetByAsync(Arg.Any<Expression<Func<RoleEntity,bool>>>(), Arg.Any<CancellationToken>())
                        .Returns(Task.FromResult<RoleEntity?>(null));

        func = async() => await _handler.Handle(update, CancellationToken.None);

        await func.Should().ThrowAsync<NotFoundException>()
                            .WithMessage("Role not found");

        await _unitOfWork.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task UpdateRoleById_RoleAlreadyExists_ShouldThrowConflictException()
    {
        RoleEntity role = new RoleEntity("testt", true);
        UpdateRoleByIdCommand update = new UpdateRoleByIdCommand("test", true);
        Func<Task> func;

        _unitOfWork.Role.GetByAsync(Arg.Any<Expression<Func<RoleEntity,bool>>>(), Arg.Any<CancellationToken>())
                        .Returns(Task.FromResult<RoleEntity?>(role));

        _unitOfWork.Role.AnyAsync(Arg.Any<Expression<Func<RoleEntity,bool>>>(), Arg.Any<CancellationToken>())
                                .Returns(Task.FromResult<bool>(true));

        func = async() => await _handler.Handle(update, CancellationToken.None);

        await func.Should().ThrowAsync<ConflictException>()
                            .WithMessage("Role name already exists.");

        await _unitOfWork.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task UpdateRoleById_ValidCommand_ShouldReturnNothing()
    {
        RoleEntity role = new RoleEntity("testt", true);
        UpdateRoleByIdCommand update = new UpdateRoleByIdCommand("john", false);

        _unitOfWork.Role.GetByAsync(Arg.Any<Expression<Func<RoleEntity,bool>>>(), Arg.Any<CancellationToken>())
                        .Returns(Task.FromResult<RoleEntity?>(role));

        _unitOfWork.Role.AnyAsync(Arg.Any<Expression<Func<RoleEntity,bool>>>(), Arg.Any<CancellationToken>())
                        .Returns(Task.FromResult<bool>(false));

        await _handler.Handle(update, CancellationToken.None);
        
        _unitOfWork.Role.Received(1).Update(role);

        await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());

        role.Name.Should().Be(update.Name);
        role.IsAdmin.Should().Be(update.IsAdmin);
    }
}