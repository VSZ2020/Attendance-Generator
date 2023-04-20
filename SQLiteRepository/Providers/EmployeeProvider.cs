using Core;
using Core.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace SQLiteRepository.Providers
{
    public class EmployeeProvider : IRepository<EmployeeEntity>
    {

        public IList<EmployeeEntity> GetAll(Func<EmployeeEntity, bool>? filter = null)
        {
            using var ctx = EstablishmentContext.Get();
            return filter != null ? ctx.Set<EmployeeEntity>().Where(filter).ToList() : ctx.Set<EmployeeEntity>().ToList();
        }

        public Task<IList<EmployeeEntity>> GetAllAsync(Func<EmployeeEntity, bool>? filter = null)
        {
            return Task.FromResult(GetAll(filter));
        }

        public EmployeeEntity GetById(int id)
        {
            using var ctx = EstablishmentContext.Get();
            return ctx.Set<EmployeeEntity>()
                .Where(e => e.Id == id)
                .FirstOrDefault() ?? throw new ArgumentNullException($"Отсутствует элемент с идентификатором {id}");
        }

        public Task<EmployeeEntity> GetByIdAsync(int id)
        {
            return Task.FromResult(GetById(id));
        }

        public IList<EmployeeEntity> GetByIds(IList<int> ids)
        {
            if (!ids.Any())
                return new List<EmployeeEntity>();

            using var ctx = EstablishmentContext.Get();
            return ctx.Set<EmployeeEntity>()
                .Where(e => ids.Contains(e.Id))
                .ToList();
        }

        public Task<IList<EmployeeEntity>> GetByIdsAsync(IList<int> ids)
        {
            throw new NotImplementedException();
        }

        public IQueryable<EmployeeEntity> GetTable()
        {
            using var ctx = EstablishmentContext.Get();
            return ctx.Set<EmployeeEntity>().AsNoTracking();
        }

        public int Insert(EmployeeEntity entity)
        {
            using var ctx = EstablishmentContext.Get();
            var addedEntity = ctx.Set<EmployeeEntity>().Add(entity).Entity.Id;
            ctx.SaveChanges();
            return addedEntity;
        }

        public IList<int> Insert(IList<EmployeeEntity> entities)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertAsync(EmployeeEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<IList<int>> InsertAsync(IList<EmployeeEntity> entities)
        {
            throw new NotImplementedException();
        }

        public void Remove(EmployeeEntity entity)
        {
            using var ctx = EstablishmentContext.Get();
            ctx.Set<EmployeeEntity>().Remove(entity);
            ctx.SaveChanges();
        }

        public void Remove(IList<EmployeeEntity> entities)
        {
            using var ctx = EstablishmentContext.Get();
            ctx.RemoveRange(entities);
            ctx.SaveChanges();
        }

        public Task RemoveAsync(EmployeeEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(IList<EmployeeEntity> entities)
        {
            throw new NotImplementedException();
        }
    }
}
