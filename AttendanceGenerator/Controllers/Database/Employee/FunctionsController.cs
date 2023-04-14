using AttendanceGenerator.Infrastructure.Logger;
using AttendanceGenerator.Model.Employees;
using AttendanceGenerator.Model.Employees.EmployeeFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceGenerator.Controllers.Database.Employee
{
    public class FunctionsController
    {

        public static void Add(Function func)
        {
            using (ApplicationDbContext context = ApplicationDbContext.GetContext())
                if (!context.EmployeeFunctions.Contains(func))
                {
                    context.EmployeeFunctions.Add(func);
                    context.SaveChanges();
                }
                else
                {
                    Logger.Log("1123", $"Должность {func.Name} уже есть в списке");
                }
        }

        public static void Remove(Function func)
        {
            using (ApplicationDbContext context = ApplicationDbContext.GetContext())
            {
                if (context.EmployeeFunctions.Contains(func))
                {
                    context.EmployeeFunctions.Remove(func);
                    context.SaveChanges();
                }
                else
                    Logger.Log("1124", $"Должности {func.Name} не существует");
            }
        }

        public void Update(Function func)
        {
            using (ApplicationDbContext context = ApplicationDbContext.GetContext())
            {
                context.EmployeeFunctions.Update(func);
                context.SaveChanges();
            }
        }

        public List<Function> GetAll()
        {
            List<Function> funcs = null!;
            using (ApplicationDbContext context = ApplicationDbContext.GetContext())
            {
                funcs = context.EmployeeFunctions.ToList();
            }
            return funcs != null ? funcs : new List<Function>();
        }
    }
}
