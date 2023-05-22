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
        public TEntity AddEntity<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null)
                throw new ArgumentNullException($"Can't add {typeof(TEntity)} because it's NULL!");
            
            using var Context = new TContext();
            var dbSet = Context.Set<TEntity>();
            if (entity is IEnumerable)
                dbSet.AddRange(entity);
            else
                dbSet.Add(entity);

            Context.SaveChanges();

            return entity;
        }
        public IList<TEntity> GetAll<TEntity, TProperty>(Func<TEntity, bool>? filter = null, Expression<Func<TEntity, TProperty>>? include = null) where TEntity : BaseEntity
        {
            using var Context = new TContext();
            if (filter != null)
            {
                if (include != null)
                    return Context.Set<TEntity>().AsNoTracking().Include(include).Where(filter).ToList();
                else
                    return Context.Set<TEntity>().AsNoTracking().Where(filter).ToList();
            }
            return include != null ? Context.Set<TEntity>().AsNoTracking().Include(include).ToList() : Context.Set<TEntity>().AsNoTracking().ToList();
        }

        public IList<TEntity> GetAll<TEntity>(Func<TEntity, bool>? filter = null) where TEntity : BaseEntity
        {
            using var Context = new TContext();
            if (filter != null)
                return Context.Set<TEntity>().AsNoTracking().Where(filter).ToList();
            return Context.Set<TEntity>().AsNoTracking().ToList();
        }

        public int GetCount<TEntity>(Func<TEntity, bool>? filter = null) where TEntity : BaseEntity
        {
            using var Context = new TContext();
            if (filter != null)
                return Context.Set<TEntity>().AsNoTracking().Where(filter).Count();
            return Context.Set<TEntity>().AsNoTracking().Count();
        }

        public TEntity GetById<TEntity>(int Id) where TEntity : BaseEntity
        {
            CheckId(Id);
            CheckIEnumerable<TEntity>();
            using var Context = new TContext();
            return Context.Set<TEntity>().AsNoTracking().Where(item => item.Id == Id).FirstOrDefault() ?? throw new Exception($"Не удалось найти {typeof(TEntity)} с Id = {Id}");
        }

        public IList<TEntity> GetByIds<TEntity>(IList<int> Ids) where TEntity : BaseEntity
        {
            if (!Ids.Any())
                throw new ArgumentException($"В массиве идентификаторов есть повторы");
            using var Context = new TContext();
            return Context.Set<TEntity>().AsNoTracking().Where(item => Ids.Contains(item.Id)).ToList();
        }

        public void Remove<TEntity>(TEntity? entity) where TEntity : BaseEntity
        {
            if (entity == null)
                throw new ArgumentNullException($"Can't remove {typeof(TEntity)} because it's NULL!");
            using var Context = new TContext();
            Context.Remove(entity);
            Context.SaveChanges();
        }

        public void Remove<TEntity>(int id) where TEntity : BaseEntity
        {
            using var Context = new TContext();
            var foundEntity = Context.Set<TEntity>().Where(entity => entity.Id == id).FirstOrDefault();
            if (foundEntity != null)
            {
                Context.Remove(foundEntity);
                Context.SaveChanges();
            }
        }

        public void Update<TEntity>(TEntity? entity) where TEntity : BaseEntity
        {
            if (entity == null)
                throw new ArgumentNullException($"Can't update {typeof(TEntity)} because it's NULL!");
            using var Context = new TContext();
            Context.Update(entity);
            Context.SaveChanges();
        }

        #region Async methods
        public TEntity AddEntityAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            if (entity != null)
            {
                using var Context = new TContext();
                var dbSet = Context.Set<TEntity>();
                if (entity is IEnumerable)
                    dbSet.AddRangeAsync(entity);
                else
                    dbSet.AddAsync(entity);
                Context.SaveChanges();
            }

            return entity;
        }

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
        public async Task<TEntity> GetByIdAsync<TEntity>(int Id) where TEntity : BaseEntity
        {
            CheckId(Id);
            CheckIEnumerable<TEntity>();
            using var Context = new TContext();
            return await Context.Set<TEntity>().AsNoTracking().Where(item => item.Id == Id).FirstOrDefaultAsync() ?? throw new Exception($"Не удалось найти {typeof(TEntity)} с Id = {Id}");
        }

        public async Task<IList<TEntity>> GetByIdsAsync<TEntity>(IList<int> Ids) where TEntity : BaseEntity
        {
            if (!Ids.Any())
                throw new ArgumentException($"В массиве идентификаторов есть повторы");

            using var Context = new TContext();
            return await Task.FromResult(Context.Set<TEntity>().AsNoTracking().Where(item => Ids.Contains(item.Id)).ToList());
        }
        #endregion

        #region Utils
        private bool CheckId(int id)
        {
            if (id <= 0)
                throw new ArgumentException($"Id должен быть числом больше 0");
            return true;
        }

        private bool CheckIEnumerable<TEntity>()
        {
            if (typeof(TEntity) is IEnumerable)
                throw new TypeLoadException("Тип элемента не может быть IEnumerable");
            return true;
        }
        #endregion
    }
}
