﻿using Core;
using Core.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace SQLiteRepository.Providers
{
    public class EstablishmentProvider : IRepository<EstablishmentEntity>
    {
        public IList<EstablishmentEntity> GetAll(Func<EstablishmentEntity, bool>? filter = null)
        {
            return filter != null ? GetTable().Where(filter).ToList() : GetTable().ToList();
        }

        public Task<IList<EstablishmentEntity>> GetAllAsync(Func<EstablishmentEntity, bool>? filter = null)
        {
            return Task.FromResult(GetAll(filter));
        }

        public EstablishmentEntity GetById(int id)
        {
            return GetTable()
                .Where(e => e.Id == id)
                .FirstOrDefault() ?? throw new ArgumentNullException($"Отсутствует элемент с идентификатором {id}");
        }

        public Task<EstablishmentEntity> GetByIdAsync(int id)
        {
            return Task.FromResult(GetById(id)); 
        }

        public IList<EstablishmentEntity> GetByIds(IList<int> ids)
        {
            if (!ids.Any())
                return new List<EstablishmentEntity>();
            return GetTable().Where(e => ids.Contains(e.Id)).ToList();
        }

        public Task<IList<EstablishmentEntity>> GetByIdsAsync(IList<int> ids)
        {
            throw new NotImplementedException();
        }

        public IQueryable<EstablishmentEntity> GetTable()
        {
            using var ctx = EstablishmentContext.Get();
            return ctx.Establishments.AsNoTracking();
        }

        public int Insert(EstablishmentEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException($"{typeof(EstablishmentEntity)} is null!");
            using var ctx = EstablishmentContext.Get();
            var addedEntity = ctx.Establishments.Add(entity);
            ctx.SaveChanges();
            return addedEntity.Entity.Id;
        }

        public IList<int> Insert(IList<EstablishmentEntity> entities)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertAsync(EstablishmentEntity entity)
        {
            return Task.FromResult(Insert(entity));
        }

        public Task<IList<int>> InsertAsync(IList<EstablishmentEntity> entities)
        {
            IList<int> res = entities.Select(e => Insert(e)).ToList();
            return Task.FromResult(res);
        }

        public void Remove(EstablishmentEntity entity)
        {
            using var ctx = EstablishmentContext.Get();
            ctx.Establishments.Remove(entity);
            ctx.SaveChanges();
        }

        public void Remove(IList<EstablishmentEntity> entities)
        {
            using var ctx = EstablishmentContext.Get();
            ctx.Establishments.RemoveRange(entities);
            ctx.SaveChanges();
        }

        public Task RemoveAsync(EstablishmentEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(IList<EstablishmentEntity> entities)
        {
            throw new NotImplementedException();
        }
    }
}
