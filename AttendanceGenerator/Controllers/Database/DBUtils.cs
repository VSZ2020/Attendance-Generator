using AttendanceGenerator.Forms;
using AttendanceGenerator.Model.Calendar.WorkingWeek.TimeConfiguration;
using AttendanceGenerator.Model.Department;
using AttendanceGenerator.Model.Establishment;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace AttendanceGenerator.Controllers.Database
{
    public class DBUtils
    {
        public const string DB_DATA_NAME = "data";
        public const string DB_PERMISSIONS_NAME = "perms";
        /*
         * Таблицы данных
         */
        public const string TABLE_EMPLOYEES = "employees";
        public const string TABLE_EMPLOYEE_INTERVALS = "time_intervals";
        public const string TABLE_PERMISSIONS_LIST = "permissions";
        public const string TABLE_ROLE_LIST = "roles";
        public const string TABLE_ROLE_PERMISSION = "role_perm";
        /// <summary>
        /// Название таблицы со списком групп
        /// </summary>
        public const string TABLE_FUNCTION_GROUPS = "func_groups";
        /// <summary>
        /// Название таблицы со списком должностей и принадлежностью к группам
        /// </summary>
        public const string TABLE_FUNCTIONS_LIST = "func_list";

        /*
         * Данные авторизации в БД
         */
        public const string USERID = "admin";
        public const string PASSWORD = "admin";

        /// <summary>
        /// Проверяет базу данных на наличие значений по-умолчанию
        /// </summary>
        public static void CheckDatabase()
        {
            //Stopwatch timer = new Stopwatch();
            //timer.Start();

            using(ApplicationDbContext ctx = ApplicationDbContext.GetContext())
            {
                ctx.RecreateDatabase();
                var wtc = new WeekTimeConfiguration(StandartWeekHours.StandartHours.ToHoursString());
                var est = new Establishment()
                {
                    Name = "Establishment 1",
                    WeekConfiguration = wtc
                };
                var dep = new Model.Department.Department() { EstablishmentId = 1, Name = "Department 1" };
                var dep2 = new Model.Department.Department() { EstablishmentId = 1, Name = "Department 2" };
                est.Departments = new System.Collections.Generic.List<Department>() { dep, dep2 };
                ctx.Establishments.Add(est);
                ctx.Departments.Add(dep);
                ctx.Departments.Add(dep2);
                ctx.SaveChanges();
            }

            
            //timer.Stop();
            //MessageBox.Show($"Данные успешно загружены за {timer.Elapsed} сек");
        }
    }
}
