using Core.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SQLiteRepository.ModelsConfigurations
{
    /// <summary>
    /// Настройка таблицы сотрудников
    /// </summary>
    public class EmployeesConfiguration : IEntityTypeConfiguration<EmployeeEntity>
    {
        public void Configure(EntityTypeBuilder<EmployeeEntity> builder)
        {

            builder
                .ToTable("employees")
                .HasData(EmployeeEntity.GetDefault());
        }
    }

    /// <summary>
    /// Настройка организаций
    /// </summary>
    public class EstablishmentConfig : IEntityTypeConfiguration<EstablishmentEntity>
    {
        public void Configure(EntityTypeBuilder<EstablishmentEntity> builder)
        {
            builder
                .HasMany(est => est.Departments)
                .WithOne(dep => dep.Establishment)
                .HasForeignKey(dep => dep.EstablishmentId)
                .OnDelete(DeleteBehavior.Cascade);
            builder
                .ToTable("establishments")
                .HasData(EstablishmentEntity.GetDefault());
        }
    }

    /// <summary>
    /// Настройка подразделений
    /// </summary>
    public class DepartmentConfig : IEntityTypeConfiguration<DepartmentEntity>
    {
        public void Configure(EntityTypeBuilder<DepartmentEntity> builder)
        {
            builder
                .HasMany(dep => dep.Employees)
                .WithOne(emp => emp.Department)
                .HasForeignKey(emp => emp.DepartmentId);
            builder
                .ToTable("departments")
                .HasData(DepartmentEntity.GetDefault());
        }
    }

    /// <summary>
    /// Настройка категорий должностей
    /// </summary>
    public class FunctionGroupConfig : IEntityTypeConfiguration<FunctionGroupEntity>
    {
        public void Configure(EntityTypeBuilder<FunctionGroupEntity> builder)
        {
            builder
                .HasMany(group => group.Functions)
                .WithOne(func => func.Group)
                .HasForeignKey(func => func.FunctionGroupId);
            builder
                .ToTable("function_groups")
                .HasData(FunctionGroupEntity.GetDefault());
        }
    }

    /// <summary>
    /// Настройка должностей
    /// </summary>
    public class FunctionConfig : IEntityTypeConfiguration<FunctionEntity>
    {
        public void Configure(EntityTypeBuilder<FunctionEntity> builder)
        {
            builder
                .HasMany(f => f.Employees)
                .WithOne(e => e.Function)
                .HasForeignKey(e => e.FunctionId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .ToTable("functions")
                .HasData(FunctionEntity.GetDefault());
        }
    }

    public class EmoployeeStatusConfig : IEntityTypeConfiguration<EmployeeStatusEntity>
    {
        public void Configure(EntityTypeBuilder<EmployeeStatusEntity> builder)
        {
            builder
                .HasMany(s => s.Employees)
                .WithOne(e => e.Status)
                .HasForeignKey(e => e.StatusId);
            builder
                .ToTable("employee_statuses")
                .HasData(EmployeeStatusEntity.GetDefault());
        }
    }

    /// <summary>
    /// Настройка сущностей типов временных интервалов
    /// </summary>
    public class TimeIntervalTypesConfig : IEntityTypeConfiguration<TimeIntervalTypeEntity>
    {
        public void Configure(EntityTypeBuilder<TimeIntervalTypeEntity> builder)
        {
            builder
                .HasMany(ti => ti.TimeIntervals)
                .WithOne(ti => ti.IntervalType)
                .HasForeignKey(ti => ti.IntervalTypeId);
                
            builder
                .ToTable("time_interval_types")
                .HasData(TimeIntervalTypeEntity.GetDefault());
        }
    }

    /// <summary>
    /// Настройка сущностей временных интервалов
    /// </summary>
    public class TimeIntervalsConfig : IEntityTypeConfiguration<TimeIntervalEntity>
    {
        public void Configure(EntityTypeBuilder<TimeIntervalEntity> builder)
        {
            builder
                .ToTable("time_intervals");
                //.HasData(TimeIntervalEntity.GetDefault());
        }
    }
}
