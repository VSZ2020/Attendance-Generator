namespace Services.POCO
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int EstablishmentId { get; set; }
        public IList<Employee>? Employees {get; set;}
        public int EmployeesCount { get => Employees?.Count ?? 0; }
    }
}
