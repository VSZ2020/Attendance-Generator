using AG.Core.Enums;
using AG.Core.Models;
using AG.Core.Models.Timesheet;
using AG.Data;
using AG.Data.Defaults;
using AG.Services.Extensions;
using AG.Services.Utils;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AG.Services.Timesheet;

public class TimesheetService
{
    public TimesheetService(DataContext ctx)
    {
        _context = ctx;
    }

    readonly DataContext _context;

    /// <summary>
    /// Generates the list of basic days of reporting period. Correction days are not applied yet!
    /// </summary>
    /// <param name="beginDay"></param>
    /// <param name="endDay"></param>
    /// <returns></returns>
    private static List<Day> MakeBaseDaysList(int beginDay, int endDay)
    {
        var daysCount = endDay - beginDay + 1;
        var baseDays = Enumerable.Range(0, daysCount).Select(i => new Day()
        {
            DayNumber = beginDay + i,
            Type = Core.Enums.DayType.DayOff,
            Hours = 0,
        })
        .ToList();

        return baseDays;
    }

    /// <summary>
    /// Loads and applies correction days to the list of base days
    /// </summary>
    /// <param name="baseDays"></param>
    /// <param name="year"></param>
    /// <param name="month"></param>
    /// <returns></returns>
    private async Task<List<Day>> ApplyCorrectionDaysToBaseAsync(List<Day> baseDays, int year, int month)
    {
        var corrections = await _context.CorrectionDays
            .AsNoTracking()
            .Where(e => e.Month == month && (e.Year == null || year == e.Year))
            .Select(e => new CorrectionDay()
            {
                Year = e.Year,
                Month = e.Month,
                Day = e.Day,
                Type = e.Type,
                Hours = e.Hours,
            })
            .ToListAsync();

        return baseDays.ApplyCorrectionDays(year, month, corrections);
    }

    private static float EstimateWorkingTimeInHours(long workingBegin, long workingEnd, long breakBegin, long breakEnd)
    {
        var workingTotal = new TimeSpan(workingEnd - workingBegin);
        var breakTotal = new TimeSpan(breakEnd - breakBegin);
        return ((float)(workingTotal - breakTotal).TotalHours);
    }

    private async Task<List<Day>> ApplyScheduleAsync(List<Day> baseDays, Guid scheduleId, int year, int month)
    {
        var scheduleEntity = await _context.Schedules
            .AsNoTracking()
            .Include(s => s.Days)
            .FirstOrDefaultAsync(e => e.Id == scheduleId);
        if (scheduleEntity == null)
            throw new NullReferenceException("Schedule is not found");

        var scheduleDays = scheduleEntity.Days.Select(e => new ScheduleDay()
        {
            WorkBegin = e.WorkBegin,
            WorkEnd = e.WorkEnd,
            IsDayOff = e.IsDayOff,
            BreakBegin = e.BreakBegin,
            BreakEnd = e.BreakEnd,
            DayOfWeek = e.DayOfWeek,
            WorkingTime = e.IsDayOff ? 0 : EstimateWorkingTimeInHours(e.WorkBegin, e.WorkEnd, e.BreakBegin, e.BreakEnd),
        }).ToList();

        return baseDays.ApplySchedule(year, month, scheduleDays);
    }

    /// <summary>
    /// Loads employee time intervals from database and applies it to the employee list of base days
    /// </summary>
    /// <param name="employeeId"></param>
    /// <param name="year"></param>
    /// <param name="month"></param>
    /// <param name="baseDays"></param>
    /// <returns></returns>
    private async Task<List<Day>> ApplyTimeIntervalsToBaseAsync(Guid employeeId, int year, int month, List<Day> baseDays)
    {
        var timeIntervals = await _context.EmplToTimeInt
            .AsNoTracking()
            .Where(e => e.EmployeeId == employeeId)
            .Select(e => new EmployeeTimeInterval()
            {
                Type = e.TimeIntervalType,
                Begin = e.Begin,
                End = e.End,
            })
            .ToListAsync();

        return baseDays.ApplyTimeIntervals(year, month, timeIntervals);
    }

    
    /// <summary>
    /// Applies holiday and shortened day adjustments to an array of days
    /// </summary>
    /// <param name="year">Reporting year</param>
    /// <param name="month">Reporting month</param>
    /// <param name="beginDay">Begin of reporting period</param>
    /// <param name="endDay">End of reporting period</param>
    /// <returns></returns>
    public async Task<List<Day>> PopulateBaseDaysAsync(int year, int month, int beginDay, int endDay)
    {
        var baseDays = MakeBaseDaysList(beginDay, endDay);
        //await ApplyCorrectionDaysToBaseAsync(baseDays, year, month);
        return baseDays;
    }

