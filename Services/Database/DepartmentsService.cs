using Core.Database.Entities;
using Core.Security;
using Services.POCO;
using SQLiteRepository;

namespace Services.Database
{
    public class DepartmentsService
    {
        public DepartmentsService(IEstablishmentItemsRepository repo) { 
            this.establishmentsRepository = repo;
        }

        private readonly IEstablishmentItemsRepository establishmentsRepository;

        public virtual DatabaseResponse<Department> GetAvailableDepartments(UserAccount user, bool countEmployees = false)
        {
            if (user == null || !UserRole.HasAccess(user.RoleType, PermissionType.ViewDepartments))
                return new DatabaseResponse<Department>(
                    DatabaseResponse<Department>.ResponseCode.PermissionsError,
                    new List<Department>(),
                    "Нет прав на просмотр списка подразделений");

            IList<DepartmentEntity> departments;
            if (UserRole.AvailableRoles[user.RoleType].HasAccess(PermissionType.ViewAllDepartments))
                departments = establishmentsRepository.GetAll<DepartmentEntity>();
            else
                departments = establishmentsRepository.GetAll<DepartmentEntity>(dep => user.DepartmentsIds.Contains(dep.Id));

            var pocoDepartments = departments.Select(DbEntityToDepartment).ToList();

            if (countEmployees)
                foreach (var dep in pocoDepartments)
                    dep.EmployeesCount = establishmentsRepository.GetCount<EmployeeEntity>(e => e.DepartmentId == dep.Id);

            return departments == null ?
                new DatabaseResponse<Department>(DatabaseResponse<Department>.ResponseCode.NoData, null, "В базе нет подразделений для указанных параметров") :
                new DatabaseResponse<Department>(DatabaseResponse<Department>.ResponseCode.Success, pocoDepartments);
        }

        public virtual void AddDepartment(Department? dep)
        {
            if (dep == null)
                throw new ArgumentNullException("Ссылка на подразделение пустая");

            establishmentsRepository.AddEntity(ToEntity(dep));
        }

        public virtual void RemoveDepartment(Department? dep)
        {
            if (dep == null)
                throw new ArgumentNullException("Ссылка на подразделение пустая");

            establishmentsRepository.Remove(ToEntity(dep));
        }

        public virtual void RemoveDepartment(int id)
        {
            establishmentsRepository.Remove<DepartmentEntity>(id);
        }

        protected Department DbEntityToDepartment(DepartmentEntity? entity)
        {
            if (entity == null)
                new Department() { Id = 0, Name = "Не определено" };

            return new Department()
            {
                Id = entity!.Id,
                Name = entity.Name,
                EstablishmentId = entity.EstablishmentId
            };
        }

        protected DepartmentEntity ToEntity(Department department)
        {
            return new DepartmentEntity()
            {
                Id = department.Id,
                Name = department.Name,
                EstablishmentId = department.EstablishmentId
            };
        }
    }
}
