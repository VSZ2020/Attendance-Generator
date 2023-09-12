namespace Services.Domains
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

		#region ctor
		public UserAccount(int id, string username, string login)
		{
			Id = id;
			Username = username;
			Login = login;
		}

		public UserAccount(string username) : this(0, username, username) { }

		public UserAccount(): this(0, "", "") { }
		#endregion
	}
}
