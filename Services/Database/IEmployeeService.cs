using Core.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Domains;
using SQLiteRepository;
using System.Linq.Expressions;
using System.Security.Principal;

namespace Services.Database
{

	#region IEmployeeService
	public interface IEmployeeService
	{
		#region Employee
		public Task<Employee> GetEmployeeByIdAsync(Guid id, FetchAim aim = FetchAim.None, FetchAim statusFetchAim = FetchAim.None, FetchAim departmentFetchAim = FetchAim.None, FetchAim functionFetchAim = FetchAim.None);
		public Task<IList<Employee>> GetEmployeesAsync(Guid departmentId, FetchAim aim = FetchAim.None, FetchAim statusFetchAim = FetchAim.None, FetchAim departmentFetchAim = FetchAim.None, FetchAim functionFetchAim = FetchAim.None);

		public Task<int> GetEmployeesCountAsync(Guid departmentId);
		public Task<bool> AddEmployeeAsync(Employee employee);
		public Task<bool> UpdateEmployeeAsync(Employee employee);
		public Task<bool> DeleteEmployeeAsync(Employee employee);
		public Task<bool> DeleteEmployeeAsync(Guid employeeId);
		#endregion

		#region Function
		public Task<EmployeeFunction> GetFunctionByIdAsync(Guid id, FetchAim aim = FetchAim.None);

		public Task<IList<EmployeeFunction>> GetFunctionsAsync(Guid functionGroupId,FetchAim aim = FetchAim.None);

		public Task<bool> AddFunction(EmployeeFunction function);
		public Task<bool> UpdateFunction(EmployeeFunction function);
		public Task<bool> DeleteFunction(Guid id);
		#endregion

		#region FunctionGroup
		public Task<FunctionGroup> GetFunctionGroupByIdAsync(Guid id, FetchAim aim = FetchAim.None);
		public Task<IList<FunctionGroup>> GetFunctionGroupsAsync(FetchAim aim = FetchAim.None);
		public Task<bool> AddFunctionGroupAsync(FunctionGroup functionGroup);
		public Task<bool> UpdateFunctionGroupAsync(FunctionGroup functionGroup);
		public Task<bool> DeleteFunctionGroupAsync(Guid id);
		#endregion

		#region TimeInterval
		public Task<TimeInterval> GetTimeIntervalByIdAsync(Guid id, FetchAim aim = FetchAim.None);
		public Task<IList<TimeInterval>> GetTimeIntervalAsync(Guid employeeId, FetchAim aim = FetchAim.None);
		public Task<IList<TimeInterval>> GetTimeIntervalAsync(Employee employee, FetchAim aim = FetchAim.None)
		{
			return GetTimeIntervalAsync(employee.Id, aim);
		}
		public Task<bool> AddTimeIntervalAsync(TimeInterval timeInterval);
		public Task<bool> UpdateTimeIntervalAsync(TimeInterval timeInterval);
		public Task<bool> DeleteTimeIntervalAsync(Guid timeIntervalId); 
		#endregion

		#region TimeIntervalType
		public Task<TimeIntervalType> GetTimeIntervalTypeByIdAsync(Guid id, FetchAim aim = FetchAim.None);
		public Task<IList<TimeIntervalType>> GetTimeIntervalTypesAsync(FetchAim aim = FetchAim.None);
		public Task<bool> AddTimeIntervalTypeAsync(TimeIntervalType timeIntervalType);
		public Task<bool> UpdateTimeIntervalTypeAsync(TimeIntervalType type);
		public Task<bool> DeleteTimeIntervalTypeAsync(Guid timeIntervalTypeId);
		#endregion

		#region Status
		public Task<EmployeeStatus> GetStatusByIdAsync(Guid id, FetchAim aim = FetchAim.None);
		public Task<IList<EmployeeStatus>> GetStatusesAsync(FetchAim aim = FetchAim.None);
		public Task<bool> AddStatusAsync(EmployeeStatus status);
		public Task<bool> UpdateStatusAsync(EmployeeStatus status);
		public Task<bool> DeleteStatusAsync(Guid id);
		#endregion
	}
	#endregion

	#region DefaultEmployeeService
	public class DefaultEmployeeService : BaseDatabaseService<EstablishmentContext>, IEmployeeService
	{
		#region ctor
		public DefaultEmployeeService(IDepartmentsService departmentService): base() 
		{ 
			this.departmentsService = departmentService;
		}
		#endregion ctor

		private IDepartmentsService departmentsService;

		#region AddEmployeeAsync
		public virtual Task<bool> AddEmployeeAsync(Employee employee)
		{
			return base.AddAsync<Employee, EmployeeEntity>(employee);
		}
		#endregion

