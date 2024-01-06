using Core.Converters;
using Core.Database.Entities;

namespace Services.Domains
{
	public class EmployeeStatus : BaseDomain, IEntityConverter<EmployeeStatusEntity>
	{
		#region ctor
		public EmployeeStatus() { }
		public EmployeeStatus(EmployeeStatusEntity entity) { Id = entity.Id; Name = entity.Name; }
		#endregion

		public string Name { get; set; }

		#region IEntityConverter
		public EmployeeStatusEntity ConvertToEntity()
		{
			return new EmployeeStatusEntity()
			{
				Id = Id,
				Name = Name,
			};
		}
		#endregion
	}
}
