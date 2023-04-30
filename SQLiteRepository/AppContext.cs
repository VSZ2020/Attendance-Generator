using Core.Database.AppEntities;
using Microsoft.EntityFrameworkCore;
using SQLiteRepository.ModelsConfigurations;

namespace SQLiteRepository
{
    public class AppContext: DbContext
    {
        public const string DEFAULT_DB_NAME = "Users";
        private string connectionString = $"Data Source ={DEFAULT_DB_NAME}.db";

        public AppContext(): this(null) { }

        public AppContext(string? connString = null)
        {
            if (string.IsNullOrEmpty(connString))
                this.connectionString = $"Data Source={DEFAULT_DB_NAME}";
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserAccountConfig());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(connectionString);
        }

        public static AppContext Get() => new AppContext();
    }
}
