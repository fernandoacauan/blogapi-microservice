using System;
using Blog.Identity.Application.Abstractions.Password;
using Blog.Identity.Application.Contracts.Persistence.Common;
using Blog.Identity.Application.Exceptions;
using Blog.Identity.Domain.Constants.Role;
using Blog.Identity.Domain.Entities.User;
using MediatR;

namespace Blog.Identity.Application.Features.User.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private IUnitOfWork _unitOfWork;
    private IPasswordService _passwordService;
    public CreateUserCommandHandler(IUnitOfWork unitOfWork, IPasswordService passwordService)
    {
        _unitOfWork = unitOfWork;
        _passwordService = passwordService;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        UserEntity user;

        if(await _unitOfWork.User.AnyAsync(u => u.Email == request.Email))
        {
            throw new ConflictException("User already exists");
        }

        string hashPassword = _passwordService.HashPassword(request.Password);

        await _unitOfWork.User.CreateAsync(user = new(request.Name, request.Surname, request.Email, hashPassword, RoleConstant.UserId));

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return user.Id;
    }
}
