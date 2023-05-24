namespace Services.POCO
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = "";
        public string MiddleName { get; set; } = "";
        public string LastName { get; set; } = "";

        public string ShortName => string.Join("", LastName, " ", FirstName[0], ". ", MiddleName[0], ".");

        public string Function { get; set; } = "";

        public float Rate { get; set; } = 0f;

        public bool IsConcurrent { get; set; }

        public string Department { get; set; } = "";
        public string PhoneNumber { get; set; } = "";

        public string Status { get; set; } = "";
    }
}
