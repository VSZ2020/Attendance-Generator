using Microsoft.EntityFrameworkCore;
using SQLiteRepository.ModelsConfigurations;

namespace SQLiteRepository
{
    public class EstablishmentContext: BaseContext
    {
		public override string DatabaseName => "Establishment";

		public EstablishmentContext(): base()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(base.ConnectionString);
#if DEBUG
            optionsBuilder.EnableSensitiveDataLogging();
#endif
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new DepartmentConfig());
            modelBuilder.ApplyConfiguration(new EstablishmentConfig());
            modelBuilder.ApplyConfiguration(new EmployeesConfiguration());
            modelBuilder.ApplyConfiguration(new FunctionConfig());
            modelBuilder.ApplyConfiguration(new FunctionGroupConfig());
            modelBuilder.ApplyConfiguration(new TimeIntervalsConfig());
            modelBuilder.ApplyConfiguration(new TimeIntervalTypesConfig());
            modelBuilder.ApplyConfiguration(new EmoployeeStatusConfig());
            modelBuilder.ApplyConfiguration(new CorrectionDaysConfig());
        }

        /// <summary>
        /// Возвращает экземпляр контекста
        /// </summary>
        /// <returns></returns>
        public static EstablishmentContext Get() => new EstablishmentContext();
    }
}