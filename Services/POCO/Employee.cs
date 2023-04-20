namespace Services.POCO
{
    public class Employee
    {
        public int Id;
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public string ShortName => string.Join("", LastName, " ", FirstName[0], ". ", MiddleName[0], ".");

        public EmployeeFunction Function { get; set; }

        public float Rate { get; set; }

        public bool IsConcurrent { get; set; }

        public Department Department { get; set; }
        public string PhoneNumber { get; set; }

        public EmployeeStatus Status { get; set; }
    }
}
