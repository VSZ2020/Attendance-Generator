using Core;
using Core.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace SQLiteRepository.Providers
{
    public class EmployeeProvider : IRepository<EmployeeEntity>
    {
        private readonly EstablishmentContext? context;
        public EmployeeProvider(EstablishmentContext context = null)
        {
            if (context == null)
                this.context = new EstablishmentContext();
            else
                this.context = context;
        }

        public IList<EmployeeEntity> GetAll(Func<EmployeeEntity, bool>? filter = null)
        {
            return filter != null ? GetTable().Where(filter).ToList() : GetTable().ToList();
        }

        public Task<IList<EmployeeEntity>> GetAllAsync(Func<EmployeeEntity, bool>? filter = null)
        {
            return Task.FromResult(GetAll(filter));
        }

        public EmployeeEntity GetById(int id)
        {
            return GetTable()
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
            return GetTable().Where(e => ids.Contains(e.Id)).ToList();
        }

        public Task<IList<EmployeeEntity>> GetByIdsAsync(IList<int> ids)
        {
            throw new NotImplementedException();
        }

        public IQueryable<EmployeeEntity> GetTable()
        {
            using var ctx = EstablishmentContext.Get();
            return ctx.Employees.AsNoTracking();
        }

        public int Insert(EmployeeEntity entity)
        {
            using var ctx = EstablishmentContext.Get();
            var addedEntity = ctx.Employees.Add(entity).Entity.Id;
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
            ctx.Employees.Remove(entity);
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
