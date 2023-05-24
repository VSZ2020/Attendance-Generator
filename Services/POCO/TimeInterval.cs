using Core.Database.Entities;

namespace Services.POCO
{
	public class TimeInterval
	{
		public TimeInterval() { }

		public TimeIntervalType TimeIntervalType { get; set; }

		public DateTime Begin { get; set; }

		public DateTime End { get; set; }

		public TimeSpan Duration { get => End > Begin ? End - Begin : new TimeSpan(0); }

		public string Comment { get; set; }
	}
}
