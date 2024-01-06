using Core.Converters;
using Core.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Services.Database
{
	public abstract class BaseDatabaseService<TContext> where TContext : DbContext, new()
	{
		public BaseDatabaseService(TContext? context = null)
		{
			this.Context = context == null ? new TContext(): context;
		}

		public TContext Context { get; set; }

		public async Task<bool> AddAsync<TObject, TEntity>(TObject obj) 
			where TObject: IEntityConverter<TEntity> 
			where TEntity: BaseEntity
		{
			var entity = obj.ConvertToEntity();
			await Context.Set<TEntity>().AddAsync(entity);
			var changesCount = await Context.SaveChangesAsync();

			return changesCount > 0;
		}


		public async Task<bool> Update<TObject, TEntity>(TObject obj)
			where TObject : IEntityConverter<TEntity>
			where TEntity : BaseEntity
		{
			var entity = obj.ConvertToEntity();
			Context.Set<TEntity>().Update(entity);
			var changesCount = await Context.SaveChangesAsync();

			return changesCount > 0;
		}

		public async Task<bool> Delete<TObject, TEntity>(TObject obj)
			where TObject : IEntityConverter<TEntity>
			where TEntity : BaseEntity
		{
			var entity = obj.ConvertToEntity();
			Context.Set<TEntity>().Remove(entity);
			var changesCount = await Context.SaveChangesAsync();

			return changesCount > 0;
		}

		public async Task<bool> DeleteById<TObject, TEntity>(Guid id)
			where TObject : IEntityConverter<TEntity>
			where TEntity : BaseEntity
		{

			var entity = Context.Set<TEntity>().SingleOrDefault(e => e.Id == id);
			if (entity == null)
				return false;

			Context.Set<TEntity>().Remove(entity);
			var changesCount = await Context.SaveChangesAsync();

			return changesCount > 0;
		}
	}
}
