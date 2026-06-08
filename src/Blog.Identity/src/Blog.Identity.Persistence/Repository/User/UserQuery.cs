using System.Data.Common;
using Blog.Identity.Application.Contracts.Persistence.User;
using Blog.Identity.Application.DTOs.User;
using Blog.Identity.Persistence.Context;
using Blog.Identity.Persistence.Repository.Common;
using Dapper;

namespace Blog.Identity.Persistence.Repository.User;

public sealed class UserQuery : BaseQuery, IUserQuery
{
    private const string LoginSql = """
                                    SELECT u."Id", u."Name", u."Surname", u."Email", u."HashedPassword", r."IsAdmin" 
                                    FROM "User" u
                                    INNER JOIN "Role" r ON r."Id" = u."RoleId"
                                    """;
    private const string UserSql = """
                                    SELECT u."Id", u."Name", u."Surname", u."Email", r."Name" AS "Role", u."CreatedAt", u."UpdatedAt" 
                                    FROM "User" u
                                    INNER JOIN "Role" r ON r."Id" = u."RoleId"
                                    """;

    public UserQuery(AuthDbContext context) : base(context)
    {
    }

    public async Task<IReadOnlyList<UserDto>> GetAllUsersAsync(CancellationToken ct = default)
    {
        var command = $"""{UserSql} ORDER BY u."Name" ASC""";

        return (await DbConnection.QueryAsync<UserDto>(new CommandDefinition(
            command, transaction: Transaction, cancellationToken: ct
        ))).ToList();
    }

    public async Task<UserLoginDto?> GetLoginUserByEmailAsync(string email, CancellationToken ct = default)
    {
        var command = $"""{LoginSql} WHERE u."Email" = @email""";

        return await DbConnection.QueryFirstOrDefaultAsync<UserLoginDto>(new CommandDefinition(
            command, new { email }, transaction: Transaction, cancellationToken: ct
        ));
    }

    public async Task<UserDto?> GetUserById(Guid id, CancellationToken ct = default)
    {
        var command = $"""{UserSql} WHERE u."Id" = @id""";

        return await DbConnection.QueryFirstOrDefaultAsync<UserDto>(new CommandDefinition(
            command, new { id }, transaction: Transaction, cancellationToken: ct
        ));
    }
}
