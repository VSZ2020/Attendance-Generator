using AttendanceGenerator.Model.Session.UserAccount;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceGenerator.Model.Department
{
    public class Department
    {
        /// <summary>
        /// Идентификатор отдела
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название отдела
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор учреждения, которому принадлежит отдел
        /// </summary>
        public int EstablishmentId { get; set; }
        /// <summary>
        /// Учреждение, которому принадлежит отдел
        /// </summary>
        //public Establishment.Establishment Establishment { get; set; }

        public List<Employees.Employee> Employees { get; set; }
        public List<UserAccount> UserAccounts { get; set; }

        public Department() { }
    }
}
