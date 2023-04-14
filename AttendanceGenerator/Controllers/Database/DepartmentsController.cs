using AttendanceGenerator.Infrastructure.Logger;
using AttendanceGenerator.Model.Department;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceGenerator.Controllers.Database
{
    public class DepartmentsController
    {
        public static void AddDepartment(Department department)
        {
            using (ApplicationDbContext context = ApplicationDbContext.GetContext())
            {
                if (!context.Departments.Contains(department))
                {
                    context.Departments.Add(department);
                    context.SaveChanges();
                }
                else
                {
                    Logger.Log("1123", $"Отдел {department.Name}(ID = {department.Id}) уже существует");
                }
            }
        }
        public static void RemoveDepartment(Department department)
        {
            using (ApplicationDbContext context = ApplicationDbContext.GetContext())
            {
                if (context.Departments.Contains(department))
                {
                    context.Departments.Remove(department);
                    context.SaveChanges();
                }
                else
                {
                    Logger.Log("1124", $"Отдел {department.Name}(ID = {department.Id}) отсутствует в списке");
                }
            }
        }

        public static void EditDepartment(Department department)
        {
            using (ApplicationDbContext context = ApplicationDbContext.GetContext())
            {
                context.Departments.Update(department);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Возвращает отделы указанной организации.
        /// Если establishmentID = -1, то возвращаются все отделы всех организаций
        /// </summary>
        /// <param name="establishmentID"></param>
        /// <returns></returns>
        public static List<Department> GetDepartments(int establishmentID)
        {
            List<Department>? departments = null;
            using (ApplicationDbContext context = ApplicationDbContext.GetContext())
            {
                if (establishmentID == -1)
                    departments = context.Departments.ToList();
                else
                    departments = context.Departments.Where(department => department.EstablishmentId == establishmentID).Include(dep=>dep.Employees).ToList();
            }
            return departments != null ? departments : new List<Department>();
        }
        /// <summary>
        /// Возвращает все отделы всех организаций
        /// </summary>
        /// <returns></returns>
        public static List<Department> GetAllDepartments() => GetDepartments(-1);
    }
}
