﻿using Microsoft.EntityFrameworkCore;
using SQLiteRepository.ModelsConfigurations;

namespace SQLiteRepository
{
    public class EstablishmentContext: DbContext
    {
        public const string DEFAULT_DB_NAME = "Establishment";
        private readonly string connectionString = "";

        //public DbSet<EstablishmentEntity> Establishments { get; set; } = null!;
        //public DbSet<DepartmentEntity> Departments { get; set; } = null!;
        //public DbSet<FunctionEntity> Functions { get; set; } = null!;
        //public DbSet<FunctionGroupEntity> FruncGroups { get; set; } = null!;
        //public DbSet<EmployeeStatusEntity> EmployeeStatuses { get; set; } = null!;
        //public DbSet<EmployeeEntity> Employees { get; set; } = null!;

        //public DbSet<TimeIntervalTypeEntity> TimeIntervalTypes { get; set; } = null!;
        //public DbSet<TimeIntervalEntity> TimeIntervals { get; set; } = null!;

        public EstablishmentContext(): this(null)
        {

        }

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
            optionsBuilder.EnableSensitiveDataLogging();
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
        }

        /// <summary>
        /// Возвращает экземпляр контекста
        /// </summary>
        /// <returns></returns>
        public static EstablishmentContext Get() => new EstablishmentContext();
    }
}