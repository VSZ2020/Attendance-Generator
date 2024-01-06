using Core.Converters;
using Core.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Linq.Expressions;

namespace SQLiteRepository
{
	public interface ItemsRepository { }
    /*
     * Шаблон класса основан на примере из https://stackoverflow.com/questions/53469498/difference-between-dbsett-property-and-sett-function-in-ef-core
     */
    public interface IItemsRepository<TContext>: ItemsRepository where TContext : DbContext, new()
    {
		#region AddEntity
		public bool AddEntity<TObject, TEntity>(TObject obj) where TEntity : BaseEntity where TObject : IEntityConverter<TEntity>
        {
            var entity = obj.ConvertToEntity();
            using var Context = new TContext();
            Context.Set<TEntity>().Add(entity);
            var num = Context.SaveChanges();

            return num > 0;
        } 
        #endregion

        #region AddEntityAsync
        public async Task<bool> AddEntityAsync<TObject, TEntity>(TObject obj) where TEntity : BaseEntity where TObject : IEntityConverter<TEntity>
        {
            var entity = obj.ConvertToEntity();
            using var Context = new TContext();
            await Context.Set<TEntity>().AddAsync(entity);
            var num = await Context.SaveChangesAsync();

            return num > 0;
        }
		#endregion

        #region AddEntities
        public bool AddEntities<TObject, TEntity>(IEnumerable<TObject> objs) where TEntity : BaseEntity where TObject : IEntityConverter<TEntity>
        {
            var entities = objs.Select(obj => obj.ConvertToEntity()).ToList();

            using var Context = new TContext();
            Context.Set<TEntity>().AddRange(entities);
            var num = Context.SaveChanges();

            return num > 0;
        } 
        #endregion

		#region AddEntitiesAsync
		public async Task<bool> AddEntitiesAsync<TObject, TEntity>(IEnumerable<TObject> objs) where TEntity : BaseEntity where TObject : IEntityConverter<TEntity>
        {
            var entities = objs.Select(obj => obj.ConvertToEntity()).ToList();

            using var Context = new TContext();
            await Context.Set<TEntity>().AddRangeAsync(entities);
            var num = await Context.SaveChangesAsync();

            return num > 0;
        } 
        #endregion

        public IList<TEntity> GetAll<TEntity>(Func<TEntity, bool>? filter = null) where TEntity : BaseEntity
        {
            if (filter == null)
                filter = (TEntity) => true;

            using var Context = new TContext();
            return Context.Set<TEntity>().AsNoTracking().Where(filter).ToList();
        }

        public int GetCount<TEntity>(Func<TEntity, bool>? filter = null) where TEntity : BaseEntity
        {
			if (filter == null)
				filter = (TEntity) => true;

			using var Context = new TContext();
            return Context.Set<TEntity>().AsNoTracking().Where(filter).Count();
        }

        public TEntity GetById<TEntity>(Guid Id) where TEntity : BaseEntity
        {
            CheckIEnumerable<TEntity>();
            using var Context = new TContext();
            return Context
                .Set<TEntity>()
                .AsNoTracking()
                .Where(item => item.Id == Id)
                .FirstOrDefault() ?? throw new Exception($"Не удалось найти {typeof(TEntity)} с Id = {Id}");
        }

        public IList<TEntity> GetByIds<TEntity>(IList<Guid> Ids) where TEntity : BaseEntity
        {
            if (!Ids.Any())
                throw new ArgumentException($"В массиве идентификаторов есть повторы");
            using var Context = new TContext();
            return Context
                .Set<TEntity>()
                .AsNoTracking().
                Where(item => Ids.Contains(item.Id))
                .ToList();
        }

        public void Remove<TEntity>(TEntity? entity) where TEntity : BaseEntity
        {
            if (entity == null)
                throw new ArgumentNullException($"Can't remove {typeof(TEntity)} because it's NULL!");
            using var Context = new TContext();
            Context.Remove(entity);
            Context.SaveChanges();
        }

        public void Remove<TEntity>(Guid id) where TEntity : BaseEntity
        {
            using var Context = new TContext();
            var foundEntity = Context
                .Set<TEntity>()
                .Where(entity => entity.Id == id)
                .FirstOrDefault();

            if (foundEntity != null)
            {
                Context.Remove(foundEntity);
                Context.SaveChanges();
            }
        }

        public bool Update<TEntity>(TEntity? entity) where TEntity : BaseEntity
        {
            if (entity == null)
                throw new ArgumentNullException($"Can't update {typeof(TEntity)} because it's NULL!");

            using var Context = new TContext();
            Context.Update(entity);
            var num = Context.SaveChanges();

            return num > 0;
        }

		public async Task<bool> UpdateAsync<TEntity>(TEntity? entity) where TEntity : BaseEntity
		{
			if (entity == null)
				throw new ArgumentNullException($"Can't update {typeof(TEntity)} because it's NULL!");

			using var Context = new TContext();
			Context.Update(entity);
			var num = await Context.SaveChangesAsync();

            return num > 0;
		}

		#region Async methods
		

        public async Task<IList<TEntity>> GetAllAsync<TEntity>(Func<TEntity, bool>? filter = null) where TEntity : BaseEntity
        {
            using var Context = new TContext();
            if (filter != null)
                return await Task.FromResult(Context.Set<TEntity>().Where(filter).ToList());
            return await Context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<IList<TEntity>> GetAllAsync<TEntity, TProperty>(Func<TEntity, bool>? filter = null, Expression<Func<TEntity, TProperty>>? include = null) where TEntity : BaseEntity
        {
            using var Context = new TContext();
            if (filter != null)
            {
                if (include != null)
                    return await Task.FromResult(Context.Set<TEntity>().AsNoTracking().Include(include).Where(filter).ToList());
                else
                    return await Task.FromResult(Context.Set<TEntity>().AsNoTracking().Where(filter).ToList());
            }
            return include != null ? await Context.Set<TEntity>().AsNoTracking().Include(include).ToListAsync() : await Context.Set<TEntity>().AsNoTracking().ToListAsync();
        }
        public async Task<TEntity> GetByIdAsync<TEntity>(Guid Id) where TEntity : BaseEntity
        {
            CheckIEnumerable<TEntity>();
            using var Context = new TContext();
            return await Context
                .Set<TEntity>()
                .AsNoTracking()
                .Where(item => item.Id == Id)
                .FirstOrDefaultAsync() ?? throw new Exception($"Не удалось найти {typeof(TEntity)} с Id = {Id}");
        }

        public async Task<IList<TEntity>> GetByIdsAsync<TEntity>(IList<Guid> Ids) where TEntity : BaseEntity
        {
            if (!Ids.Any())
                throw new ArgumentException($"В массиве идентификаторов есть повторы");

            using var Context = new TContext();
            return await Task.FromResult(Context.Set<TEntity>().AsNoTracking().Where(item => Ids.Contains(item.Id)).ToList());
        }
        #endregion

        #region Utils

        private bool CheckIEnumerable<TEntity>()
        {
            if (typeof(TEntity) is IEnumerable)
                throw new TypeLoadException("Тип элемента не может быть IEnumerable");
            return true;
        }
        #endregion
    }
}
