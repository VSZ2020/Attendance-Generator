using Core;
using Core.Database.Entities;
using SQLiteRepository.Providers;

namespace Services.Database
{
    public class ProvidersManager
    {
        private static Dictionary<Type, IRepository> _providers = new();

        public ProvidersManager()
        {
            //Регистрируем провайдеры
            Register(new EmployeeProvider());
            Register(new EstablishmentProvider());
            Register(new DepartmentProvider());
            Register(new FunctionProvider());
            Register(new TimeIntervalProvider());
            Register(new TimeIntervalTypesProvider());
            Register(new EmployeeStatusProvider());

            Register(new UserAccountProvider());
        }

        public ProvidersManager Register<TEntity>(IRepository<TEntity> provider) where TEntity : BaseEntity
        {
            _providers[typeof(TEntity)] = provider;
            return this;
        }

        public IRepository<TEntity> Get<TEntity>() where TEntity : BaseEntity
        {
            if (!_providers.ContainsKey(typeof(TEntity)))
                throw new KeyNotFoundException($"Не найдено провайдера, соответствующего сущности {typeof(TEntity)}");
            if (_providers[typeof(TEntity)] is not IRepository<TEntity> provider)
                throw new TypeLoadException($"Не удалось получить репозиторий заданного типа {typeof(TEntity)}");
            return provider;
        }
    }
}
