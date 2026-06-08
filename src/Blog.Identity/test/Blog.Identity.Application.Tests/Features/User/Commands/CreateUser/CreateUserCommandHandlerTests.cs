using System.Linq.Expressions;
using Blog.Identity.Application.Abstractions.Password;
using Blog.Identity.Application.Contracts.Persistence.Common;
using Blog.Identity.Application.Exceptions;
using Blog.Identity.Application.Features.User.Commands.CreateUser;
using Blog.Identity.Domain.Constants.Role;
using Blog.Identity.Domain.Entities.User;
using FluentAssertions;
using NSubstitute;

namespace Blog.Identity.Application.Tests.Features.User.Commands.CreateUser;

public class CreateUserCommandHandlerTests
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly CreateUserCommandHandler _handler;
    private readonly IPasswordService _passwordService;

    public CreateUserCommandHandlerTests()
    {
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _passwordService = Substitute.For<IPasswordService>();
        _handler = new(_unitOfWork, _passwordService);
    }

    [Fact]
    public async Task CreateUserCommand_EmailAlreadyExists_ShouldThrowConflictException()
    {
        UserEntity user = new UserEntity("John", "Doe", "john.doe@brainwave.com", "abcdefg", RoleConstant.UserId);
        CreateUserCommand createUser = new CreateUserCommand(user.Name, user.Surname, user.Email, user.HashedPassword);
        Func<Task> func;

        _unitOfWork.User.AnyAsync(Arg.Any<Expression<Func<UserEntity,bool>>>(), Arg.Any<CancellationToken>())
                        .Returns(Task.FromResult<bool>(true));

        func = async() => await _handler.Handle(createUser, CancellationToken.None);

        await func.Should().ThrowAsync<ConflictException>()
                            .WithMessage("User already exists");

        await _unitOfWork.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task CreateUserCommand_ValidCommand_ShouldReturnUserId()
    {
        CreateUserCommand createUser = new CreateUserCommand("John", "Doe", "john.doe@brainwave.com", "abcdefg");
        Guid id;

        _unitOfWork.User.AnyAsync(Arg.Any<Expression<Func<UserEntity,bool>>>(), Arg.Any<CancellationToken>())
                        .Returns(Task.FromResult<bool>(false));

        _passwordService.HashPassword(Arg.Any<string>())
                        .Returns("12345");

        id = await _handler.Handle(createUser, CancellationToken.None);

        id.Should().NotBeEmpty();

        await _unitOfWork.User.Received(1).CreateAsync(Arg.Any<UserEntity>(), Arg.Any<CancellationToken>());

        await _unitOfWork.Received(1).SaveChangesAsync(CancellationToken.None);
    }
}
