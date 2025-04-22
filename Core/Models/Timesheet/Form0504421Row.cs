using Newtonsoft.Json;
using System.Collections.Generic;

namespace AG.Core.Models.Timesheet
{
    [JsonObject]
    public class Form0504421Row
    {
        public Form0504421Row()
        {
            Days = new List<Day>();
        }

        [JsonProperty("number")]
        public int Number { get; set; } = 0;

        /// <summary>
        /// Last, first and middle name of employee
        /// </summary>
        [JsonProperty("employee")]
        public string EmployeeName { get; set; } = string.Empty;

        /// <summary>
        /// Employee position
        /// </summary>
        [JsonProperty("function")]
        public string Function { get; set; } = string.Empty;

        /// <summary>
        /// Employee rate between 0,125 and 1,0
        /// </summary>
        [JsonProperty("rate")]
        public float Rate { get; set; }

        /// <summary>
        /// Employee status
        /// </summary>
        [JsonProperty("is_concurrent")]
        public bool IsConcurrent { get; set; }

        public IList<Day> Days { get; set; }
    }
}
