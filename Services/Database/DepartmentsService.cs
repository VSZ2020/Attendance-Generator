using Core;
using Core.Database.AppEntities;
using Core.Database.Entities;
using Services.POCO;

namespace Services.Database
{
    public class DepartmentsService
    {
        private int establishmentId = 0;
        private readonly IRepository<DepartmentEntity> departmentsRepository;
        private readonly IRepository<UserAccountEntity> userAccountRepository;

        public DepartmentsService(
            int establishmentId,
            IRepository<DepartmentEntity> departmentRep)
        {
            this.establishmentId = establishmentId;
            this.departmentsRepository = departmentRep;
        }

        public IList<Department> GetDepartments()
        {
            var deps = departmentsRepository.GetAll(e => e.EstablishmentId == establishmentId);
            return deps
                .Select(d => new Department()
                {
                    Id = d.Id,
                    Name = d.Name
                })
                .ToList();
        }

        public async Task<IList<Department>> GetDepartmentsAsync()
        {
            var deps = await departmentsRepository.GetAllAsync(e => e.EstablishmentId == establishmentId);
            return deps
                .Select(d => new Department()
                {
                    Id = d.Id,
                    Name = d.Name
                })
                .ToList();
        }
    }
}
