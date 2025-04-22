using AG.Core.Enums;
using AG.Core.Models;

namespace AG.Services.Repository;

public class TimeIntervalService
{

    #region AllTimeIntervals

    public static List<TimeInterval> AllTimeIntervals = new List<TimeInterval>()
    {
        new (){
            CODE = 1,
            Title = "Работа в дневное время",
            ShortTitle = "Я",
            Reason = "Продолжительность работы в дневное время",
            DayType = DayType.WorkingDay,
        },
        new (){
            CODE = 2,
            Title = "Работа в ночное время",
            ShortTitle = "Н",
            Reason = "Продолжительность работы в ночное время",
            DayType = DayType.WorkingNight
        },
        new (){
            CODE = 3,
            Title = "Работа в выходные",
            ShortTitle = "РВ",
            Reason = "Продолжительность работы в выходные и нерабочие дни",
            DayType = DayType.WorkingHoliday
        },
        new (){
            CODE = 4,
            Title = "Сверхурочная работа",
            ShortTitle = "С",
            Reason = "Продолжительность сверхурочной работы",
            DayType = DayType.WorkingOvertime
        },
        new (){
            CODE = 5,
            Title = "Работа вахтовым методом",
            ShortTitle = "ВМ",
            Reason = "Продолжительность работы вахтовым методом",
            DayType = DayType.WorkingWatch,
        },
        new (){
            CODE = 6,
            Title = "Командировка",
            ShortTitle = "К",
            Reason = "Служебная командировка",
            DayType = DayType.BusinessTrip,
        },
        new (){
            CODE = 7,
            Title = "Повышение квалификации",
            ShortTitle = "ПК",
            Reason = "Повышение квалификации с отрывом от работы",
            DayType = DayType.TrainingInterrupt,
        },
        new (){
            CODE = 8,
            Title = "Повышение квалификации (другое)",
            ShortTitle = "ПМ",
            Reason = "Повышение квалификации с отрывом от работы в другой местности",
            DayType = DayType.TrainingInterruptOther,
        },
        new (){
            CODE = 9,
            Title = "Основной оплачиваемый отпуск",
            ShortTitle = "ОТ",
            Reason = "Ежегодный основной оплачиваемый отпус",
            DayType = DayType.VacationPaid
        },
        new (){
            CODE = 10,
            Title = "Дополнительный оплачиваемый отпуск",
            ShortTitle = "ОД",
            Reason = "Ежегодный дополнительный оплачиваемый отпуск",
            DayType = DayType.VacationPaidAdditional
        },
        new (){
            CODE = 11,
            Title = "Доп. отпуск по обучению с оплатой",
            ShortTitle = "У",
            Reason = "Дополнительный отпуск в связи с обучением с сохранением среднего заработка работникам, совмещающим работу с обучением",
            DayType = DayType.VacationEduPaid
        },
        new (){
            CODE = 12,
            Title = "Сокращенный день для обучающихся с оплатой",
            ShortTitle = "УВ",
            Reason = "Сокращенная продолжительность рабочего времени для обучающихся без отрыва от производства с частичным сохранением заработной платы",
            DayType = DayType.ShortDayEduPaid
        },
        new (){
            CODE = 13,
            Title = "Доп. отпуск по обучению без оплаты",
            ShortTitle = "УД",
            Reason = "Дополнительный отпуск в связи с обучением без сохранения заработной платы",
            DayType = DayType.VacationEduUnpaid
        },
        new (){
            CODE = 14,
            Title = "Отпуск по беременности и родам",
            ShortTitle = "Р",
            Reason = "Отпуск по беременности и родам (отпуск в связи с усыновлением новорожденного ребенка)",
            DayType = DayType.MaternityLeave
        },
        new (){
            CODE = 15,
            Title = "Отпуск по уходу за ребенком",
            ShortTitle = "ОЖ",
            Reason = "Отпуск по уходу за ребенком до достижения им возраста трех лет",
            DayType = DayType.MaternityLeave3YearsOld
        },
        new (){
            CODE = 16,
            Title = "Отпуск без сохранения заработной платы (работодатель)",
            ShortTitle = "ДО",
            Reason = "Отпуск без сохранения заработной платы, предоставленный работнику по разрешению работодателя",
            DayType = DayType.VacationUnpaid,
        },
        new (){
            CODE = 17,
            Title = "Отпуск без сохранения заработной платы (законодательство)",
            ShortTitle = "ОЗ",
            Reason = "Отпуск без сохранения заработной платы при условиях, предусмотренных действующим законодательством Российской Федерации",
            DayType = DayType.VacationLegalUnpaid
        },
        new (){
            CODE = 18,
            Title = "Доп. отпуск без оплаты",
            ShortTitle = "ДБ",
            Reason = "Ежегодный дополнительный отпуск без сохранения заработной платы",
            DayType = DayType.VacationUnpaidAdditional,
        },
        new (){
            CODE = 19,
            Title = "Больничный",
            ShortTitle = "Б",
            Reason = "Временная нетрудоспособность (кроме случаев, предусмотренных кодом \"Т\") с назначением пособия согласно законодательству",
            DayType = DayType.IllnessPaid
        },
        new (){
            CODE = 20,
            Title = "Больничный (без оплаты)",
            ShortTitle = "Т",
            Reason = "Временная нетрудоспособность без назначения пособия в случаях, предусмотренных законодательством",
            DayType = DayType.IllnessUnpaid
        },
        new (){
            CODE = 21,
            Title = "Сокращенный рабочий день",
            ShortTitle = "ЛЧ",
            Reason = "Сокращенная продолжительность рабочего времени против нормальной продолжительности рабочего дня в случаях, предусмотренных законодательством",
            DayType = DayType.ShortDayLegal,
        },
        new (){
            CODE = 22,
            Title = "Время вынужденного прогула",
            ShortTitle = "ПВ",
            Reason = "Время вынужденного прогула в случае признания увольнения, перевода на другую работу или отстранения от работы незаконными с восстановлением на прежней работе",
            DayType = DayType.ForcedAbsenteeism,
        },
        new (){
            CODE = 23,
            Title = "Прогулы (законодательство)",
            ShortTitle = "Г",
            Reason = "Невыходы на время исполнения государственных или общественных обязанностей согласно законодательству",
            DayType = DayType.AbsenteeismGov,
        },
        new (){
            CODE = 24,
            Title = "Прогулы (неуважит.)",
            ShortTitle = "ПР",
            Reason = "Прогулы (отсутствие на рабочем месте без уважительных причин в течение времени, установленного законодательством)",
            DayType = DayType.UnexcusedAbsenteeism
        },
        new (){
            CODE = 25,
            Title = "Сокращенный рабочий день",
            ShortTitle = "НС",
            Reason = "Продолжительность работы в режиме неполного рабочего времени по инициативе работодателя в случаях, предусмотренных законодательством",
            DayType = DayType.ShortDayEmployer,
        },
        new (){
            CODE = 26,
            Title = "Выходные дни",
            ShortTitle = "В",
            Reason = "Выходные дни (еженедельный отпуск) и нерабочие праздничные дни",
            DayType = DayType.DayOff,
        },
        new (){
            CODE = 27,
            Title = "Доп. выходные дни (оплачиваемые)",
            ShortTitle = "ОВ",
            Reason = "Дополнительные выходные дни (оплачиваемые)",
            DayType = DayType.AdditionalDayOffPaid,
        },
        new (){
            CODE = 28,
            Title = "Доп. выходные дни (неоплачиваемые)",
            ShortTitle = "НВ",
            Reason = "Дополнительные выходные дни (без сохранения заработной платы)",
            DayType = DayType.AdditionalDayOffUnpaid,
        },
        new (){
            CODE = 29,
            Title = "Забастовка",
            ShortTitle = "ЗБ",
            Reason = "Забастовка (при условиях и в порядке, предусмотренных законом)",
            DayType = DayType.Strike,
        },
        new (){
            CODE = 30,
            Title = "Неявка",
            ShortTitle = "НН",
            Reason = "Неявки по невыясненным причинам (до выяснения обстоятельств)",
            DayType = DayType.UnknownAbsenteeism,
        },
        new (){
            CODE = 31,
            Title = "Простой (работодатель)",
            ShortTitle = "РП",
            Reason = "Время простоя по вине работодателя",
            DayType = DayType.StrikeByEmployer,
        },
        new (){
            CODE = 32,
            Title = "Простой (искл.)",
            ShortTitle = "НП",
            Reason = "Время простоя по причинам, не зависящим от работодателя и работника",
            DayType = DayType.StrikeOther,
        },
        new (){
            CODE = 33,
            Title = "Простой (работник)",
            ShortTitle = "ВП",
            Reason = "Время простоя по вине работника",
            DayType = DayType.StrikeByEmployee,
        },
        new (){
            CODE = 34,
            Title = "Отстранение от работы с оплатой",
            ShortTitle = "НО",
            Reason = "Отстранение от работы (недопущение к работе) с оплатой (пособием) в соответствии с законодательством",
            DayType = DayType.SuspensionFromWorkPaid,
        },
        new (){
            CODE = 35,
            Title = "Отстранение от работы без оплаты",
            ShortTitle = "НБ",
            Reason = "Отстранение от работы (недопущение к работе) по причинам, предусмотренным законодательством, без начисления заработной платы",
            DayType = DayType.SuspensionFromWorkUnpaid,
        },
        new (){
            CODE = 36,
            Title = "Простой при задержке оплаты",
            ShortTitle = "НЗ",
            Reason = "Время приостановки работы в случае задержки выплаты заработной платы",
            DayType = DayType.WorkSuspension,
        },
    };

