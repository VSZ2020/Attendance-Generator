using AttendanceGenerator.Infrastructure.Logger;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceGenerator.Model.Calendar.WorkingWeek.TimeConfiguration
{
    public class WeekTimeConfiguration
    {
        /// <summary>
        /// Идентификатор конфигурации
        /// </summary>
        
        //public int Id { get; set; }

        /// <summary>
        /// Учреждение, к которому относится конфигурация
        /// </summary>
        //public int? EstablishmentId { get; set; }
        //public Establishment.Establishment? Establishment { get; set; }

        public string HoursMatrixString { get; set; }

        /// <summary>
        /// Матрица рабочих и выходных часов. 
        /// Если значение >0, то день рабочий, иначе - выходной.
        /// </summary>
        [NotMapped]
        float[] HoursMatrix { get; set; }

        public WeekTimeConfiguration(string hoursMatrixString)
        {
            this.HoursMatrixString = hoursMatrixString;
            TryParseHoursString(hoursMatrixString);
        }

        public WeekTimeConfiguration()
        {
            //HoursMatrixString = StandartWeekHours.StandartHours.ToHoursString();
        }

        private void TryParseHoursString(string hoursString)
        {
            HoursMatrix = StandartWeekHours.StandartHours.ToArray();
            string[] hours = hoursString.Split(";");
            if (hours.Length == 7)
            {
                var currentFormat = System.Globalization.CultureInfo.InvariantCulture;
                for (int i = 0; i < 7; i++)
                {
                    float.TryParse(hours[i], System.Globalization.NumberStyles.None, currentFormat, out HoursMatrix[i]);
                }
            }
            else
            {
                Logger.Log("456", "Количество часов в полученной из базы строке не равно 7");
            }
        }
    }
}
