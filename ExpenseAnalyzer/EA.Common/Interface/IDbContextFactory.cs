using Microsoft.EntityFrameworkCore;

namespace EA.Common
{
    public interface IDbContextFactory
    {
        TDbContext Get<TDbContext>() where TDbContext : DbContext, new();
    }
}
