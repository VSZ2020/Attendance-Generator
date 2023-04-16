﻿using Core;
using Core.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace SQLiteRepository.Providers
{
    public class FunctionProvider : IRepository<FunctionEntity>
    {
        public IList<FunctionEntity> GetAll(Func<FunctionEntity, bool>? filter = null)
        {
            return filter != null ? GetTable().Where(filter).ToList() : GetTable().ToList();
        }

        public Task<IList<FunctionEntity>> GetAllAsync(Func<FunctionEntity, bool>? filter = null)
        {
            return Task.FromResult(GetAll(filter));
        }

        public FunctionEntity GetById(int id)
        {
            return GetTable()
                .Where(e => e.Id == id)
                .FirstOrDefault() ?? throw new ArgumentNullException($"Отсутствует элемент с идентификатором {id}");
        }

        public Task<FunctionEntity> GetByIdAsync(int id)
        {
            return Task.FromResult(GetById(id));
        }

        public IList<FunctionEntity> GetByIds(IList<int> ids)
        {
            if (!ids.Any())
                return new List<FunctionEntity>();
            return GetTable().Where(e => ids.Contains(e.Id)).ToList();
        }

        public Task<IList<FunctionEntity>> GetByIdsAsync(IList<int> ids)
        {
            throw new NotImplementedException();
        }

        public IQueryable<FunctionEntity> GetTable()
        {
            using var ctx = EstablishmentContext.Get();
            return ctx.Functions.AsNoTracking();
        }

        public int Insert(FunctionEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException($"{typeof(FunctionEntity)} is null!");
            using var ctx = EstablishmentContext.Get();
            var addedEntity = ctx.Functions.Add(entity);
            ctx.SaveChanges();
            return addedEntity.Entity.Id;
        }

        public IList<int> Insert(IList<FunctionEntity> entities)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertAsync(FunctionEntity entity)
        {
            return Task.FromResult(Insert(entity));
        }

        public Task<IList<int>> InsertAsync(IList<FunctionEntity> entities)
        {
            IList<int> res = entities.Select(e => Insert(e)).ToList();
            return Task.FromResult(res);
        }

        public void Remove(FunctionEntity entity)
        {
            using var ctx = EstablishmentContext.Get();
            ctx.Functions.Remove(entity);
            ctx.SaveChanges();
        }

        public void Remove(IList<FunctionEntity> entities)
        {
            using var ctx = EstablishmentContext.Get();
            ctx.Functions.RemoveRange(entities);
            ctx.SaveChanges();
        }

        public Task RemoveAsync(FunctionEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(IList<FunctionEntity> entities)
        {
            throw new NotImplementedException();
        }
    }
}
