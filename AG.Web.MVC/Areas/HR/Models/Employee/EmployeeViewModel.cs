using AG.Services.Utils;

namespace AG.Web.MVC.Areas.HR.Models.Employee
{
    public record class EmployeeViewModel
    {
        public Guid Id { get; set; }
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string FullName => NameUtils.ToLongName(LastName, FirstName, MiddleName);
        public string ShortName => NameUtils.ToShortName(LastName, FirstName, MiddleName);

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public float Rate { get; set; }

        public string Function { get; set; }
    }
}
