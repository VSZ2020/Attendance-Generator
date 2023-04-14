using AttendanceGenerator.Infrastructure.Logger;
using AttendanceGenerator.Model.Calendar.TimeInterval;
using AttendanceGenerator.Model.Department;
using AttendanceGenerator.Model.Employees;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AttendanceGenerator.Controllers.Database.Employee
{
    public class EmployeeController
    {
        /// <summary>
        /// Добаляет нового сотрудника
        /// </summary>
        /// <param name="employee">Сотрудник</param>
        public static void AddEmployee(AttendanceGenerator.Model.Employees.Employee employee)
        {
            using (ApplicationDbContext context = ApplicationDbContext.GetContext())
                if (!context.Employees.Contains(employee))
                {
                    context.Employees.Add(employee);
                    context.SaveChanges();
                }
                else
                {
                    Logger.Log("1123", $"Сотрудник {employee.FirstName} {employee.SecondName} уже есть в списке");
                }
        }
        /// <summary>
        /// Удаляет сотрудника из списка и базы
        /// </summary>
        /// <param name="employee">Сотрудник</param>
        public static void RemoveEmployee(AttendanceGenerator.Model.Employees.Employee employee)
        {
            using (ApplicationDbContext context = ApplicationDbContext.GetContext())
            {
                if (context.Employees.Contains(employee))
                {
                    context.Employees.Remove(employee);
                    context.SaveChanges();
                }
                else
                    Logger.Log("1124", $"Сотрудника {employee.FirstName} {employee.SecondName} с ID = {employee.Id} не существует");
            }
        }
        /// <summary>
        /// Обновляет данные сотрудника
        /// </summary>
        /// <param name="newEmployee"></param>
        /// <returns>True - обновлено удачно, False - при обновлении были ошибки</returns>
        public void UpdateEmployee(AttendanceGenerator.Model.Employees.Employee newEmployee)
        {
            using (ApplicationDbContext context = ApplicationDbContext.GetContext())
            {
                context.Employees.Update(newEmployee);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Возвращает список всех сотрудников отдела.
        /// Если departmentID = -1, то возвращаются все сотрудники из всех отделов
        /// </summary>
        /// <param name="departmentID">Идентификатор отдела</param>
        /// <returns></returns>
        public EmployeeList GetEmployees(int departmentID)
        {
            EmployeeList employees = new EmployeeList();
            using (ApplicationDbContext context = ApplicationDbContext.GetContext())
            {
                if (departmentID > -1)
                {
                    employees = new EmployeeList(context.Employees.Where(empl => empl.DepartmentId == departmentID).ToList());
                }
                else
                    employees = new EmployeeList(context.Employees.ToList());
            }
            return employees;
        }

        #region TimeIntervals region
        public static void AddTimeInterval(TimeInterval interval)
        {
            using (ApplicationDbContext context = ApplicationDbContext.GetContext())
            {
                if (!context.TimeIntervals.Contains(interval))
                {
                    context.TimeIntervals.Add(interval);
                    context.SaveChanges();
                }
                else
                    Logger.Log("1123", $"Интервал с параметрами {interval.IntervalName} [{interval.From.ToString()}; {interval.To.ToString()}] уже есть в списке");
            }
        }

        public static void RemoveTimeInterval(TimeInterval interval)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                if (context.TimeIntervals.Contains(interval))
                {
                    context.TimeIntervals.Remove(interval);
                    context.SaveChanges();
                }
                else
                    Logger.Log("1123", $"Интервала с параметрами {interval.IntervalName} [{interval.From.ToString()}; {interval.To.ToString()}] нет в списке");
            }
        }

        public static void UpdateTimeInterval(TimeInterval interval)
        {
            using (ApplicationDbContext context = ApplicationDbContext.GetContext())
            {
                context.TimeIntervals.Update(interval);
                context.SaveChanges();
            }
        }
        /// <summary>
        /// Возвращает список временных интервалов, принадлежащих данному сотруднику
        /// </summary>
        /// <param name="employeeID">Идентификатор сотрудника</param>
        /// <returns></returns>
        public static List<TimeInterval> GetEmployeeIntervals(int employeeID)
        {
            List<TimeInterval> intervals = null!;
            using (ApplicationDbContext context = ApplicationDbContext.GetContext())
            {
                intervals = context.TimeIntervals.Where(interval => interval.EmployeeID == employeeID).ToList();
            }
            return intervals != null ? intervals : new List<TimeInterval>();
        }
        #endregion
    }
}
