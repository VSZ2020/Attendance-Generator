using Core.Converters;
using Core.Database.Entities;

namespace Services.Domains
{
	public class EmployeeFunction : BaseDomain, IEntityConverter<FunctionEntity>
	{
		public EmployeeFunction() { }
		public EmployeeFunction(FunctionEntity entity)
		{
			Id = entity.Id;
			Name = entity.Name;
			ShortName = entity.ShortName;
			Description = entity.Description;
			FunctionGroupId = entity.FunctionGroupId;
		}

		public string Name { get; set; }
		public string ShortName { get; set; }
		public string Description {  get; set; }

		public Guid FunctionGroupId { get; set; }

		/// <summary>
		/// Группа, к которой относится должность
		/// </summary>
		public FunctionGroup? FunctionGroup { get; set; }

		#region IEntityConverter
		public FunctionEntity ConvertToEntity()
		{
			return new FunctionEntity()
			{
				Id = Id,
				Name = Name,
				ShortName = ShortName,
				Description = this.Description,
				FunctionGroupId = FunctionGroupId,
			};
		}
		#endregion
	}
}
