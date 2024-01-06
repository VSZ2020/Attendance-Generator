using Core.Converters;
using Core.Database.Entities;

namespace Services.Domains
{
	public class FunctionGroup : BaseDomain, IEntityConverter<FunctionGroupEntity>
	{
		#region ctor
		public FunctionGroup() { }
		public FunctionGroup(FunctionGroupEntity entity)
		{
			this.Id = entity.Id;
			this.GroupName = entity.GroupName;
		} 
		#endregion

		public string GroupName { get; set; }

		#region IEntityConverter
		public FunctionGroupEntity ConvertToEntity()
		{
			return new FunctionGroupEntity()
			{
				Id = this.Id,
				GroupName = this.GroupName,
			};
		} 
		#endregion
	}
}
