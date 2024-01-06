using Microsoft.EntityFrameworkCore;
using SQLiteRepository.ModelsConfigurations;

namespace SQLiteRepository
{
	public class AppContext: BaseContext
    {
		public override string DatabaseName => "Users";

        public AppContext()
        {
            base.RecreateDb();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserAccountConfig());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(base.ConnectionString);
        }

        public static AppContext Instance => new AppContext();
    }
}
