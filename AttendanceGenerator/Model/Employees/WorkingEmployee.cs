using AttendanceGenerator.Model.Calendar.TimeInterval;
using AttendanceGenerator.Model.Employees.EmployeeFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceGenerator.Model.Employees
{
    public class WorkingEmployee: Employee
    {
        private List<TimeInterval> _timeIntervals;
        #region IWorkingEmployee
        public List<TimeInterval> EmployeeTimeIntervals { get; set; }
        #endregion IWorkingEmployee

        public WorkingEmployee(
            int id,
            string firstName,
            string secondName,
            string middleName,
            float rate,
            bool isConcurrentEmployee,
            int departmentId,
            bool isWorking,
            List<TimeInterval>? intervals)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.SecondName = secondName;
            this.MiddleName = middleName;
            this.Rate = rate;
            this.IsConcurrentWorker = isConcurrentEmployee;
            this.DepartmentId = departmentId;
            this.IsWorking = isWorking;
            this.EmployeeTimeIntervals = intervals ?? new List<TimeInterval>();
        }

        public WorkingEmployee(){ }
    }
}
