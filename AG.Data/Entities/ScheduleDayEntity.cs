using System.ComponentModel.DataAnnotations.Schema;

namespace AG.Data.Entities
{
    public class ScheduleDayEntity: BaseEntity
    {
        public DayOfWeek DayOfWeek { get; set; }

        public long WorkBegin { get; set; }
        public long WorkEnd { get; set; }

        public long BreakBegin { get; set; }
        public long BreakEnd { get; set; }

        public bool IsDayOff { get; set; }

        public Guid ScheduleId { get; set; }

        [ForeignKey(nameof(ScheduleId))]
        public ScheduleEntity? Schedule { get; set; }
    }
}
