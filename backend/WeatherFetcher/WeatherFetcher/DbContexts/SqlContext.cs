using Microsoft.EntityFrameworkCore;
using WeatherFetcher.Models.Entity;

namespace WeatherFetcher.DbContexts
{
    public class SqlContext : DbContext
    {
        public DbSet<WeatherRequestByZip> WeatherRequestByZip { get; set; }

        public SqlContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=weatherdb;Trusted_Connection=True;");
        }
    }
}