		#region AddFunction
		public virtual Task<bool> AddFunction(EmployeeFunction function)
		{
			return AddAsync<EmployeeFunction, FunctionEntity>(function);
		}
		#endregion

		#region AddFunctionGroupAsync
		public virtual Task<bool> AddFunctionGroupAsync(FunctionGroup functionGroup)
		{
			return base.AddAsync<FunctionGroup, FunctionGroupEntity>(functionGroup);
		} 
		#endregion

		#region AddStatusAsync
		public virtual Task<bool> AddStatusAsync(EmployeeStatus status)
		{
			return AddAsync<EmployeeStatus, EmployeeStatusEntity>(status);
		} 
		#endregion

		#region AddTimeIntervalAsync
		public virtual Task<bool> AddTimeIntervalAsync(TimeInterval timeInterval)
		{
			return base.AddAsync<TimeInterval, TimeIntervalEntity>(timeInterval);
		}
		#endregion

		#region AddTimeIntervalTypeAsync
		public virtual Task<bool> AddTimeIntervalTypeAsync(TimeIntervalType timeIntervalType)
		{
			return base.AddAsync<TimeIntervalType, TimeIntervalTypeEntity>(timeIntervalType);
		} 
		#endregion

		#region DeleteEmployeeAsync
		public virtual Task<bool> DeleteEmployeeAsync(Employee employee)
		{
			return base.Delete<Employee, EmployeeEntity>(employee);
		}

		public virtual Task<bool> DeleteEmployeeAsync(Guid employeeId)
		{
			return base.DeleteById<Employee, EmployeeEntity>(employeeId);
		}
		#endregion

		#region DeleteFunction
		public virtual Task<bool> DeleteFunction(Guid id)
		{
			return base.DeleteById<EmployeeFunction, FunctionEntity>(id);
		}
		#endregion

		#region DeleteFunctionGroupAsync
		public virtual Task<bool> DeleteFunctionGroupAsync(Guid id)
		{
			return base.DeleteById<FunctionGroup, FunctionGroupEntity>(id);
		} 
		#endregion

		#region DeleteStatusAsync
		public virtual Task<bool> DeleteStatusAsync(Guid id)
		{
			return base.DeleteById<EmployeeStatus, EmployeeStatusEntity>(id);
		} 
		#endregion

		#region DeleteTimeIntervalAsync
		public virtual Task<bool> DeleteTimeIntervalAsync(Guid timeIntervalId)
		{
			return base.DeleteById<TimeInterval, TimeIntervalEntity>(timeIntervalId);
		} 
		#endregion

		#region DeleteTimeIntervalTypeAsync
		public virtual Task<bool> DeleteTimeIntervalTypeAsync(Guid timeIntervalTypeId)
		{
			return base.DeleteById<TimeIntervalType, TimeIntervalTypeEntity>(timeIntervalTypeId);
		}
		#endregion

		#region GetEmployeeByIdAsync
		public virtual async Task<Employee> GetEmployeeByIdAsync(Guid id, FetchAim aim = FetchAim.None, FetchAim statusFetchAim = FetchAim.None, FetchAim departmentFetchAim = FetchAim.None, FetchAim functionFetchAim = FetchAim.None)
		{
			var entity = await Context
				.Set<EmployeeEntity>()
				.AsNoTracking()
				.SingleOrDefaultAsync(e => e.Id == id);
			if (entity == null)
				throw new NullReferenceException($"Сотрудник с идентификатором {id} не найден в базе данных");

			var department = (aim == FetchAim.Card || aim == FetchAim.Table) ? await departmentsService.GetDepartmentByIdAsync(entity.DepartmentId, departmentFetchAim) : null;
			var status = (aim == FetchAim.Card || aim == FetchAim.Table) ? await GetStatusByIdAsync(entity.StatusId, statusFetchAim) : null;
			var function = (aim == FetchAim.Card || aim == FetchAim.Table) ? await GetFunctionByIdAsync(entity.FunctionId, functionFetchAim) : null;

			return new Employee(entity)
			{
				Department = department,
				Status = status,
				Function = function,
			};
		}
		#endregion

