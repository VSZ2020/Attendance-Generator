using Core;
using Core.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace SQLiteRepository.Providers
{
    public class TimeIntervalTypesProvider : IRepository<TimeIntervalTypeEntity>
    {
        public IList<TimeIntervalTypeEntity> GetAll(Func<TimeIntervalTypeEntity, bool>? filter = null)
        {
            using var ctx = EstablishmentContext.Get();
            return filter != null ? ctx.Set<TimeIntervalTypeEntity>().Where(filter).ToList() : ctx.Set<TimeIntervalTypeEntity>().ToList();
        }

        public Task<IList<TimeIntervalTypeEntity>> GetAllAsync(Func<TimeIntervalTypeEntity, bool>? filter = null)
        {
            return Task.FromResult(GetAll(filter));
        }

        public TimeIntervalTypeEntity GetById(int id)
        {
            using var ctx = EstablishmentContext.Get();
            return ctx.Set<TimeIntervalTypeEntity>()
                .Where(e => e.Id == id)
                .FirstOrDefault() ?? throw new ArgumentNullException($"Отсутствует элемент с идентификатором {id}");
        }

        public Task<TimeIntervalTypeEntity> GetByIdAsync(int id)
        {
            return Task.FromResult(GetById(id));
        }

        public IList<TimeIntervalTypeEntity> GetByIds(IList<int> ids)
        {
            if (!ids.Any())
                return new List<TimeIntervalTypeEntity>();

            using var ctx = EstablishmentContext.Get();
            return ctx.Set<TimeIntervalTypeEntity>().Where(e => ids.Contains(e.Id)).ToList();
        }

        public Task<IList<TimeIntervalTypeEntity>> GetByIdsAsync(IList<int> ids)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TimeIntervalTypeEntity> GetTable()
        {
            using var ctx = EstablishmentContext.Get();
            return ctx.Set<TimeIntervalTypeEntity>().AsNoTracking();
        }

        public int Insert(TimeIntervalTypeEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException($"{typeof(TimeIntervalTypeEntity)} is null!");
            using var ctx = EstablishmentContext.Get();
            var addedEntity = ctx.Set<TimeIntervalTypeEntity>().Add(entity);
            ctx.SaveChanges();
            return addedEntity.Entity.Id;
        }

        public IList<int> Insert(IList<TimeIntervalTypeEntity> entities)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertAsync(TimeIntervalTypeEntity entity)
        {
            return Task.FromResult(Insert(entity));
        }

        public Task<IList<int>> InsertAsync(IList<TimeIntervalTypeEntity> entities)
        {
            IList<int> res = entities.Select(e => Insert(e)).ToList();
            return Task.FromResult(res);
        }

        public void Remove(TimeIntervalTypeEntity entity)
        {
            using var ctx = EstablishmentContext.Get();
            ctx.Set<TimeIntervalTypeEntity>().Remove(entity);
            ctx.SaveChanges();
        }

        public void Remove(IList<TimeIntervalTypeEntity> entities)
        {
            using var ctx = EstablishmentContext.Get();
            ctx.Set<TimeIntervalTypeEntity>().RemoveRange(entities);
            ctx.SaveChanges();
        }

        public Task RemoveAsync(TimeIntervalTypeEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(IList<TimeIntervalTypeEntity> entities)
        {
            throw new NotImplementedException();
        }
    }
}
