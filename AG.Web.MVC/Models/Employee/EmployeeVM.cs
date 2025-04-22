namespace AG.Web.MVC.Models.Employee
{
    public record class EmployeeVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public float Rate { get; set; }

        public string Function { get; set; }
    }
}
