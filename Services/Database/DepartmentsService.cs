using Core.Database.Entities;
using Services.POCO;
using SQLiteRepository;

namespace Services.Database
{
	public class DepartmentsService: IDepartmentsService
    {
		#region ctor
		public DepartmentsService(IEstablishmentItemsRepository repo) { 
            this.establishmentsRepository = repo;
        }
		#endregion ctor

		#region fields
		private readonly IEstablishmentItemsRepository establishmentsRepository;

		#endregion fields

		public DatabaseResponse<Department> GetDepartments(UserAccount user, bool countEmployees = false)
        {
            IList<DepartmentEntity> departments;
            departments = establishmentsRepository.GetAll<DepartmentEntity>();

            var pocoDepartments = departments.Select(DbEntityToDepartment).ToList();

            if (countEmployees)
                foreach (var dep in pocoDepartments)
                    dep.EmployeesCount = establishmentsRepository.GetCount<EmployeeEntity>(e => e.DepartmentId == dep.Id);

            return departments == null ?
                new DatabaseResponse<Department>(DatabaseResponse<Department>.ResponseCode.NoData, null, "В базе нет подразделений для указанных параметров") :
                new DatabaseResponse<Department>(DatabaseResponse<Department>.ResponseCode.Success, pocoDepartments);
        }

		public Task<DatabaseResponse<Department>> GetDepartmentsAsync(UserAccount user, bool countEmployees = false)
        {
            return Task.FromResult(GetDepartments(user, countEmployees));
		}


		public void AddDepartment(Department? dep)
        {
            if (dep == null)
                throw new ArgumentNullException("Ссылка на подразделение пустая");

            establishmentsRepository.AddEntity(ToEntity(dep));
        }

        public void RemoveDepartment(Department? dep)
        {
            if (dep == null)
                throw new ArgumentNullException("Ссылка на подразделение пустая");

            establishmentsRepository.Remove(ToEntity(dep));
        }

        public void RemoveDepartment(int id)
        {
            establishmentsRepository.Remove<DepartmentEntity>(id);
        }

		#region Utils
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
		#endregion Utils
	}
}
