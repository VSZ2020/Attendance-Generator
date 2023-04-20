using Core;
using Core.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace SQLiteRepository.Providers
{
    public class TimeIntervalProvider : IRepository<TimeIntervalEntity>
    {
        public IList<TimeIntervalEntity> GetAll(Func<TimeIntervalEntity, bool>? filter = null)
        {
            using var ctx = EstablishmentContext.Get();
            return filter != null ? ctx.Set<TimeIntervalEntity>().Where(filter).ToList() : ctx.Set<TimeIntervalEntity>().ToList();
        }

        public Task<IList<TimeIntervalEntity>> GetAllAsync(Func<TimeIntervalEntity, bool>? filter = null)
        {
            return Task.FromResult(GetAll(filter));
        }

        public TimeIntervalEntity GetById(int id)
        {
            using var ctx = EstablishmentContext.Get();
            return ctx.Set<TimeIntervalEntity>()
                .Where(e => e.Id == id)
                .FirstOrDefault() ?? throw new ArgumentNullException($"Отсутствует элемент с идентификатором {id}");
        }

        public Task<TimeIntervalEntity> GetByIdAsync(int id)
        {
            return Task.FromResult(GetById(id));
        }

        public IList<TimeIntervalEntity> GetByIds(IList<int> ids)
        {
            if (!ids.Any())
                return new List<TimeIntervalEntity>();

            using var ctx = EstablishmentContext.Get();
            return ctx.Set<TimeIntervalEntity>().Where(e => ids.Contains(e.Id)).ToList();
        }

        public Task<IList<TimeIntervalEntity>> GetByIdsAsync(IList<int> ids)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TimeIntervalEntity> GetTable()
        {
            using var ctx = EstablishmentContext.Get();
            return ctx.Set<TimeIntervalEntity>().AsNoTracking();
        }

        public int Insert(TimeIntervalEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException($"{typeof(TimeIntervalEntity)} is null!");
            using var ctx = EstablishmentContext.Get();
            var addedEntity = ctx.Set<TimeIntervalEntity>().Add(entity);
            ctx.SaveChanges();
            return addedEntity.Entity.Id;
        }

        public IList<int> Insert(IList<TimeIntervalEntity> entities)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertAsync(TimeIntervalEntity entity)
        {
            return Task.FromResult(Insert(entity));
        }

        public Task<IList<int>> InsertAsync(IList<TimeIntervalEntity> entities)
        {
            IList<int> res = entities.Select(e => Insert(e)).ToList();
            return Task.FromResult(res);
        }

        public void Remove(TimeIntervalEntity entity)
        {
            using var ctx = EstablishmentContext.Get();
            ctx.Set<TimeIntervalEntity>().Remove(entity);
            ctx.SaveChanges();
        }

        public void Remove(IList<TimeIntervalEntity> entities)
        {
            using var ctx = EstablishmentContext.Get();
            ctx.Set<TimeIntervalEntity>().RemoveRange(entities);
            ctx.SaveChanges();
        }

        public Task RemoveAsync(TimeIntervalEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(IList<TimeIntervalEntity> entities)
        {
            throw new NotImplementedException();
        }
    }
}
