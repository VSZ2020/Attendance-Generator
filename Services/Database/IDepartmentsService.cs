using Core.Calendar;
using Core.Database.Entities;
using Core.Security;
using Microsoft.EntityFrameworkCore;
using Services.Domains;
using SQLiteRepository;
using System.Linq.Expressions;
using System.Security.Principal;

namespace Services.Database
{
	#region IDepartmentsService
	public interface IDepartmentsService
	{
		public Task<bool> AddEstablishmentAsync(Establishment establishment);
		public Task<bool> RemoveEstablishmentAsync(Establishment establishment);
		public Task<bool> UpdateEstablishmentAsync(Establishment establishment);
		public Task<Establishment> GetEstablishmentByIdAsync(Guid id, FetchAim aim = FetchAim.None);
		public Task<IList<Establishment>> GetEstablishmentsAsync(FetchAim aim = FetchAim.None);
		public Task<Department> GetDepartmentByIdAsync(Guid id, FetchAim aim = FetchAim.None);
		public Task<IList<Department>> GetDepartmentsAsync(Guid EstablishmentId, FetchAim aim = FetchAim.None);

		public Task<bool> AddDepartmentAsync(Department dep);
		public Task<bool> RemoveDepartmentAsync(Department dep);
		public Task<bool> RemoveDepartmentAsync(Guid id);
		public Task<bool> UpdateDepartmentAsync(Department dep);

		public Task<IList<CorrectionDay>> GetCorrectionDaysAsync(Guid EstablishmentId, FetchAim aim = FetchAim.None);
		public Task<CorrectionDay> GetCorrectionDayByIdAsync(Guid CorrectionDayId, FetchAim aim = FetchAim.None);
		public Task<bool> AddCorrectionDay(CorrectionDay correctionDay);
		public Task<bool> UpdateCorrectionDay(CorrectionDay correctionDay);
		public Task<bool> RemoveCorrectionDay(CorrectionDay correctionDay);

	}
	#endregion

	public class DepartmentServiceUtils
	{
		#region DayTypeNameTranslator
		public static string DayTypeNameTranslator(DayType dayType)
		{
			return dayType switch
			{
				DayType.DayOff => "Выходной",
				DayType.PreHoliday => "Предпраздничный",
				DayType.Holiday => "Праздничный",
				DayType.Working => "Рабочий",
				_ => throw new NotImplementedException()
			};
		}
		#endregion
	}

	#region DefaultDepartmentsService
	public class DefaultDepartmentsService : BaseDatabaseService<EstablishmentContext>, IDepartmentsService
	{
		#region AddEstablishmentAsync
		public Task<bool> AddEstablishmentAsync(Establishment establishment)
		{
			return base.AddAsync<Establishment, EstablishmentEntity>(establishment);
		} 
		#endregion

		#region RemoveEstablishmentAsync
		public Task<bool> RemoveEstablishmentAsync(Establishment establishment)
		{
			return base.Delete<Establishment, EstablishmentEntity>(establishment);
		} 
		#endregion

		#region UpdateEstablishmentAsync
		public Task<bool> UpdateEstablishmentAsync(Establishment establishment)
		{
			return base.Update<Establishment, EstablishmentEntity>(establishment);
		} 
		#endregion

		#region GetEstablishmentByIdAsync
		public virtual async Task<Establishment> GetEstablishmentByIdAsync(Guid id, FetchAim aim = FetchAim.None)
		{
			var establishmentEntity = await Context.Set<EstablishmentEntity>().AsNoTracking().SingleOrDefaultAsync(e => e.Id == id) ?? new EstablishmentEntity();
			var departments = aim == FetchAim.Card ? await GetDepartmentsAsync(id, FetchAim.Index) : null;
			return new Establishment(establishmentEntity)
			{
				Departments = departments,
			};
		} 
		#endregion

		#region GetEstablishmentsAsync
		public virtual async Task<IList<Establishment>> GetEstablishmentsAsync(FetchAim aim = FetchAim.None)
		{
			var entityIds = await Context.Set<EstablishmentEntity>().AsNoTracking().Select(e => e.Id).ToListAsync();
			return entityIds.Select(e => GetEstablishmentByIdAsync(e).Result).ToList();
		} 
		#endregion

