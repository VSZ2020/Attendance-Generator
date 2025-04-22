using AG.Core.Enums;
using AG.Core.Infrastructure.Validation;
using AG.Data;
using Microsoft.EntityFrameworkCore;

namespace AG.Services.Repository
{
    public class EmployeeTimeIntervalService
    {
        public EmployeeTimeIntervalService(DataContext ctx)
        {
            _context = ctx;
        }

        readonly DataContext _context;

        public async Task<List<ValidationMessage>> ValidateDatesInterception(Guid employeeId, DateTime dateBegin, DateTime dateEnd)
        {
            var timeIntervals = await _context.EmplToTimeInt
                .AsNoTracking()
                .Where(e => e.EmployeeId == employeeId)
                .ToArrayAsync();
            var errors = new List<ValidationMessage>(); 
            foreach(var item in timeIntervals)
            {
                if (HasDatesInterception(item.Begin, item.End,dateBegin, dateEnd))
                {
                    errors.Add(new("Пересечение", $"Обнаружено пересечение с временным интервалом {TimeIntervalService.TimeIntervalsDict[item.TimeIntervalType].Title} ({item.Begin.ToString("dd.MM.yyyy")} - {item.End.ToString("dd.MM.yyyy")})"));
                }
            }
            return errors;
        }

        public async Task<bool> HasDatesInterception(Guid employeeId, DateTime dateBegin, DateTime dateEnd)
        {
            var entitiesCount = await _context.EmplToTimeInt.Where(e => e.EmployeeId == employeeId && !(e.Begin > dateEnd || dateBegin > e.End)).CountAsync();
            return entitiesCount > 0;
        }

        public bool HasDatesInterception(DateTime date1Begin, DateTime date1End, DateTime date2Begin, DateTime date2End)
        {
            return !(date1Begin > date2End || date2Begin > date1End);
        }

    }
}
