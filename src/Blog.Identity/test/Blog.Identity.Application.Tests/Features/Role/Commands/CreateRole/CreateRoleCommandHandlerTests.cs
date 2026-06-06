using System.Linq.Expressions;
using Blog.Identity.Application.Contracts.Persistence.Common;
using Blog.Identity.Application.Exceptions;
using Blog.Identity.Application.Features.Role.Commands.CreateRole;
using Blog.Identity.Domain.Entities.Role;
using FluentAssertions;
using NSubstitute;

namespace Blog.Identity.Application.Tests.Features.Role.Commands.CreateRole;

public class CreateRoleCommandHandlerTests
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly CreateRoleCommandHandler _handler;

    public CreateRoleCommandHandlerTests()
    {
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _handler = new(_unitOfWork);
    }

    [Fact]
    public async Task CreateRoleWithValidCommand_ShouldReturnId()
    {
        var command = new CreateRoleCommand("test", true);
        Guid id;

        _unitOfWork.Role.AnyAsync(Arg.Any<Expression<Func<RoleEntity,bool>>>(), Arg.Any<CancellationToken>())
                        .Returns(Task.FromResult(false));

        id = await _handler.Handle(command, CancellationToken.None);

        await _unitOfWork.Role.Received(1).CreateAsync(Arg.Any<RoleEntity>(), Arg.Any<CancellationToken>());

        await _unitOfWork.SaveChangesAsync(Arg.Any<CancellationToken>());

        id.Should().NotBeEmpty();
    }

    [Fact]
    public async Task CreateRole_RoleAlreadyExists_ShouldThrowConflictException()
    {
        var command = new CreateRoleCommand("test", true);
        Func<Task> func;

        _unitOfWork.Role.AnyAsync(Arg.Any<Expression<Func<RoleEntity,bool>>>(), Arg.Any<CancellationToken>())
                        .Returns(Task.FromResult(true));

        func = async() => await _handler.Handle(command, CancellationToken.None);

        await func.Should().ThrowAsync<ConflictException>()
                            .WithMessage("Role already exists.");

        await _unitOfWork.Role.DidNotReceive().CreateAsync(Arg.Any<RoleEntity>(), Arg.Any<CancellationToken>());
        await _unitOfWork.SaveChangesAsync(Arg.Any<CancellationToken>());
    }
}
