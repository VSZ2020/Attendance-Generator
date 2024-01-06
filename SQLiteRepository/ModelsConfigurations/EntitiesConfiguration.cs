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
			builder.HasKey(e => e.Id);
            builder
                .HasMany(e => e.TimeIntervals)
                .WithOne(t => t.Employee)
                .HasForeignKey(t => t.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

			builder
                .ToTable("employees")
                .HasData(DefaultEntitiesProvider.GetDefaultEmployees());
        }
    }

    /// <summary>
    /// Настройка организаций
    /// </summary>
    public class EstablishmentConfig : IEntityTypeConfiguration<EstablishmentEntity>
    {
        public void Configure(EntityTypeBuilder<EstablishmentEntity> builder)
        {
            builder.HasKey(e => e.Id);
            builder
                .HasMany(est => est.Departments)
                .WithOne(dep => dep.Establishment)
                .HasForeignKey(dep => dep.EstablishmentId)
                .OnDelete(DeleteBehavior.Cascade);
            builder
                .HasMany(e => e.CorrectionDays)
                .WithOne(e => e.Establishment)
                .HasForeignKey(e => e.EstablishmentId)
                .OnDelete(DeleteBehavior.Cascade);
            builder
                .ToTable("establishments")
                .HasData(DefaultEntitiesProvider.GetDefaultEstablishments());
        }
    }

    /// <summary>
    /// Настройка подразделений
    /// </summary>
    public class DepartmentConfig : IEntityTypeConfiguration<DepartmentEntity>
    {
        public void Configure(EntityTypeBuilder<DepartmentEntity> builder)
        {
			builder.HasKey(e => e.Id);
			builder
                .HasMany(dep => dep.Employees)
                .WithOne(emp => emp.Department)
                .HasForeignKey(emp => emp.DepartmentId);
            builder
                .ToTable("departments")
                .HasData(DefaultEntitiesProvider.GetDefaultDepartments());
        }
    }

    /// <summary>
    /// Настройка категорий должностей
    /// </summary>
    public class FunctionGroupConfig : IEntityTypeConfiguration<FunctionGroupEntity>
    {
        public void Configure(EntityTypeBuilder<FunctionGroupEntity> builder)
        {
			builder.HasKey(e => e.Id);
			builder
                .HasMany(group => group.Functions)
                .WithOne(func => func.Group)
                .HasForeignKey(func => func.FunctionGroupId);
            builder
                .ToTable("function_groups")
                .HasData(DefaultEntitiesProvider.GetDefaultFunctionGroups());
        }
    }

    /// <summary>
    /// Настройка должностей
    /// </summary>
    public class FunctionConfig : IEntityTypeConfiguration<FunctionEntity>
    {
        public void Configure(EntityTypeBuilder<FunctionEntity> builder)
        {
			builder.HasKey(e => e.Id);
			builder
                .HasMany(f => f.Employees)
                .WithOne(e => e.Function)
                .HasForeignKey(e => e.FunctionId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .ToTable("functions")
                .HasData(DefaultEntitiesProvider.GetDefaultFunctions());
        }
    }

    public class EmoployeeStatusConfig : IEntityTypeConfiguration<EmployeeStatusEntity>
    {
        public void Configure(EntityTypeBuilder<EmployeeStatusEntity> builder)
        {
			builder.HasKey(e => e.Id);
			builder
                .HasMany(s => s.Employees)
                .WithOne(e => e.Status)
                .HasForeignKey(e => e.StatusId);
            builder
                .ToTable("employee_statuses")
                .HasData(DefaultEntitiesProvider.GetDefaultEmployeeStatuses());
        }
    }

    /// <summary>
    /// Настройка сущностей типов временных интервалов
    /// </summary>
    public class TimeIntervalTypesConfig : IEntityTypeConfiguration<TimeIntervalTypeEntity>
    {
        public void Configure(EntityTypeBuilder<TimeIntervalTypeEntity> builder)
        {
			builder.HasKey(e => e.Id);
            builder
                .HasMany(ti => ti.TimeIntervals)
                .WithOne(ti => ti.IntervalType)
                .HasForeignKey(ti => ti.IntervalTypeId);
                
            builder
                .ToTable("time_interval_types")
                .HasData(DefaultEntitiesProvider.GetDefaultTimeIntervalTypes());
        }
    }

    /// <summary>
    /// Настройка сущностей временных интервалов
    /// </summary>
    public class TimeIntervalsConfig : IEntityTypeConfiguration<TimeIntervalEntity>
    {
        public void Configure(EntityTypeBuilder<TimeIntervalEntity> builder)
        {
			builder.HasKey(e => e.Id);
			builder
                .ToTable("time_intervals");
                //.HasData(TimeIntervalEntity.GetDefault());
        }
    }


	public class CorrectionDaysConfig : IEntityTypeConfiguration<CorrectionDayEntity>
	{
		public void Configure(EntityTypeBuilder<CorrectionDayEntity> builder)
		{
			builder.HasKey(e => e.Id);
			builder.ToTable("correction_days");
			//.HasData(TimeIntervalEntity.GetDefault());
		}
	}
}
