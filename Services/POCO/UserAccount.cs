using Core.Security;

namespace Services.POCO
{
    public class UserAccount
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Login { get; set; }
        public DateTime SessionExpiredAt { get; set; }

        public string? Roles { get; set; }
        public int DepartmentId { get; set; }
        public List<int> DepartmentsIds { get; set; }
    }
}
