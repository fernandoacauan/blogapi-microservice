using System.Data;
using System.Data.Common;
using Blog.Identity.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Blog.Identity.Persistence.Repository.Common;

public abstract class BaseQuery
{
    protected IDbConnection DbConnection { get; }
    protected DbTransaction? Transaction { get; }

    protected BaseQuery(AuthDbContext context)
    {
        DbConnection = context.Database.GetDbConnection();
        Transaction = context.Database.CurrentTransaction?.GetDbTransaction();
    }

}
