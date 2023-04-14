using AttendanceGenerator.Controllers.Database.ModelConfigurations;
using AttendanceGenerator.Model.Calendar.TimeInterval;
using AttendanceGenerator.Model.Calendar.WorkingWeek.TimeConfiguration;
using AttendanceGenerator.Model.Department;
using AttendanceGenerator.Model.Employees.EmployeeFunctions;
using AttendanceGenerator.Model.Establishment;
using AttendanceGenerator.Model.Session.Permissions;
using AttendanceGenerator.Model.Session.Role;
using AttendanceGenerator.Model.Session.UserAccount;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security;

namespace AttendanceGenerator.Controllers.Database
{
    public class ApplicationDbContext: DbContext
    {
        
        public DbSet<AttendanceGenerator.Model.Employees.Employee> Employees { get; set; } = null!;
        public DbSet<Function> EmployeeFunctions { get; set; } = null!;
        public DbSet<TimeInterval> TimeIntervals { get; set; } = null!;

        public DbSet<Establishment> Establishments { get; set; } = null!;
        public DbSet<Department> Departments { get; set; } = null!;


        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Permission> Permissions { get; set; } = null!;
        public DbSet<UserAccount> Accounts { get; set; } = null!;

        public ApplicationDbContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DBUtils.DB_DATA_NAME}.db");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TimeInterval>().ToTable("TimeIntervals");
            builder.Entity<Model.Employees.Employee>().ToTable("Employees");
            builder.Entity<Function>().ToTable("Functions");
            builder.Entity<FunctionGroup>().ToTable("FunctionGroups");
            builder.Entity<Department>().ToTable("Departments");
            builder.Entity<Establishment>().ToTable("Establishments");

            builder.Entity<Role>().ToTable("Roles");
            builder.Entity<Permission>().ToTable("Permissions");
           
            builder.ApplyConfiguration(new EstablishmentEntityConfig());
            builder.ApplyConfiguration(new EmployeeEntityConfig());

            //Пользователи по-умолчанию
            builder.ApplyConfiguration(new UserAccountEntityConfig());

            //Должности
            builder.ApplyConfiguration(new FunctionGroupEntityConfiguration());
            builder.ApplyConfiguration(new FunctionEntityConfig());

            //Разрешения
            builder.ApplyConfiguration(new PermissionsEntityConfig());

            //Роли пользователя
            builder.ApplyConfiguration(new RolesEntityConfig());

            //var admin_perms = new List<Permission>()
            //{
            //        perm_permissionsView,
            //        perm_permissionsEdit,
            //        perm_employeesView,
            //        perm_employeeAdd,
            //        perm_employeeEdit,
            //        perm_employeeRemove,
            //        perm_EstablishmentsView,
            //        perm_EstablishmentAdd,
            //        perm_EstablishmentEdit,
            //        perm_EstablishmentRemove,
            //        perm_departmentsView,
            //        perm_departmentAdd,
            //        perm_departmentEdit,
            //        perm_departmentRemove,
            //        perm_functionsView,
            //        perm_functionAdd,
            //        perm_functionEdit,
            //        perm_functionRemove
            //};
            
            //var moderator_perms = new List<Permission>()
            //    {
            //        perm_permissionsView,
            //        perm_employeesView,
            //        perm_employeeAdd,
            //        perm_employeeEdit,
            //        perm_employeeRemove,
            //        perm_EstablishmentsView,
            //        perm_EstablishmentAdd,
            //        perm_EstablishmentEdit,
            //        perm_EstablishmentRemove,
            //        perm_departmentsView,
            //        perm_departmentAdd,
            //        perm_departmentEdit,
            //        perm_departmentRemove,
            //        perm_functionsView,
            //        perm_functionAdd,
            //        perm_functionEdit,
            //        perm_functionRemove
            //    };
            //var user_perms = new List<Permission>()
            //    {
            //        perm_employeesView,
            //        perm_employeeAdd,
            //        perm_employeeEdit,
            //        perm_employeeRemove,
            //        perm_EstablishmentsView,
            //        perm_EstablishmentAdd,
            //        perm_EstablishmentEdit,
            //        perm_departmentsView,
            //        perm_departmentAdd,
            //        perm_departmentEdit,
            //        perm_departmentRemove,
            //        perm_functionsView,
            //        perm_functionAdd,
            //        perm_functionEdit,
            //        perm_functionRemove
            //    };
            //var guest_perms = new List<Permission>()
            //    {
            //        perm_employeesView,
            //        perm_EstablishmentsView,
            //        perm_departmentsView,
            //        perm_functionsView
            //    };
            //admin.Permissions.AddRange(admin_perms);
            builder.Entity<TimeInterval>().Property(u => u.IntervalName).HasColumnName("name").IsRequired();
            //builder.Entity<Role>().
            //    HasMany(r => r.Permissions).
            //    WithMany(p => p.Roles).
            //    UsingEntity<RolePermission>(
            //        r => r.HasOne(rp=>rp.Permission).WithMany(t => t.Roles).HasForeignKey(pt=>pt.RoleId),
            //    );

        }
        public static ApplicationDbContext GetContext()
        {
            return new ApplicationDbContext();
        }

        public void RecreateDatabase()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}
