using System;
using AutoMapper;
using Blog.Identity.Application.Abstractions.Password;
using Blog.Identity.Application.Abstractions.TokenInfo;
using Blog.Identity.Application.Contracts.Persistence.Common;
using Blog.Identity.Application.Exceptions;
using Blog.Identity.Domain.Constants.Role;
using Blog.Identity.Domain.Entities.User;
using MediatR;

namespace Blog.Identity.Application.Features.User.Commands.UpdateUser;

public sealed class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenInformation _tokenInformation;
    private readonly IPasswordService _passwordService;
    private readonly IMapper _mapper;

    public UpdateUserCommandHandler(IUnitOfWork unitOfWork, ITokenInformation tokenInformation, IPasswordService passwordService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _tokenInformation = tokenInformation;
        _passwordService = passwordService;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        Guid userId = _tokenInformation.GetId();
        bool isAdmin = _tokenInformation.IsAdmin();
        UserEntity user;

        if (request.Id != userId && !isAdmin)
        {
            throw new ForbiddenException("Only administrators can update others users");
        }

        user = await _unitOfWork.User.GetByAsync(u => u.Id == request.Id, cancellationToken, true)
            ?? throw new NotFoundException("User not found");

        if (user.Email != request.Email && await _unitOfWork.User.AnyAsync(u => u.Email == request.Email, cancellationToken))
        {
            throw new ConflictException("Email already in use");
        }

        _mapper.Map(request, user);

        user.SetHashedPassword(_passwordService.HashPassword(request.Password));

        _unitOfWork.User.Update(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
