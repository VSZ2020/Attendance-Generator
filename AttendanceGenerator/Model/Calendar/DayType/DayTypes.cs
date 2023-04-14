using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceGenerator.Model.Calendar.DayType
{
    /// <summary>
    /// Хранит перечень возможных типов дней
    /// </summary>
    public class DayTypes
    {
        public enum EDayType
        {
            Undefined = -1,
            Working = 1,
            DayOff = 2,
            PreHoliday = 3,
            Holiday = 4
        }
        /// <summary>
        /// Список доступных типов дней
        /// </summary>
        public static Dictionary<EDayType, IDayType> Types = new Dictionary<EDayType, IDayType>()
        {
            {EDayType.Working, new Working() },
            {EDayType.DayOff, new DayOff() },
            {EDayType.PreHoliday, new PreHoliday() },
            {EDayType.Holiday, new Holiday() },
            {EDayType.Working, new Working() },
        };

        public static IDayType GetDefaultDayType() => Types[EDayType.Working];

        public static IDayType GetByType(EDayType type)
        {
            return (Types.ContainsKey(type)) ? Types[type] : new Undefined();
        }

        #region Day Types
        /// <summary>
        /// Рабочий день
        /// </summary>
        public class Working : IDayType
        {
            public EDayType DType => EDayType.Working;
            public string Title => App.Current.Resources["DayStatus_Title_PreHoliday"].ToString() ?? "[Рабочий]";

            public string ShortTitle => string.Empty;

            public string Description { get; set; } = App.Current.Resources["DayStatus_Title_PreHoliday"].ToString() ?? string.Empty;

            public bool IsDayOff { get; set; } = false;
        }
        /// <summary>
        /// Выходной
        /// </summary>
        public class DayOff : IDayType
        {
            public EDayType DType => EDayType.DayOff;
            public string Title => App.Current.Resources["DayStatus_Title_DayOff"].ToString() ?? "[Выходной]";

            public string ShortTitle => App.Current.Resources["DayStatus_ShortTitle_DayOff"].ToString() ?? "[В]";

            public string Description { get; set; } = App.Current.Resources["DayStatus_Description_DayOff"].ToString() ?? string.Empty;

            public bool IsDayOff { get; set; } = true;
        }
        /// <summary>
        /// Предпраздничный
        /// </summary>
        public class PreHoliday : IDayType
        {
            public EDayType DType => EDayType.PreHoliday;
            public string Title => App.Current.Resources["DayStatus_Title_PreHoliday"].ToString() ?? "[Предпраздничный]";

            public string ShortTitle => App.Current.Resources["DayStatus_ShortTitle_PreHoliday"].ToString() ?? "[пп]";

            public string Description { get; set; } = App.Current.Resources["DayStatus_Description_PreHoliday"].ToString() ?? string.Empty;

            public bool IsDayOff { get; set; } = false;
        }
        /// <summary>
        /// Праздничный день
        /// </summary>
        public class Holiday : IDayType
        {
            public EDayType DType => EDayType.Holiday;
            public string Title => App.Current.Resources["DayStatus_Title_Holiday"].ToString() ?? "[Праздничный]";

            public string ShortTitle => App.Current.Resources["DayStatus_ShortTitle_Holiday"].ToString() ?? "[П]";

            public string Description { get; set; } = App.Current.Resources["DayStatus_Description_Holiday"].ToString() ?? string.Empty;

            public bool IsDayOff { get; set; } = true;
        }
        /// <summary>
        /// Неопределенный
        /// </summary>
        public class Undefined : IDayType
        {
            public EDayType DType => EDayType.Undefined;
            public string Title => App.Current.Resources["DayStatus_Title_Undefined"].ToString() ?? "[Не определен]";

            public string ShortTitle => App.Current.Resources["DayStatus_ShortTitle_Undefined"].ToString() ?? "[N]";

            public string Description { get; set; } = string.Empty;

            public bool IsDayOff { get; set; } = false;
        }
        #endregion
    }
}
