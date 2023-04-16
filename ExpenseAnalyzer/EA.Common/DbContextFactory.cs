using Microsoft.EntityFrameworkCore;

namespace EA.Common
{
    public class DbContextFactory : IDbContextFactory
    {
        public TDbContext Get<TDbContext>() where TDbContext : DbContext, new()
        {
            return new TDbContext();
        }
    }
}