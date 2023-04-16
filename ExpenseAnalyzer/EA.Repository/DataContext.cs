using EA.Model;
using Microsoft.EntityFrameworkCore;

namespace EA.Repository
{
    public class DataContext : DbContext
    {
        private readonly string connectionString;

        public DataContext(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseJet(this.connectionString);
            }
        }

        /// <summary>
        /// Tag Dbset
        /// </summary>
        public DbSet<Tag> Tags { get; set; }
        /// <summary>
        /// HDFC SB Statement
        /// </summary>
        public DbSet<HdfcSbStatement> HdfcSbStatements { get; set; }

    }
}