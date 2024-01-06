using Core.Converters;
using Core.Database.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Services.Domains
{
	public class Department : BaseDomain, IEntityConverter<DepartmentEntity>
	{
		#region ctor
		public Department()
		{
		}
		public Department(DepartmentEntity entity)
		{
			Id = entity.Id;
			Name = entity.Name;
			EstablishmentId = entity.EstablishmentId;
		}
		#endregion

		[Display(Name = "Название подразделения")]
		public string Name { get; set; }

		[Display(Name = "Количество сотрудников")]
		public int EmployeesCount { get; set; }
		public Guid EstablishmentId { get; set; }

		[Display(Name = "Руководитель подразделения")]
		public Guid HeadOfLabId { get; set; } = Guid.Empty;

		#region IEntityConverter
		public DepartmentEntity ConvertToEntity()
		{
			return new DepartmentEntity()
			{
				Id = Id,
				Name = Name,
				EstablishmentId = EstablishmentId,
				HeadOfLabId = HeadOfLabId,
			};
		}
		#endregion
	}
}
