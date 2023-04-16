using Core.Database.Entities;
using Microsoft.EntityFrameworkCore;
using SQLiteRepository.ModelsConfigurations;

namespace SQLiteRepository
{
    public class EstablishmentContext: DbContext
    {
        public const string DEFAULT_DB_NAME = "Establishment";
        private readonly string connectionString = "";
        

        /// <summary>
        /// Список сотрудников
        /// </summary>
        public DbSet<EmployeeEntity> Employees { get; set; } = null!;
        public DbSet<DepartmentEntity> Departments { get; set; } = null!;
        public DbSet<EstablishmentEntity> Establishments { get; set; } = null!;

        public DbSet<FunctionGroupEntity> FunctionGroups { get; set; } = null!;

        /// <summary>
        /// Перечень должностей
        /// </summary>
        public DbSet<FunctionEntity> Functions { get; set; } = null!;

        /// <summary>
        /// Типы временных интервалов
        /// </summary>
        public DbSet<TimeIntervalTypeEntity> TimeIntervalTypes { get; set; } = null!;
        public DbSet<TimeIntervalEntity> TimeIntervals { get; set; } = null!;

        /// <summary>
        /// Список статусов сотрудника: активен, уволен.
        /// </summary>
        public DbSet<EmployeeStatusEntity> EmployeeStatuses { get; set; } = null!;


        public EstablishmentContext(string? connectionString = null)
        {
            if (string.IsNullOrEmpty(connectionString))
                this.connectionString = $"Data Source={DEFAULT_DB_NAME}.db";

            Database.EnsureDeleted();
            Database.EnsureCreated();
          
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new EmployeesConfiguration());
            modelBuilder.ApplyConfiguration(new DepartmentConfig());
            modelBuilder.ApplyConfiguration(new EstablishmentConfig());
            modelBuilder.ApplyConfiguration(new FunctionConfig());
            modelBuilder.ApplyConfiguration(new FunctionGroupConfig());
            modelBuilder.ApplyConfiguration(new TimeIntervalsConfig());
            modelBuilder.ApplyConfiguration(new TimeIntervalTypesConfig());
            modelBuilder.ApplyConfiguration(new EmoployeeStatusConfig());
        }

        /// <summary>
        /// Возвращает экземпляр контекста
        /// </summary>
        /// <returns></returns>
        public static EstablishmentContext Get() => new EstablishmentContext();
    }
}