		#region GetEmployeesAsync
		public virtual async Task<IList<Employee>> GetEmployeesAsync(Guid departmentId, FetchAim aim = FetchAim.None, FetchAim statusFetchAim = FetchAim.None, FetchAim departmentFetchAim = FetchAim.None, FetchAim functionFetchAim = FetchAim.None)
		{
			//Expression<Func<EmployeeEntity, bool>> predicate = (e) => departmentId != Guid.Empty ? e.DepartmentId == departmentId : true;
			//var entitiesIds =
			//	await Context
			//	.Set<EmployeeEntity>()
			//	.Where(predicate)
			//	.Select(e => e.Id)
			//	.ToListAsync();
			//return entitiesIds.Select(id => GetEmployeeByIdAsync(id, aim, statusFetchAim, departmentFetchAim, functionFetchAim).Result).ToList();

			Expression<Func<EmployeeEntity, bool>> predicate = (e) => departmentId != Guid.Empty ? e.DepartmentId == departmentId : true;
			var employees =
				await Context
				.Set<EmployeeEntity>()
				.AsNoTracking()
				.Where(predicate)
				.Select(e => new Employee(e))
				.ToListAsync();

			foreach (var employee in employees)
			{
				var department = (aim == FetchAim.Card || aim == FetchAim.Table) ? await departmentsService.GetDepartmentByIdAsync(employee.DepartmentId, departmentFetchAim) : null;
				var status = (aim == FetchAim.Card || aim == FetchAim.Table) ? await GetStatusByIdAsync(employee.StatusId, statusFetchAim) : null;
				var function = (aim == FetchAim.Card || aim == FetchAim.Table) ? await GetFunctionByIdAsync(employee.FunctionId, functionFetchAim) : null;
				
				employee.Status = status;
				employee.Function = function;
				employee.Department = department;
			}
			return employees;
		}
		#endregion

		#region GetEmployeesCountAsync
		public Task<int> GetEmployeesCountAsync(Guid departmentId)
		{
			return Context.Set<EmployeeEntity>().Where(e => e.DepartmentId == departmentId).CountAsync();
		} 
		#endregion

		#region GetFunctionByIdAsync
		public virtual async Task<EmployeeFunction> GetFunctionByIdAsync(Guid id, FetchAim aim = FetchAim.None)
		{
			var entity = 
				await Context
				.Set<FunctionEntity>()
				.AsNoTracking()
				.SingleOrDefaultAsync(e => e.Id == id);
			if (entity == null)
				throw new NullReferenceException($"Должность с идентификатором {id} не найдена в базе данных");

			var functionGroup = (aim == FetchAim.Card || aim == FetchAim.Table) ? await GetFunctionGroupByIdAsync(entity.FunctionGroupId) : null;

			return new EmployeeFunction(entity)
			{
				FunctionGroup = functionGroup
			};
		}
		#endregion

		#region GetFunctionsAsync
		public virtual async Task<IList<EmployeeFunction>> GetFunctionsAsync(Guid functionGroupId, FetchAim aim = FetchAim.None)
		{
			Expression<Func<FunctionEntity, bool>> predicate = (e) => functionGroupId != Guid.Empty ? e.FunctionGroupId == functionGroupId : true;
			var entitiesIds =
				await Context
				.Set<FunctionEntity>()
				.AsNoTracking()
				.Where(predicate)
				.Select(e => e.Id)
				.ToListAsync();
			return entitiesIds.Select(id => GetFunctionByIdAsync(id, aim).Result).ToList();
		} 
		#endregion

		#region GetFunctionGroupByIdAsync
		public virtual async Task<FunctionGroup> GetFunctionGroupByIdAsync(Guid id, FetchAim aim = FetchAim.None)
		{
			var entity = 
				await Context
				.Set<FunctionGroupEntity>()
				.AsNoTracking()
				.SingleOrDefaultAsync(e => e.Id == id);
			if (entity == null)
				throw new NullReferenceException($"Группа должностей с идентификатором {id} не найдена в базе данных");

			return new FunctionGroup(entity);
		}
		#endregion

		#region GetFunctionGroupsAsync
		public virtual async Task<IList<FunctionGroup>> GetFunctionGroupsAsync(FetchAim aim = FetchAim.None)
		{
			return await Context
				.Set<FunctionGroupEntity>()
				.AsNoTracking()
				.Select(e => new FunctionGroup(e))
				.ToListAsync();
		} 
		#endregion

		#region GetStatusByIdAsync
		public virtual async Task<EmployeeStatus> GetStatusByIdAsync(Guid id, FetchAim aim = FetchAim.None)
		{
			var entity = 
				await Context
				.Set<EmployeeStatusEntity>()
				.AsNoTracking()
				.SingleOrDefaultAsync(e => e.Id == id);
			if (entity == null)
				throw new NullReferenceException($"Статус с идентификатором {id} не найден в базе данных");

			return new EmployeeStatus(entity);
		}
		#endregion

		#region GetStatusesAsync
		public virtual async Task<IList<EmployeeStatus>> GetStatusesAsync(FetchAim aim = FetchAim.None)
		{
			var entitiesIds =
				await Context
				.Set<EmployeeStatusEntity>()
				.AsNoTracking()
				.Select(e => e.Id)
				.ToListAsync();
			return entitiesIds.Select(id => GetStatusByIdAsync(id, aim).Result).ToList();
		}
		#endregion

