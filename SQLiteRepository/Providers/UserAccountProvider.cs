using Core;
using Core.Database.AppEntities;
using Microsoft.EntityFrameworkCore;

namespace SQLiteRepository.Providers
{
    public class UserAccountProvider : IRepository<UserAccountEntity>
    {
        public IList<UserAccountEntity> GetAll(Func<UserAccountEntity, bool>? filter = null)
        {
            using var ctx = AppContext.Get();
            var table = ctx.Set<UserAccountEntity>();
            return filter != null ? table.Where(filter).ToList() : table.ToList();
        }

        public Task<IList<UserAccountEntity>> GetAllAsync(Func<UserAccountEntity, bool>? filter = null)
        {
            return Task.FromResult(GetAll(filter));
        }

        public UserAccountEntity GetById(int id)
        {
            return GetTable()
                .Where(e => e.Id == id)
                .FirstOrDefault() ?? throw new ArgumentNullException($"Отсутствует элемент с идентификатором {id}");
        }

        public Task<UserAccountEntity> GetByIdAsync(int id)
        {
            return Task.FromResult(GetById(id));
        }

        public IList<UserAccountEntity> GetByIds(IList<int> ids)
        {
            if (!ids.Any())
                return new List<UserAccountEntity>();
            return GetTable().Where(e => ids.Contains(e.Id)).ToList();
        }

        public Task<IList<UserAccountEntity>> GetByIdsAsync(IList<int> ids)
        {
            throw new NotImplementedException();
        }

        public IQueryable<UserAccountEntity> GetTable()
        {
            using var ctx = AppContext.Get();
            return ctx.Set<UserAccountEntity>().AsNoTracking();
        }

        public int Insert(UserAccountEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException($"{typeof(UserAccountEntity)} is null!");
            using var ctx = AppContext.Get();
            var addedEntity = ctx.Set<UserAccountEntity>().Add(entity);
            ctx.SaveChanges();
            return addedEntity.Entity.Id;
        }

        public IList<int> Insert(IList<UserAccountEntity> entities)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertAsync(UserAccountEntity entity)
        {
            return Task.FromResult(Insert(entity));
        }

        public Task<IList<int>> InsertAsync(IList<UserAccountEntity> entities)
        {
            IList<int> res = entities.Select(e => Insert(e)).ToList();
            return Task.FromResult(res);
        }

        public void Remove(UserAccountEntity entity)
        {
            using var ctx = AppContext.Get();
            ctx.Set<UserAccountEntity>().Remove(entity);
            ctx.SaveChanges();
        }

        public void Remove(IList<UserAccountEntity> entities)
        {
            using var ctx = AppContext.Get();
            ctx.Set<UserAccountEntity>().RemoveRange(entities);
            ctx.SaveChanges();
        }

        public Task RemoveAsync(UserAccountEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(IList<UserAccountEntity> entities)
        {
            throw new NotImplementedException();
        }
    }
}
