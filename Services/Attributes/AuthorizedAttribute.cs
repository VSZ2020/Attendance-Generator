namespace Services.Attributes
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public class AuthorizedAttribute: Attribute
	{
		public string? Roles;
		public string? Description;
		public AuthorizedAttribute() { }
	}
}
