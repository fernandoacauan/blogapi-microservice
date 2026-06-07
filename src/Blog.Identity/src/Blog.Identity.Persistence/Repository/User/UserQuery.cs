using System.Data.Common;
using Blog.Identity.Application.Contracts.Persistence.User;
using Blog.Identity.Application.DTOs.User;
using Blog.Identity.Persistence.Context;
using Blog.Identity.Persistence.Repository.Common;
using Dapper;

namespace Blog.Identity.Persistence.Repository.User;

public sealed class UserQuery : BaseQuery, IUserQuery
{
    private const string BaseSql = """
                                    SELECT u."Id", u."Name", u."Surname", u."Email", u."HashedPassword", r."IsAdmin" 
                                    FROM "User" u
                                    INNER JOIN "Role" r ON r."Id" = u."RoleId"
                                    """;

    public UserQuery(AuthDbContext context) : base(context)
    {
    }

    public async Task<UserLoginDto?> GetLoginUserByEmailAsync(string email, CancellationToken ct = default)
    {
        var command = $"""{BaseSql} WHERE u."Email" = @email""";

        return await DbConnection.QueryFirstOrDefaultAsync<UserLoginDto>(new CommandDefinition(
            command, new { email }, transaction: Transaction, cancellationToken: ct
        ));
    }
}