    /// <summary>
    /// Based on the basic array of days, fills in the hours in accordance with the work schedule, fills in the periods of absence of the employee, takes into account the date of hiring and dismissal
    /// </summary>
    /// <param name="functionId">Employee function identifier in database</param>
    /// <param name="year">Reporting year</param>
    /// <param name="month">Reporting month</param>
    /// <param name="baseDays">List of base days</param>
    /// <returns>Returns a list of corrected days including employee schedule, timeintervals and fire/hire periods</returns>
    public async Task<List<Day>> PopulateEmployeeDaysAsync(Guid functionId, int year, int month, List<Day> baseDays)
    {
        var emplFunc = await _context.EmplToDepTable
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == functionId);
        if (emplFunc == null) return baseDays;

        var rate = emplFunc.Rate;
        var employeeDays = baseDays.Copy();

        await ApplyScheduleAsync(employeeDays, emplFunc.ScheduleId, year, month);
        await ApplyCorrectionDaysToBaseAsync(employeeDays, year, month);

        employeeDays = employeeDays
            .MultiplyByEmployeeRate(rate)
            .SetPreHireDays(year, month, emplFunc.AssignmentDate)
            .SetPostFiredDays(year, month, emplFunc.FiredDate);

        await ApplyTimeIntervalsToBaseAsync(emplFunc.EmployeeId, year, month, employeeDays);

        return employeeDays;
    }


    public async Task<List<Form0504421Row>> GetForm0504421Rows(Guid departmentId, int year, int month, int beginDay, int endDay)
    {
        var baseDays = await PopulateBaseDaysAsync(year, month, beginDay, endDay);
        var employeToDep = await _context.EmplToDepTable
            .AsNoTracking()
            .Include(e => e.Employee)
            .Include(e => e.Function)
            .Where(e => e.DepartmentId == departmentId)
            .ToListAsync();

        List<Form0504421Row> rows = new();
        foreach(var item in employeToDep)
        {
            var functionId = item.Id;
            var employee = item.Employee;
            var employeeDays = await PopulateEmployeeDaysAsync(functionId, year, month, baseDays);
            rows.Add(new Form0504421Row()
            {
                EmployeeName = NameUtils.ToShortName(employee!),
                Function = item.Function!.ShortName,
                IsConcurrent = item.IsConcurrent,
                Number = item.TimesheetNumber,
                Days = employeeDays,
                Rate = item.Rate,
            });
        }

        return rows;
    }

    public static float EvaluateHalfMonthHours(IList<Day> days)
    {
        if (days.Count >= 15)
        {
            return days.Take(15).Sum(e => e.Hours);
        }
        return 0;
    }

    public string SerializeTimesheet(Form0504421Content timesheet)
    {
        var json = "";
        try
        {
            json = JsonConvert.SerializeObject(timesheet, new JsonSerializerSettings() {  MissingMemberHandling = MissingMemberHandling.Ignore});
        }
        catch (JsonSerializationException ex)
        {
            //TODO: Add logger
        }
        return json;
    }

    public Form0504421Content? DeserializeTimesheet(string json)
    {
        Form0504421Content? obj = new Form0504421Content();
        try
        {
            obj = JsonConvert.DeserializeObject<Form0504421Content>(json, new JsonSerializerSettings() { MissingMemberHandling = MissingMemberHandling.Ignore });
        }
        catch (JsonSerializationException ex)
        {
            //TODO: Add logger
        }
        return obj;
    }
}