using ETLLibrary.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ETLLibrary.Database
{
    public class EtlContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        private const string ConnectionString = "Server=.; Database=ETLDB; Trusted_Connection=True;";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}