    #endregion

    public static List<TimeInterval> TimeIntervals => AllTimeIntervals.Where(e => NoAppearencesToShow().Contains(e.DayType)).ToList();

    public static Dictionary<DayType, TimeInterval> AllTimeIntervalsDict = AllTimeIntervals.ToDictionary(e => e.DayType);

    public static Dictionary<DayType, TimeInterval> TimeIntervalsDict = TimeIntervals.ToDictionary(e => e.DayType);

    public static string GetTimeIntervalShortName(Day day)
    {
        return day.Type switch
        {
            DayType.WorkingDay or DayType.WorkingNight or DayType.WorkingOvertime or DayType.WorkingWatch => day.Hours.ToString("G3"),
            _ => AllTimeIntervalsDict[day.Type].ShortTitle,
        };
    }

    public static DayType[] NoAppearencesToShow() => [
        DayType.BusinessTrip,
        DayType.TrainingInterrupt,
        DayType.TrainingInterruptOther,
        DayType.VacationPaid,
        DayType.VacationUnpaid,
        DayType.VacationLegalUnpaid,
        DayType.VacationUnpaidAdditional,
        DayType.VacationEduPaid,
        DayType.VacationEduUnpaid,
        DayType.MaternityLeave,
        DayType.MaternityLeave3YearsOld,
        DayType.IllnessPaid,
        DayType.IllnessUnpaid,
        DayType.ForcedAbsenteeism,
        DayType.AbsenteeismGov,
        DayType.UnexcusedAbsenteeism,
        DayType.Strike,
        DayType.UnknownAbsenteeism,
        DayType.SuspensionFromWorkPaid,
        DayType.SuspensionFromWorkUnpaid,
        DayType.WorkSuspension,
    ];
}