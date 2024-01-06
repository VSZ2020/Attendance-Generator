using Core.Converters;
using Core.Database.Entities;

namespace Services.Domains
{
	public class TimeIntervalType : BaseDomain, IEntityConverter<TimeIntervalTypeEntity>
	{
		#region ctor
		public TimeIntervalType() { }
		public TimeIntervalType(TimeIntervalTypeEntity entity)
		{
			Id = entity.Id;
			LongName = entity.Name;
			ShortName = entity.ShortName;
			Description = entity.Description;
		}
		#endregion

		public string LongName { get; set; }
		public string ShortName { get; set; }
		public string Description { get; set; }

		#region IEntityConverter
		public TimeIntervalTypeEntity ConvertToEntity()
		{
			return new TimeIntervalTypeEntity()
			{
				Id = Id,
				Name = LongName,
				ShortName = ShortName,
				Description = Description,
			};
		}
		#endregion
	}
}
