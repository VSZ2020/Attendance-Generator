using System.Collections;
using AG.Core.Enums;
using AG.Core.Models;

namespace AG.Services.Extensions;

public static class TimesheetExtensions
{
    private static DayType CorrDayToDayType(CorrectionDayType type)
    {
        return type switch
        {
            CorrectionDayType.DayOff => DayType.DayOff,
            CorrectionDayType.PreHoliday => DayType.ShortDayLegal,
            _ => DayType.WorkingDay
        };
    }

    #region ApplySchedule
    /// <summary>
    /// Applies a work schedule to an existing list of days
    /// </summary>
    /// <param name="days">List of base days</param>
    /// <param name="year">Current year</param>
    /// <param name="month">Current month</param>
    /// <param name="scheduleDays">List of defined week hours</param>
    /// <returns>List of days with applied hours</returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static List<Day> ApplySchedule(this List<Day> days, int year, int month, List<ScheduleDay> scheduleDays)
    {
        if (scheduleDays.Count != 7)
            throw new ArgumentOutOfRangeException("The number of days in a week is not equal to seven");

        for (int i = 0; i < days.Count; i++)
        {
            var date = new DateTime(year, month, days[i].DayNumber);
            var dayOfWeek = date.DayOfWeek == DayOfWeek.Sunday ? 6 : (int)date.DayOfWeek - 1;

            days[i].Type = scheduleDays[dayOfWeek].IsDayOff ? DayType.DayOff : DayType.WorkingDay;
            days[i].Hours = scheduleDays[dayOfWeek].WorkingTime;
        }

        return days;
    }
    #endregion

    #region Copy
    /// <summary>
    /// Makes a copy of days array
    /// </summary>
    /// <param name="days">Source list of days</param>
    /// <returns></returns>
    public static List<Day> Copy(this List<Day> days)
    {
        return days.Select(d => new Day()
        {
            DayNumber = d.DayNumber,
            Hours = d.Hours,
            Type = d.Type,
        })
        .ToList();
    } 
    #endregion

    #region MultiplyByEmployeeRate
    public static List<Day> MultiplyByEmployeeRate(this List<Day> days, float rate)
    {
        foreach (var day in days)
        {
            day.Hours *= rate;
        }
        return days;
    } 
    #endregion

    #region ApplyCorrectionDays
    /// <summary>
    /// Applies correction days to collection of days
    /// </summary>
    /// <param name="days">List of month days</param>
    /// <param name="year">Year for days list</param>
    /// <param name="month">Month number for days list</param>
    /// <param name="correctionDays">List of reference days</param>
    /// <returns>List of corrected days</returns>
    public static List<Day> ApplyCorrectionDays(this List<Day> days, int year, int month, IEnumerable<CorrectionDay> correctionDays)
    {
        foreach (var corrDay in correctionDays)
        {
            if (corrDay.Year != null && year != corrDay.Year.Value) continue;
            if (month != corrDay.Month) continue;
            //if day number is greater than days amount in month, then skip iteration
            if (corrDay.Day > days.Count)
                continue;

            var dayNum = corrDay.Day - 1;
            days[dayNum].Type = CorrDayToDayType(corrDay.Type);

            days[dayNum].Hours = corrDay.Type switch
            {
                CorrectionDayType.DayOff => corrDay.Hours,
                CorrectionDayType.PreHoliday => corrDay.Hours,
                _ => days[dayNum].Hours
            };
        }

        return days;
    }
    #endregion

    #region ApplyTimeIntervals
    /// <summary>
    /// Applies time intervals for employee days
    /// </summary>
    /// <param name="days">List of employee days</param>
    /// <param name="year">Year for days list</param>
    /// <param name="month">Month for days list</param>
    /// <param name="intervals">Time intervals filtered by current month and year</param>
    /// <returns></returns>
    public static List<Day> ApplyTimeIntervals(this List<Day> days, int year, int month, IEnumerable<EmployeeTimeInterval> intervals)
    {
        foreach (var day in days)
        {
            var date = new DateTime(year, month, day.DayNumber);
            foreach (var interval in intervals)
            {
                if (interval.Begin <= date && date <= interval.End)
                {
                    day.Type = interval.Type;
                }
            }

        }
        return days;
    }
    #endregion

    #region SetPreHireDays
    public static List<Day> SetPreHireDays(this List<Day> days, int year, int month, DateTime hiredDate)
    {
        foreach (var day in days)
        {
            var date = new DateTime(year, month, day.DayNumber);
            if (date < hiredDate)
                day.Hours = 0;
        }

        return days;
    }
    #endregion

    #region SetPostFiredDays
    public static List<Day> SetPostFiredDays(this List<Day> days, int year, int month, DateTime? firedDate)
    {
        if (firedDate != null)
        {
            foreach (var day in days)
            {
                var date = new DateTime(year, month, day.DayNumber);
                if (date > firedDate)
                    day.Hours = 0;
            }
        }

        return days;
    } 
    #endregion
}