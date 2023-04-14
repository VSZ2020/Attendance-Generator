using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceGenerator.Model.Session.Permissions
{
    public static class AppPermissions
    {
        /*
         * Разрешения для сотрудников
         */
        public const string READ_EMPLOYEES          = "employees_view";
        public const string EMPLOYEE_ADD            = "employee_add";
        public const string EMPLOYEE_EDIT           = "employee_edit";
        public const string EMPLOYEE_REMOVE         = "employee_remove";

        public const string READ_ESTABLISHMENTS     = "establishments_view";
        public const string ESTABLISHMENT_ADD       = "establishment_add";
        public const string ESTABLISHMENT_EDIT      = "establishment_edit";
        public const string ESTABLISHMENT_REMOVE    = "establishment_remove";

        /*
         * Разрешения для отделов
         */
        public const string READ_DEPARTMENTS        = "departments_view";
    }
}
