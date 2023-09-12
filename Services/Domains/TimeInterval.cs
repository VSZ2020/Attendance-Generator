using Core.Database.Entities;
using Core.ViewModel;

namespace Services.Domains
{
	public class TimeInterval: BaseModel
	{
		private TimeIntervalType timeIntervalType;
		private DateTime begin;
		private DateTime end;
		private TimeSpan duration;
		private string comment;

		public TimeInterval() { }

		public int Id { get; set; }
		public TimeIntervalType TimeIntervalType { get => timeIntervalType; set { timeIntervalType = value; OnChanged(); } }

		public DateTime Begin { get => begin; set { begin = value; OnChanged(); OnChanged(nameof(duration)); } }

		public DateTime End { get => end; set { end = value; OnChanged(); OnChanged(nameof(duration)); } }

		public TimeSpan Duration { get => End > Begin ? End - Begin : new TimeSpan(0); }

		public string Comment { get => comment; set { comment = value; OnChanged(); } }
	}
}
