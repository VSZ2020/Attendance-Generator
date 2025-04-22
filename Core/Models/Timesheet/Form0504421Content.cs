using AG.Core.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AG.Core.Models.Timesheet
{
    [JsonObject]
    public class Form0504421Content
    {
        /// <summary>
        /// OCUD Form
        /// </summary>
        [JsonProperty("ocud_form")]
        public string OcudForm { get; set; } = "504421";

        /// <summary>
        /// OCPO date
        /// </summary>
        [JsonProperty("ocpo_date")]
        public DateTime OcpoDate { get; set; }

        /// <summary>
        /// Number of correction
        /// </summary>
        [JsonProperty("correction")]
        public int CorrectionNumber { get; set; }

        /// <summary>
        /// Document creation date
        /// </summary>
        [JsonProperty("creation_date")]
        public DateTime CreationDate { get; set; }

        [JsonProperty("form")]
        public TimesheetFormType TimesheetType { get; set; }

        [JsonProperty("establishment")]
        public string Establishment { get; set; } = string.Empty;

        [JsonProperty("department")]
        public string Department { get; set; } = string.Empty;

        [JsonProperty("begin")]
        public DateTime Begin { get; set; }

        [JsonProperty("end")]
        public DateTime End { get; set; }

        [JsonProperty("number")]
        public string? TimesheetNumber { get; set; }


        [JsonProperty("resp_empl")]
        public string? ResponsibleEmployee { get; set; }

        [JsonProperty("resp_empl_func")]
        public string? ResponsibleEmployeeFunction { get; set; }

        [JsonProperty("exec_empl")]
        public string? ExecutiveEmployee { get; set; }

        [JsonProperty("exec_empl_func")]
        public string? ExecutiveEmployeeFunction { get; set; }

        [JsonProperty("acc_empl")]
        public string? AccountingEmployee { get; set; }

        [JsonProperty("acc_empl_func")]
        public string? AccountingEmployeeFunction { get; set; }

        public List<Form0504421Row>? Rows { get; set; }
    }
}
