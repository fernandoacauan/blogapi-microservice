using Blog.Identity.Application.Contracts.Persistence.Role;
using Blog.Identity.Application.DTOs.Role;
using Blog.Identity.Persistence.Context;
using Blog.Identity.Persistence.Repository.Common;
using Dapper;

namespace Blog.Identity.Persistence.Repository.Role;

public sealed class RoleQuery : BaseQuery, IRoleQuery
{
    private const string BaseSql = """SELECT r."Id", r."Name", r."IsAdmin", r."CreatedAt", r."UpdatedAt" FROM "Role" r""";

    public RoleQuery(AuthDbContext context) : base(context)
    {
    }

    public async Task<IReadOnlyList<RoleDto>> GetAllAsync(CancellationToken ct = default)
    {
        var command = $"""{BaseSql} ORDER BY r."Name" ASC""";

        return (await DbConnection.QueryAsync<RoleDto>(
            new CommandDefinition(command, cancellationToken: ct, transaction: Transaction)
        )).AsList();
    }

    public async Task<RoleDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var command = $"""{BaseSql} WHERE r."Id" = @id""";

        return await DbConnection.QueryFirstOrDefaultAsync<RoleDto>(
            new CommandDefinition(command, new { id }, cancellationToken: ct, transaction: Transaction)
        );
    }
}
