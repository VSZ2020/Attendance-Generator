using Core.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Linq.Expressions;

namespace SQLiteRepository
{
    /*
     * Шаблон класса основан на примере из https://stackoverflow.com/questions/53469498/difference-between-dbsett-property-and-sett-function-in-ef-core
     */
    public class ItemsRepository<TContext> where TContext : DbContext, new()
    {
        private TContext Context;

        public ItemsRepository(TContext? context = null) => this.Context = context is null ?  new TContext() : context;

        public TEntity AddEntity<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity != null)
                throw new ArgumentNullException($"Can't add {typeof(TEntity)} because it's NULL!");

            var dbSet = Context.Set<TEntity>();
            if (entity is IEnumerable)
                dbSet.AddRange(entity);
            else
                dbSet.Add(entity);

            Context.SaveChanges();

            return entity;
        }
        public IList<TEntity> GetAll<TEntity, TProperty>(Func<TEntity, bool>? filter = null, Expression<Func<TEntity, TProperty>>? include = null) where TEntity : class
        {
            if (filter != null)
            {
                if (include != null)
                    return Context.Set<TEntity>().Include(include).Where(filter).ToList();
                else
                    return Context.Set<TEntity>().Where(filter).ToList();
            }
            return include != null ? Context.Set<TEntity>().AsNoTracking().Include(include).ToList() : Context.Set<TEntity>().AsNoTracking().ToList();
        }

        public IList<TEntity> GetAll<TEntity>(Func<TEntity, bool>? filter = null) where TEntity : class
        {
            if (filter != null)
                return Context.Set<TEntity>().Where(filter).ToList();
            return Context.Set<TEntity>().AsNoTracking().ToList();
        }

        public TEntity GetById<TEntity>(int Id) where TEntity : BaseEntity
        {
            CheckId(Id);
            CheckIEnumerable<TEntity>();

            return Context.Set<TEntity>().Where(item => item.Id == Id).FirstOrDefault() ?? throw new Exception($"Не удалось найти {typeof(TEntity)} с Id = {Id}");
        }

        public IList<TEntity> GetByIds<TEntity>(IList<int> Ids) where TEntity : BaseEntity
        {
            if (!Ids.Any())
                throw new ArgumentException($"В массиве идентификаторов есть повторы");

            return Context.Set<TEntity>().Where(item => Ids.Contains(item.Id)).ToList();
        }

        public void Remove<TEntity>(TEntity? entity) where TEntity : class
        {
            if (entity == null)
                throw new ArgumentNullException($"Can't remove {typeof(TEntity)} because it's NULL!");
            
            Context.Remove(entity);
            Context.SaveChanges();
        }

        public void Update<TEntity>(TEntity? entity) where TEntity : class
        {
            if (entity == null)
                throw new ArgumentNullException($"Can't update {typeof(TEntity)} because it's NULL!");

            Context.Update(entity);
            Context.SaveChanges();
        }

        #region Async methods
        public TEntity AddEntityAsync<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity != null)
            {
                var dbSet = Context.Set<TEntity>();
                if (entity is IEnumerable)
                    dbSet.AddRangeAsync(entity);
                else
                    dbSet.AddAsync(entity);
                Context.SaveChanges();
            }

            return entity;
        }

        public async Task<IList<TEntity>> GetAllAsync<TEntity>(Func<TEntity, bool>? filter = null) where TEntity : class
        {
            if (filter != null)
                return await Task.FromResult(Context.Set<TEntity>().Where(filter).ToList());
            return await Context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<IList<TEntity>> GetAllAsync<TEntity, TProperty>(Func<TEntity, bool>? filter = null, Expression<Func<TEntity, TProperty>>? include = null) where TEntity : class
        {
            if (filter != null)
            {
                if (include != null)
                    return await Task.FromResult(Context.Set<TEntity>().Include(include).Where(filter).ToList());
                else
                    return await Task.FromResult(Context.Set<TEntity>().Where(filter).ToList());
            }
            return include != null ? await Context.Set<TEntity>().AsNoTracking().Include(include).ToListAsync() : await Context.Set<TEntity>().AsNoTracking().ToListAsync();
        }
        public async Task<TEntity> GetByIdAsync<TEntity>(int Id) where TEntity : BaseEntity
        {
            CheckId(Id);
            CheckIEnumerable<TEntity>();

            return await Context.Set<TEntity>().Where(item => item.Id == Id).FirstOrDefaultAsync() ?? throw new Exception($"Не удалось найти {typeof(TEntity)} с Id = {Id}");
        }

        public async Task<IList<TEntity>> GetByIdsAsync<TEntity>(IList<int> Ids) where TEntity : BaseEntity
        {
            if (!Ids.Any())
                throw new ArgumentException($"В массиве идентификаторов есть повторы");

            return await Task.FromResult(Context.Set<TEntity>().Where(item => Ids.Contains(item.Id)).ToList());
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
