using AG.Data;
using Microsoft.EntityFrameworkCore;

namespace AG.Services.Repository
{
    public class EmployeeFunctionService
    {
        public EmployeeFunctionService(DataContext ctx)
        {
            _context = ctx;
        }

        readonly DataContext _context;

        /// <summary>
        /// Checks if the employee has a position in the selected department
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="departmentId"></param>
        /// <param name="functionId"></param>
        /// <returns></returns>
        public bool ValidateEmployeeFunction(Guid employeeId, Guid departmentId, Guid functionId)
        {
            var entity = _context.EmplToDepTable.AsNoTracking().FirstOrDefault(e => e.EmployeeId == employeeId && e.DepartmentId == departmentId && e.FunctionId == functionId);
            return entity is null;
        }

        /// <summary>
        /// Checks if the employee has a position in the selected department
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="departmentId"></param>
        /// <param name="functionId"></param>
        /// <param name="id"></param>
        /// <returns>Returns empty string  or null if no validation errors</returns>
        public async Task<string?> ValidateEmployeeFunctionAsync(Guid employeeId, Guid departmentId, Guid functionId, Guid scheduleId, DateTime assignmentDate, DateTime? firedDate, Guid? id = null)
        {
            var entities = await _context.EmplToDepTable
                .AsNoTracking()
                .Include(e => e.Function)
                .Where(e => e.EmployeeId == employeeId && e.DepartmentId == departmentId && e.FunctionId == functionId)
                .Where(e => id != null ? e.Id != id : true)
                .OrderByDescending(e => e.AssignmentDate)
                .ToListAsync();

            if (entities.Count > 0)
            {
                var lastFunction = entities.First();
                if (lastFunction.FiredDate == null && lastFunction.AssignmentDate < assignmentDate)
                    return $"Сотрудник еще работает на должности '{lastFunction.Function!.Name}'. Сначала установите для неё срок окончания работы, затем задайте новую должность с датой назначения после даты последнего увольнения или перевода";
                if (lastFunction.AssignmentDate.Date == assignmentDate.Date)
                    return "Даты назначения на одну и ту же должность в подразделении не должны совпадать. Выберите другую дату назначения на должность.";
                if (lastFunction.AssignmentDate.Date > assignmentDate.Date)
                    return "Сотрудник уже работает на данной должности";
                

            }
            return null;
        }

        public async Task<bool> IsLastFunction(Guid Id, Guid employeeId, Guid departmentId, Guid functionId)
        {
            var entities = await _context.EmplToDepTable
                .AsNoTracking()
                .Where(e => e.EmployeeId == employeeId && e.DepartmentId == departmentId && e.FunctionId == functionId)
                .OrderByDescending(e => e.AssignmentDate)
                .ToListAsync();

            if (entities.Count > 0 && entities.First().Id != Id)
            {
                return false;
            }
            return true;
        }
    }
}