		#region GetTimeIntervalByIdAsync
		public virtual async Task<TimeInterval> GetTimeIntervalByIdAsync(Guid id, FetchAim aim = FetchAim.None)
		{
			var entity =
				await Context
				.Set<TimeIntervalEntity>()
				.AsNoTracking()
				.SingleOrDefaultAsync(e => e.Id == id);

			if (entity == null)
				throw new NullReferenceException($"Интервал с идентификатором {id} не найден в базе данных");

			var timeIntervalType = (aim == FetchAim.Table || aim == FetchAim.Card) ? await GetTimeIntervalTypeByIdAsync(entity.IntervalTypeId, aim) : null;
			
			return new TimeInterval(entity)
			{
				TimeIntervalType = timeIntervalType
			};
		}
		#endregion

		#region GetTimeIntervalAsync
		public virtual async Task<IList<TimeInterval>> GetTimeIntervalAsync(Guid employeeId, FetchAim aim = FetchAim.None)
		{
			Expression<Func<TimeIntervalEntity, bool>> predicate = (e) => employeeId != Guid.Empty ? e.EmployeeId == employeeId : true;
			var timeIntervals =
				await Context
				.Set<TimeIntervalEntity>()
				.AsNoTracking()
				.Where(predicate)
				.Select(e => new TimeInterval(e))
				.ToListAsync();

			if (aim == FetchAim.Card || aim == FetchAim.Table)
			{
				foreach (var timeInterval in timeIntervals)
				{
					timeInterval.TimeIntervalType = await GetTimeIntervalTypeByIdAsync(timeInterval.TimeIntervalTypeId);
					timeInterval.Employee = await GetEmployeeByIdAsync(timeInterval.EmployeeId, FetchAim.Index);
				}
			}

			return timeIntervals;
		} 
		#endregion

		#region GetTimeIntervalTypeByIdAsync
		public virtual async Task<TimeIntervalType> GetTimeIntervalTypeByIdAsync(Guid id, FetchAim aim = FetchAim.None)
		{
			var entity =
				await Context
				.Set<TimeIntervalTypeEntity>()
				.AsNoTracking()
				.SingleOrDefaultAsync(e => e.Id == id);

			if (entity == null)
				throw new NullReferenceException($"Тип интервала с идентификатором {id} не найден в базе данных");

			return new TimeIntervalType(entity);
		}
		#endregion

		#region GetTimeIntervalTypesAsync
		public virtual async Task<IList<TimeIntervalType>> GetTimeIntervalTypesAsync(FetchAim aim = FetchAim.None)
		{
			return await Context
				.Set<TimeIntervalTypeEntity>()
				.AsNoTracking()
				.Select(e => new TimeIntervalType(e))
				.ToListAsync();
		} 
		#endregion

		#region UpdateEmployeeAsync
		public virtual Task<bool> UpdateEmployeeAsync(Employee employee)
		{
			return base.Update<Employee, EmployeeEntity>(employee);
		}
		#endregion

		#region UpdateFunction
		public virtual Task<bool> UpdateFunction(EmployeeFunction function)
		{
			return base.Update<EmployeeFunction, FunctionEntity>(function);
		}
		#endregion

		#region UpdateFunctionGroupAsync
		public virtual Task<bool> UpdateFunctionGroupAsync(FunctionGroup functionGroup)
		{
			return base.Update<FunctionGroup, FunctionGroupEntity>(functionGroup);
		} 
		#endregion

		#region UpdateStatusAsync
		public virtual Task<bool> UpdateStatusAsync(EmployeeStatus status)
		{
			return base.Update<EmployeeStatus, EmployeeStatusEntity>(status);
		} 
		#endregion

		#region UpdateTimeIntervalAsync
		public virtual Task<bool> UpdateTimeIntervalAsync(TimeInterval timeInterval)
		{
			return base.Update<TimeInterval, TimeIntervalEntity>(timeInterval);
		}
		#endregion

		#region UpdateTimeIntervalTypeAsync
		public virtual Task<bool> UpdateTimeIntervalTypeAsync(TimeIntervalType type)
		{
			return base.Update<TimeIntervalType, TimeIntervalTypeEntity>(type);
		}
		#endregion

	}
	#endregion

	#region SecurityEmployeeService
	public class SecurityEmployeeService: DefaultEmployeeService
	{
		#region	ctor
		public SecurityEmployeeService(IDepartmentsService service, IPrincipal principal): base(service)
		{
			this._principal = principal;
		}
		#endregion

		private readonly IPrincipal _principal;
	}
	#endregion
}
