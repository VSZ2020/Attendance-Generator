using Core;
using Core.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace SQLiteRepository.Providers
{
    public class DepartmentProvider : IRepository<DepartmentEntity>
    {
        public IList<DepartmentEntity> GetAll(Func<DepartmentEntity, bool>? filter = null)
        {
            using var ctx = EstablishmentContext.Get();
            var table = ctx.Set<DepartmentEntity>();
            return filter != null ? table.Where(filter).ToList() : table.ToList();
        }

        public Task<IList<DepartmentEntity>> GetAllAsync(Func<DepartmentEntity, bool>? filter = null)
        {
            return Task.FromResult(GetAll(filter));
        }

        public DepartmentEntity GetById(int id)
        {
            using var ctx = EstablishmentContext.Get();
            return ctx.Set<DepartmentEntity>()
                .Where(e => e.Id == id)
                .FirstOrDefault() ?? throw new ArgumentNullException($"Отсутствует подразделение с идентификатором {id}");
        }

        public Task<DepartmentEntity> GetByIdAsync(int id)
        {
            return Task.FromResult(GetById(id));
        }

        public IList<DepartmentEntity> GetByIds(IList<int> ids)
        {
            if (!ids.Any())
                return new List<DepartmentEntity>();
            using var ctx = EstablishmentContext.Get();
            return ctx.Set<DepartmentEntity>().Where(e => ids.Contains(e.Id)).ToList();
        }

        public async Task<IList<DepartmentEntity>> GetByIdsAsync(IList<int> ids)
        {
            if (!ids.Any())
                return new List<DepartmentEntity>();

            async Task<IList<DepartmentEntity>> getByIds()
            {
                using var ctx = EstablishmentContext.Get();
                return await ctx.Set<DepartmentEntity>().Where(i => ids.Contains(i.Id)).ToListAsync();
            }
            return await getByIds();
        }

        public IQueryable<DepartmentEntity> GetTable()
        {
            using var ctx = EstablishmentContext.Get();
            return ctx.Set<DepartmentEntity>().AsNoTracking();
        }

        public int Insert(DepartmentEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException($"{typeof(DepartmentEntity)} is null!");
            using var ctx = EstablishmentContext.Get();
            var addedEntity = ctx.Set<DepartmentEntity>().Add(entity);
            ctx.SaveChanges();
            return addedEntity.Entity.Id;
        }

        public IList<int> Insert(IList<DepartmentEntity> entities)
        {
            return entities.Select(e => Insert(e)).ToList();
        }

        public Task<int> InsertAsync(DepartmentEntity entity)
        {
            return Task.FromResult(Insert(entity));
        }

        public Task<IList<int>> InsertAsync(IList<DepartmentEntity> entities)
        {
            IList<int> res = entities.Select(e => Insert(e)).ToList();
            return Task.FromResult(res);
        }

        public void Remove(DepartmentEntity entity)
        {
            using var ctx = EstablishmentContext.Get();
            ctx.Set<DepartmentEntity>().Remove(entity);
            ctx.SaveChanges();
        }

        public void Remove(IList<DepartmentEntity> entities)
        {
            using var ctx = EstablishmentContext.Get();
            ctx.Set<DepartmentEntity>().RemoveRange(entities);
            ctx.SaveChanges();
        }

        public Task RemoveAsync(DepartmentEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(IList<DepartmentEntity> entities)
        {
            throw new NotImplementedException();
        }
    }
}
