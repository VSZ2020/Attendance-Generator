using AG.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace AG.Data.Entities
{
    public class CorrectionDayEntity: BaseEntity
    {
        public int? Year {get;set;}

        [Required]
        public int Month { get; set; }

        [Required]
        public int Day { get; set; }

        public string Title { get; set; }

        public CorrectionDayType Type { get; set; }

        public float Hours { get; set; }
    }
}
