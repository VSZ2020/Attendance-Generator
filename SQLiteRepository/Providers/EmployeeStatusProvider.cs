using Core;
using Core.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace SQLiteRepository.Providers
{
    public class EmployeeStatusProvider : IRepository<EmployeeStatusEntity>
    {
        public IList<EmployeeStatusEntity> GetAll(Func<EmployeeStatusEntity, bool>? filter = null)
        {
            using var ctx = EstablishmentContext.Get();
            var table = ctx.Set<EmployeeStatusEntity>();
            return filter != null ? table.Where(filter).ToList() : table.ToList();
        }

        public Task<IList<EmployeeStatusEntity>> GetAllAsync(Func<EmployeeStatusEntity, bool>? filter = null)
        {
            return Task.FromResult(GetAll(filter));
        }

        public EmployeeStatusEntity GetById(int id)
        {
            using var ctx = EstablishmentContext.Get();
            return ctx.Set<EmployeeStatusEntity>()
                .Where(e => e.Id == id)
                .FirstOrDefault() ?? throw new ArgumentNullException($"Отсутствует статус с идентификатором {id}");
        }

        public Task<EmployeeStatusEntity> GetByIdAsync(int id)
        {
            return Task.FromResult(GetById(id));
        }

        public IList<EmployeeStatusEntity> GetByIds(IList<int> ids)
        {
            if (!ids.Any())
                return new List<EmployeeStatusEntity>();
            using var ctx = EstablishmentContext.Get();
            return ctx.Set<EmployeeStatusEntity>().Where(e => ids.Contains(e.Id)).ToList();
        }

        public async Task<IList<EmployeeStatusEntity>> GetByIdsAsync(IList<int> ids)
        {
            if (!ids.Any())
                return new List<EmployeeStatusEntity>();

            async Task<IList<EmployeeStatusEntity>> getByIds()
            {
                using var ctx = EstablishmentContext.Get();
                return await ctx.Set<EmployeeStatusEntity>().Where(i => ids.Contains(i.Id)).ToListAsync();
            }
            return await getByIds();
        }

        public IQueryable<EmployeeStatusEntity> GetTable()
        {
            using var ctx = EstablishmentContext.Get();
            return ctx.Set<EmployeeStatusEntity>().AsNoTracking();
        }

        public int Insert(EmployeeStatusEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException($"{typeof(EmployeeStatusEntity)} is null!");
            using var ctx = EstablishmentContext.Get();
            var addedEntity = ctx.Set<EmployeeStatusEntity>().Add(entity);
            ctx.SaveChanges();
            return addedEntity.Entity.Id;
        }

        public IList<int> Insert(IList<EmployeeStatusEntity> entities)
        {
            return entities.Select(e => Insert(e)).ToList();
        }

        public Task<int> InsertAsync(EmployeeStatusEntity entity)
        {
            return Task.FromResult(Insert(entity));
        }

        public Task<IList<int>> InsertAsync(IList<EmployeeStatusEntity> entities)
        {
            IList<int> res = entities.Select(e => Insert(e)).ToList();
            return Task.FromResult(res);
        }

        public void Remove(EmployeeStatusEntity entity)
        {
            using var ctx = EstablishmentContext.Get();
            ctx.Set<EmployeeStatusEntity>().Remove(entity);
            ctx.SaveChanges();
        }

        public void Remove(IList<EmployeeStatusEntity> entities)
        {
            using var ctx = EstablishmentContext.Get();
            ctx.Set<EmployeeStatusEntity>().RemoveRange(entities);
            ctx.SaveChanges();
        }

        public Task RemoveAsync(EmployeeStatusEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(IList<EmployeeStatusEntity> entities)
        {
            throw new NotImplementedException();
        }
    }
}
