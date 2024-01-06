using Core.Database.Entities;

namespace Core.Converters
{
	public interface IEntityConverter<TEntity> where TEntity : BaseEntity
	{
		public TEntity ConvertToEntity();
	}
}
