using Core.Calendar;
using Services.Calendar;
using Services.Factories;
using Services.Infrastructure.Configuration.Configs;
using SQLiteRepository;

namespace AG.Tests
{
    public class CalendarServiceTester
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void GetMonth()
        {
            CalendarService cs = new CalendarService();
            var config = new WorkingWeekConfig();
            var correctingDays = new List<Day>()
            {
                new Day(2023, 5, 8, DayType.DayOff),
                new Day(2023, 5, 2, DayType.DayOff),
                new Day(2023, 5, 3, DayType.DayOff),
                new Day(2023, 5, 4, DayType.DayOff),
                new Day(2023, 5, 5, DayType.DayOff),
            };
            var month = cs.CreateMonth(2023, 5, config, CalendarService.GetDefaultHolidays(), correctingDays);
            Assert.Pass();
        }
    }
}