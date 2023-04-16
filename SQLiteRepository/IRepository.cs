using Core.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core
{
    public interface IRepository { }
    public interface IRepository<DbEntity> : IRepository where DbEntity: BaseEntity
    {
        IQueryable<DbEntity> GetTable();
        DbEntity GetById(int id);

        IList<DbEntity> GetByIds(IList<int> ids);

        IList<DbEntity> GetAll(Func<DbEntity, bool>? filter = null);
        int Insert(DbEntity entity);
        void Remove(DbEntity entity);

        IList<int> Insert(IList<DbEntity> entities);

        void Remove(IList<DbEntity> entities);

        #region Async methods
        Task<DbEntity> GetByIdAsync(int id);

        Task<IList<DbEntity>> GetAllAsync(Func<DbEntity, bool>? filter = null);

        Task<IList<DbEntity>> GetByIdsAsync(IList<int> ids);

        Task<int> InsertAsync(DbEntity entity);
        Task RemoveAsync(DbEntity entity);
        Task<IList<int>> InsertAsync(IList<DbEntity> entities);
        Task RemoveAsync(IList<DbEntity> entities);
        #endregion
    }
}
