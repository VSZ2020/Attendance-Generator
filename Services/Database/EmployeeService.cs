using Core;
using Core.Database.Entities;
using Services.POCO;

namespace Services.Database
{
    public class EmployeeService
    {
        private readonly IRepository<EmployeeEntity> employeeRepository;
        private readonly IRepository<FunctionEntity> functionsRepository;
        private readonly IRepository<DepartmentEntity> departmentsRepository;

        public EmployeeService(
            IRepository<EmployeeEntity> employeeRep,
            IRepository<FunctionEntity> funcRep,
            IRepository<DepartmentEntity> depsRep)
        {
            employeeRepository = employeeRep;
            this.departmentsRepository = depsRep;
            this.functionsRepository = funcRep;
        }

        public IList<Employee> GetEmployees(int departmentId = 0)
        {
            IList<EmployeeEntity> employees;
            if (departmentId > 0)
                employees = employeeRepository.GetAll(e => e.DepartmentId == departmentId);
            else
                employees = employeeRepository.GetAll(null);
            return employees.Select(empl => new Employee()
            {
                Id = empl.Id,
                FirstName = empl.FirstName,
                LastName = empl.LastName,
                MiddleName = empl.MiddleName, 
                Rate = empl.Rate
            }).ToList();
        }
    }
}
