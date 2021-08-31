using ETLLibrary.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ETLLibrary.Database
{
    public class EtlContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Csv> CsvFiles { get; set; }
        private const string ConnectionString = "Server=.; Database=ETLDB; Trusted_Connection=True;";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseSqlServer(ConnectionString);
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Csv>()
                .HasOne(p => p.User)
                .WithMany(b => b.csvFiles)
                .HasForeignKey(p => p.UserId);
        }
    }
}