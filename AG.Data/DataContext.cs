using AG.Data.Entities;
using AG.Data.Entities.RelationshipTables;
using Microsoft.EntityFrameworkCore;

namespace AG.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> builder): base(builder)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<EmployeeEntity> Employees { get; set; } = default!;
        public DbSet<EstablishmentEntity> Establishments { get; set; } = default!;
        public DbSet<DepartmentEntity> Departments { get; set; } = default!;
        public DbSet<TimeIntervalEntity> TimeIntervals { get; set; } = default!;
        public DbSet<BusinessTripEntity> BusinessTrips { get; set; } = default!;
        public DbSet<FunctionEntity> Functions { get; set; } = default!;

        public DbSet<UserEntity> Users { get; set; } = default!;
        

        //Relationship tables
        public DbSet<EmployeeToDepartment> EmplToDepTable { get; set; } = default!;
        public DbSet<EmployeeToFunction> EmplToFunc { get; set; } = default!;
        public DbSet<EmployeeToTimeInterval> EmplToTimeInt { get; set; } = default!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
#if DEBUG
            optionsBuilder.EnableSensitiveDataLogging();
#endif
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
