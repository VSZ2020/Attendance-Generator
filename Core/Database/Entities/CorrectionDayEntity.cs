using Core.Calendar;
using System;

namespace Core.Database.Entities
{
	public class CorrectionDayEntity: BaseEntity
	{
		public CorrectionDayEntity() { }

		public DateTime Date { get; set; }
		public DayType Type { get; set; }

		public EstablishmentEntity? Establishment { get; set; }
		public Guid EstablishmentId { get; set; }
	}
}