		#region AddDepartmentAsync
		public Task<bool> AddDepartmentAsync(Department dep)
		{
			return base.AddAsync<Department, DepartmentEntity>(dep);
		}
		#endregion

		#region GetDepartmentByIdAsync
		//TODO: Указание FetchAim пока ни на что не влияет
		public virtual async Task<Department> GetDepartmentByIdAsync(Guid id, FetchAim aim = FetchAim.None)
		{
			var entity = await Context.Set<DepartmentEntity>().AsNoTracking().SingleOrDefaultAsync(d => d.Id == id) ?? new DepartmentEntity();
			return new Department(entity);
		}
		#endregion

		#region GetDepartmentsAsync
		public async Task<IList<Department>> GetDepartmentsAsync(Guid EstablishmentId, FetchAim aim = FetchAim.None)
		{
			Expression<Func<DepartmentEntity, bool>> predicate = (e) => EstablishmentId != Guid.Empty ? e.EstablishmentId == EstablishmentId : true;
			return await Context
				.Set<DepartmentEntity>()
				.AsNoTracking()
				.Where(predicate)
				.Select(d => new Department(d))
				.ToListAsync();
		}
		#endregion

		#region RemoveDepartmentAsync
		public virtual Task<bool> RemoveDepartmentAsync(Department dep)
		{
			return base.Delete<Department, DepartmentEntity>(dep);
		}

		public Task<bool> RemoveDepartmentAsync(Guid id)
		{
			return base.DeleteById<Department, DepartmentEntity>(id);
		}
		#endregion

		#region UpdateDepartmentAsync
		public virtual Task<bool> UpdateDepartmentAsync(Department dep)
		{
			return base.Update<Department, DepartmentEntity>(dep);
		}
		#endregion

		#region GetCorrectionDaysAsync
		public async Task<IList<CorrectionDay>> GetCorrectionDaysAsync(Guid EstablishmentId, FetchAim aim = FetchAim.None)
		{
			Expression<Func<CorrectionDayEntity, bool>> predicate = (e) => EstablishmentId != Guid.Empty ? e.EstablishmentId == EstablishmentId : true;
			var correctionDaysIds = await Context
				.Set<CorrectionDayEntity>()
				.AsNoTracking()
				.Where(predicate)
				.Select(d => d.Id)
				.ToListAsync();
			return correctionDaysIds.Select(d => GetCorrectionDayByIdAsync(d, aim).Result).ToList();
		}
		#endregion

		#region GetCorrectionDayByIdAsync
		public async Task<CorrectionDay> GetCorrectionDayByIdAsync(Guid CorrectionDayId, FetchAim aim = FetchAim.None)
		{
			var entity = await Context.Set<CorrectionDayEntity>().AsNoTracking().SingleOrDefaultAsync(e => e.Id == CorrectionDayId) ?? new CorrectionDayEntity();
			return new CorrectionDay(entity);
		}
		#endregion

		#region AddCorrectionDay
		public Task<bool> AddCorrectionDay(CorrectionDay correctionDay)
		{
			return base.AddAsync<CorrectionDay, CorrectionDayEntity>(correctionDay);
		} 
		#endregion

		#region UpdateCorrectionDay
		public Task<bool> UpdateCorrectionDay(CorrectionDay correctionDay)
		{
			return base.Update<CorrectionDay, CorrectionDayEntity>(correctionDay);
		} 
		#endregion

		#region RemoveCorrectionDay
		public Task<bool> RemoveCorrectionDay(CorrectionDay correctionDay)
		{
			return base.Delete<CorrectionDay, CorrectionDayEntity>(correctionDay);
		} 
		#endregion
	}
	#endregion

	#region SecureDepartmentService
	public class SecureDepartmentsService: DefaultDepartmentsService
	{
		#region ctor
		public SecureDepartmentsService(IPrincipal principal) : base()
		{
			this._principal = principal;
		} 
		#endregion

		private IPrincipal _principal;

		public override Task<bool> RemoveDepartmentAsync(Department dep)
		{
			if (!_principal.IsInRole(RolesDefault.ADMINISTRATOR) || !_principal.IsInRole(RolesDefault.MODERATOR))
				throw new UnauthorizedAccessException($"У Вас нет прав на удаление подразделения {dep.Name}!");

			return base.RemoveDepartmentAsync(dep);
		}
	}

	#endregion
}
