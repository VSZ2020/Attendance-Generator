using AG.Core.Enums;
using Newtonsoft.Json;

namespace AG.Core.Models
{
    [JsonObject]
    public class Day
    {
        /*public int Year { get; set; }
        public int Month { get; set; }*/

        [JsonProperty("day")]
        public int DayNumber { get; set; }
        
        [JsonProperty("hours")]
        public float Hours { get; set; }

        [JsonProperty("type")]
        public DayType Type { get; set; }

        public Day Copy() => new Day() { DayNumber = this.DayNumber, Hours = this.Hours, Type = this.Type };
    }
}
