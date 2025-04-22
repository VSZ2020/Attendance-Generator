using AG.Data.Entities;
using AG.Data.Entities.RelationshipTables;
using Microsoft.EntityFrameworkCore;

namespace AG.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> builder): base(builder)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<EmployeeEntity> Employees { get; set; } = default!;
        public DbSet<EstablishmentEntity> Establishments { get; set; } = default!;
        public DbSet<DepartmentEntity> Departments { get; set; } = default!;
        public DbSet<TimeIntervalEntity> TimeIntervals { get; set; } = default!;
        public DbSet<BusinessTripEntity> BusinessTrips { get; set; } = default!;
        public DbSet<FunctionEntity> Functions { get; set; } = default!;


        public DbSet<TimesheetEntity> Timesheets { get; set; } = default!;
        public DbSet<TimesheetDefaults> TimesheetDefaults { get; set; } = default!;
        public DbSet<CorrectionDayEntity> CorrectionDays { get; set;} = default!;
        public DbSet<ScheduleEntity> Schedules { get; set;} = default!;
        public DbSet<ScheduleDayEntity> ScheduleDays { get; set;} = default!;

        public DbSet<UserEntity> Users { get; set; } = default!;
        

        //Relationship tables
        public DbSet<EmployeeToDepartment> EmplToDepTable { get; set; } = default!;
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

            //Employee → Department relationship
            //modelBuilder.Entity<EmployeeEntity>()
            //    .HasMany(e => e.Departments)
            //    .WithMany(e => e.Employees)
            //    .UsingEntity<EmployeeToDepartment>(
            //    j => j
            //        .HasOne(t => t.Department)
            //        .WithMany(t => t.EmployeeToDepartmentTable)
            //        .HasForeignKey(t => t.DepartmentId),
            //    j => j
            //        .HasOne(t => t.Employee)
            //        .WithMany(t => t.EmployeeToDepartmentTable)
            //        .HasForeignKey(t => t.EmployeeId),
            //    j =>
            //    {
            //        j.ToTable(nameof(EmployeeToDepartment));
            //        j.HasKey(t => new { t.EmployeeId, t.DepartmentId });
            //    });

            //https://stackoverflow.com/questions/38553853/modeling-a-many-to-many-relationship-between-3-tables-with-code-first
            modelBuilder.Entity<EmployeeToDepartment>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<EmployeeToDepartment>()
                .HasOne(e => e.Employee)
                .WithMany(e => e.EmployeeToDepartmentTable)
                .HasForeignKey(e => e.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EmployeeToDepartment>()
                .HasOne(e => e.Department)
                .WithMany(e => e.EmployeeToDepartmentTable)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EmployeeToDepartment>()
                .HasOne(e => e.Schedule)
                .WithMany(e => e.EmployeeToDepartmentTable)
                .HasForeignKey(e => e.ScheduleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EmployeeToDepartment>()
                .HasOne(e => e.Function)
                .WithMany(e => e.EmployeeToDepartmentTable)
                .HasForeignKey(e => e.FunctionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EmployeeToTimeInterval>()
                .HasOne(e => e.Employee)
                .WithMany(e => e.EmployeeToTimeIntervalTable)
                .HasForeignKey(e => e.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
            //Employee → Function relationship
            //modelBuilder.Entity<EmployeeEntity>()
            //    .HasMany(e => e.Functions)
            //    .WithMany(e => e.Employees)
            //    .UsingEntity<EmployeeToFunction>(
            //    j => j
            //        .HasOne(t => t.Function)
            //        .WithMany(t => t.EmployeeToFunctionTable)
            //        .HasForeignKey(t => t.FunctionId),
            //    j => j
            //        .HasOne(t => t.Employee)
            //        .WithMany(t => t.EmployeeToFunctionTable)
            //        .HasForeignKey(t => t.EmployeeId),
            //    j =>
            //    {
            //        j.Property(p => p.Rate).HasDefaultValue(1F);
            //        j.Property(p => p.AssignmentDate).HasDefaultValue(DateTime.Now);
            //        j.ToTable(nameof(EmployeeToFunction));
            //        j.HasKey(t => new { t.EmployeeId, t.FunctionId });
            //    });

            //Employee → TimeInterval relationship
            /*modelBuilder.Entity<EmployeeEntity>()
                .HasMany(e => e.TimeIntervals)
                .WithMany(e => e.Employees)
                .UsingEntity<EmployeeToTimeInterval>(
                j => j
                    .HasOne(t => t.TimeInterval)
                    .WithMany(t => t.EmployeeToTimeIntervalTable)
                    .HasForeignKey(t => t.TimeIntervalId),
                j => j
                    .HasOne(t => t.Employee)
                    .WithMany(t => t.EmployeeToTimeIntervalTable)
                    .HasForeignKey(t => t.EmployeeId),
                j =>
                {
                    j.ToTable(nameof(EmployeeToTimeInterval));
                    j.HasKey(t => new { t.EmployeeId, t.TimeIntervalId });
                });*/


            modelBuilder.Entity<TimeIntervalEntity>().HasData(Defaults.DefaultEntities.TimeIntervals());
            modelBuilder.Entity<EstablishmentEntity>().HasData(Defaults.DefaultEntities.Establishments());
            modelBuilder.Entity<DepartmentEntity>().HasData(Defaults.DefaultEntities.Departments());
            modelBuilder.Entity<EmployeeEntity>().HasData(Defaults.DefaultEntities.Employees());
            modelBuilder.Entity<FunctionEntity>().HasData(Defaults.DefaultEntities.Functions());
            modelBuilder.Entity<TimesheetDefaults>().HasData(Defaults.DefaultEntities.TimesheetDefaults());
            modelBuilder.Entity<CorrectionDayEntity>().HasData(Defaults.DefaultEntities.CorrectionDays());

            modelBuilder.Entity<UserEntity>().HasData(Defaults.DefaultEntities.Users());

            modelBuilder.Entity<ScheduleEntity>().HasData(Defaults.DefaultEntities.Schedules());
            modelBuilder.Entity<ScheduleDayEntity>().HasData(Defaults.DefaultEntities.SchedulesDays());

            modelBuilder.Entity<EmployeeToDepartment>().HasData(Defaults.DefaultEntities.EmployeeWithDepartment());
            modelBuilder.Entity<EmployeeToTimeInterval>().HasData(Defaults.DefaultEntities.EmployeeWithTimeInterval());
        }
    }
}
