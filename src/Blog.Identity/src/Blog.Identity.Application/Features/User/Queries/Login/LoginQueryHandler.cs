using System;
using Blog.Identity.Application.Abstractions.Password;
using Blog.Identity.Application.Abstractions.Token;
using Blog.Identity.Application.Contracts.Persistence.Common;
using Blog.Identity.Application.Contracts.Persistence.User;
using Blog.Identity.Application.DTOs.User;
using Blog.Identity.Application.Exceptions;
using MediatR;

namespace Blog.Identity.Application.Features.User.Queries.Login;

public sealed class LoginQueryHandler : IRequestHandler<LoginQuery, string>
{
    private readonly ITokenService _tokenService;
    private readonly IPasswordService _passwordService;
    private readonly IUserQuery _userQuery;

    public LoginQueryHandler(ITokenService tokenService, IPasswordService passwordService, IUserQuery userQuery)
    {
        _tokenService = tokenService;
        _passwordService = passwordService;
        _userQuery = userQuery;
    }

    public async Task<string> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        UserLoginDto? userLoginDto = await _userQuery.GetLoginUserByEmailAsync(request.Email, cancellationToken);

        if (userLoginDto == null || !_passwordService.VerifyPassword(request.Password, userLoginDto.HashedPassword))
        {
            throw new NotFoundException("Email or password invalid");
        }

        return _tokenService.GenerateToken(userLoginDto);
    }
}
