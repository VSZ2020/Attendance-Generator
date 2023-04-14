using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceGenerator.Model.Employees
{
    /// <summary>
    /// Описывает список сотрудников
    /// </summary>
    public class EmployeeList
    {
        private IList<Employee> _employees;

        public Employee? GetById(int ID)
        {
            foreach(var employee in _employees)
            {
                if (employee.Id == ID)
                    return employee;
            }
            return null;
        }

        public Employee this[int index]
        {
            get => _employees[index];
            set => _employees[index] = value;
        }

        public int Count { get => _employees.Count; }

        public EmployeeList() :this(new List<Employee>()) { }

        public EmployeeList(List<Employee> employees)
        {
            _employees = employees;
        }

        public void Add(Employee employee)
        {
            _employees.Add(employee);
        }

        public void Remove(Employee employee)
        {
            _employees.Remove(employee);
        }

        public void Clear() => _employees.Clear();

        public bool Contains(Employee employee)
            => _employees.Contains(employee);

        public EmployeeList Copy()
        {
            var list = new EmployeeList();
            foreach(var employee in _employees)
            {
                list.Add(employee);
            }
            return list;
        }

        public bool IsEqualNames(Employee employee1, Employee employee2)
        {
            return employee1.FirstName == employee2.FirstName &&
                employee1.SecondName == employee2.SecondName &&
                employee1.MiddleName == employee2.MiddleName;
        }

        public IList<Employee> ToList() => _employees;
    }
}
