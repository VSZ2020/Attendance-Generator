using Core.Converters;
using Core.Database.Entities;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Services.Domains
{
	public class TimeInterval : BaseDomain, IEntityConverter<TimeIntervalEntity>, INotifyPropertyChanged
	{
		#region ctor
		public TimeInterval()
		{

		}
		public TimeInterval(TimeIntervalEntity entity)
		{
			Id = entity.Id;
			Begin = entity.StartDate;
			End = entity.EndDate;
			Comment = entity.Comment;
			TimeIntervalTypeId = entity.IntervalTypeId;
			EmployeeId = entity.EmployeeId;
		}
		#endregion

		private TimeIntervalType timeIntervalType;
		private DateTime begin;
		private DateTime end;
		private TimeSpan duration;
		private string comment;

		public Guid EmployeeId { get; set; }
		public Employee? Employee { get; set; }

		public Guid TimeIntervalTypeId { get; set; }
		public TimeIntervalType? TimeIntervalType { get => timeIntervalType; set { timeIntervalType = value; OnChanged(); } }

		public DateTime Begin { get => begin; set { begin = value; OnChanged(); OnChanged(nameof(duration)); } }

		public DateTime End { get => end; set { end = value; OnChanged(); OnChanged(nameof(duration)); } }

		public TimeSpan Duration { get => End > Begin ? End - Begin : new TimeSpan(0); }

		public string? Comment { get => comment; set { comment = value; OnChanged(); } }

		#region IEntityConverter
		public TimeIntervalEntity ConvertToEntity()
		{
			return new TimeIntervalEntity()
			{
				Id = Id,
				StartDate = Begin,
				EndDate = End,
				Comment = Comment,
				IntervalTypeId = TimeIntervalTypeId,
				EmployeeId = EmployeeId,
			};
		}
		#endregion

		#region INotifyPropertyChanged region
		public event PropertyChangedEventHandler? PropertyChanged;
		protected void OnChanged([CallerMemberName] string? name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
		#endregion
	}
}
