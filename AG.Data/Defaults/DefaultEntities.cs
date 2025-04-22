using AG.Core.Enums;
using AG.Core.Policy;
using AG.Data.Entities;
using AG.Data.Entities.RelationshipTables;

namespace AG.Data.Defaults
{
    public class DefaultEntities
    {
        public const string MANAGERS_FUNC = "Руководители";
        public const string SPECIALISTS_FUNC = "Специалисты";
        public const string SCIENTIFIC_FUNC = "Научные сотрудники";

        public static Guid DEFAULT_ESTABLISHMENT_ID = new Guid("00000000-0000-0000-0000-000000000001");
        public static Guid DEFAULT_DAYOFF_ID = new Guid("00000000-0000-0000-0000-000000000026");

        #region Time Intervals
        public static IEnumerable<TimeIntervalEntity> TimeIntervals()
        {
            return new List<TimeIntervalEntity>()
            {
                new (){
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    CODE = 1,
                    Title = "Работа в дневное время",
                    ShortTitle = "Я",
                    Reason = "Продолжительность работы в дневное время",
                    DayType = DayType.WorkingDay,
                },
                new (){
                    Id = new Guid("00000000-0000-0000-0000-000000000002"),
                    CODE = 2,
                    Title = "Работа в ночное время",
                    ShortTitle = "Н",
                    Reason = "Продолжительность работы в ночное время",
                    DayType = DayType.WorkingNight
                },
                new (){
                    Id = new Guid("00000000-0000-0000-0000-000000000003"),
                    CODE = 3,
                    Title = "Работа в выходные",
                    ShortTitle = "РВ",
                    Reason = "Продолжительность работы в выходные и нерабочие дни",
                    DayType = DayType.WorkingHoliday
                },
                new (){
                    Id = new Guid("00000000-0000-0000-0000-000000000004"),
                    CODE = 4,
                    Title = "Сверхурочная работа",
                    ShortTitle = "С",
                    Reason = "Продолжительность сверхурочной работы",
                    DayType = DayType.WorkingOvertime
                },
                new (){
                    Id = new Guid("00000000-0000-0000-0000-000000000005"),
                    CODE = 5,
                    Title = "Работа вахтовым методом",
                    ShortTitle = "ВМ",
                    Reason = "Продолжительность работы вахтовым методом",
                    DayType = DayType.WorkingWatch,
                },
                new (){
                    Id = new Guid("00000000-0000-0000-0000-000000000006"),
                    CODE = 6,
                    Title = "Командировка",
                    ShortTitle = "К",
                    Reason = "Служебная командировка",
                    DayType = DayType.BusinessTrip,
                },
                new (){
                    Id = new Guid("00000000-0000-0000-0000-000000000007"),
                    CODE = 7,
                    Title = "Повышение квалификации",
                    ShortTitle = "ПК",
                    Reason = "Повышение квалификации с отрывом от работы",
                    DayType = DayType.TrainingInterrupt,
                },
                new (){
                    Id = new Guid("00000000-0000-0000-0000-000000000008"),
                    CODE = 8,
                    Title = "Повышение квалификации (другое)",
                    ShortTitle = "ПМ",
                    Reason = "Повышение квалификации с отрывом от работы в другой местности",
                    DayType = DayType.TrainingInterruptOther,
                },
                new (){
                    Id = new Guid("00000000-0000-0000-0000-000000000009"),
                    CODE = 9,
                    Title = "Основной оплачиваемый отпуск",
                    ShortTitle = "ОТ",
                    Reason = "Ежегодный основной оплачиваемый отпус",
                    DayType = DayType.VacationPaid
                },
                new (){
                    Id = new Guid("00000000-0000-0000-0000-000000000010"),
                    CODE = 10,
                    Title = "Дополнительный оплачиваемый отпуск",
                    ShortTitle = "ОД",
                    Reason = "Ежегодный дополнительный оплачиваемый отпуск",
                    DayType = DayType.VacationPaidAdditional
                },
                new (){
                    Id = new Guid("00000000-0000-0000-0000-000000000011"),
                    CODE = 11,
                    Title = "Доп. отпуск по обучению с оплатой",
                    ShortTitle = "У",
                    Reason = "Дополнительный отпуск в связи с обучением с сохранением среднего заработка работникам, совмещающим работу с обучением",
                    DayType = DayType.VacationEduPaid
                },
                new (){
                    Id = new Guid("00000000-0000-0000-0000-000000000012"),
                    CODE = 12,
                    Title = "Сокращенный день для обучающихся с оплатой",
                    ShortTitle = "УВ",
                    Reason = "Сокращенная продолжительность рабочего времени для обучающихся без отрыва от производства с частичным сохранением заработной платы",
                    DayType = DayType.ShortDayEduPaid
                },
                new (){
                    Id = new Guid("00000000-0000-0000-0000-000000000013"),
                    CODE = 13,
                    Title = "Доп. отпуск по обучению без оплаты",
                    ShortTitle = "УД",
                    Reason = "Дополнительный отпуск в связи с обучением без сохранения заработной платы",
                    DayType = DayType.VacationEduUnpaid
                },
                new (){
                    Id = new Guid("00000000-0000-0000-0000-000000000014"),
                    CODE = 14,
                    Title = "Отпуск по беременности и родам",
                    ShortTitle = "Р",
                    Reason = "Отпуск по беременности и родам (отпуск в связи с усыновлением новорожденного ребенка)",
                    DayType = DayType.MaternityLeave
                },
                new (){
                    Id = new Guid("00000000-0000-0000-0000-000000000015"),
                    CODE = 15,
                    Title = "Отпуск по уходу за ребенком",
                    ShortTitle = "ОЖ",
                    Reason = "Отпуск по уходу за ребенком до достижения им возраста трех лет",
                    DayType = DayType.MaternityLeave3YearsOld
                },
                new (){
                    Id = new Guid("00000000-0000-0000-0000-000000000016"),
                    CODE = 16,
                    Title = "Отпуск без сохранения заработной платы (работодатель)",
                    ShortTitle = "ДО",
                    Reason = "Отпуск без сохранения заработной платы, предоставленный работнику по разрешению работодателя",
                    DayType = DayType.VacationUnpaid,
                },
                new (){
                    Id = new Guid("00000000-0000-0000-0000-000000000017"),
                    CODE = 17,
                    Title = "Отпуск без сохранения заработной платы (законодательство)",
                    ShortTitle = "ОЗ",
                    Reason = "Отпуск без сохранения заработной платы при условиях, предусмотренных действующим законодательством Российской Федерации",
                    DayType = DayType.VacationLegalUnpaid
                },
                new (){
                    Id = new Guid("00000000-0000-0000-0000-000000000018"),
                    CODE = 18,
                    Title = "Доп. отпуск без оплаты",
                    ShortTitle = "ДБ",
                    Reason = "Ежегодный дополнительный отпуск без сохранения заработной платы",
                    DayType = DayType.VacationUnpaidAdditional,
                },
                new (){
                    Id = new Guid("00000000-0000-0000-0000-000000000019"),
                    CODE = 19,
                    Title = "Больничный",
                    ShortTitle = "Б",
                    Reason = "Временная нетрудоспособность (кроме случаев, предусмотренных кодом \"Т\") с назначением пособия согласно законодательству",
                    DayType = DayType.IllnessPaid
                },
                new (){
                    Id = new Guid("00000000-0000-0000-0000-000000000020"),
                    CODE = 20,
                    Title = "Больничный (без оплаты)",
                    ShortTitle = "Т",
                    Reason = "Временная нетрудоспособность без назначения пособия в случаях, предусмотренных законодательством",
                    DayType = DayType.IllnessUnpaid
                },
                new (){
                    Id = new Guid("00000000-0000-0000-0000-000000000021"),
                    CODE = 21,
                    Title = "Сокращенный рабочий день",
                    ShortTitle = "ЛЧ",
                    Reason = "Сокращенная продолжительность рабочего времени против нормальной продолжительности рабочего дня в случаях, предусмотренных законодательством",
                    DayType = DayType.ShortDayLegal,
                },
                new (){
                    Id = new Guid("00000000-0000-0000-0000-000000000022"),
                    CODE = 22,
                    Title = "Время вынужденного прогула",
                    ShortTitle = "ПВ",
                    Reason = "Время вынужденного прогула в случае признания увольнения, перевода на другую работу или отстранения от работы незаконными с восстановлением на прежней работе",
                    DayType = DayType.ForcedAbsenteeism,
                },
                new (){
                    Id = new Guid("00000000-0000-0000-0000-000000000023"),
                    CODE = 23,
                    Title = "Прогулы (законодательство)",
                    ShortTitle = "Г",
                    Reason = "Невыходы на время исполнения государственных или общественных обязанностей согласно законодательству",
                    DayType = DayType.AbsenteeismGov,
                },
                new (){
                    Id = new Guid("00000000-0000-0000-0000-000000000024"),
                    CODE = 24,
                    Title = "Прогулы (неуважит.)",
                    ShortTitle = "ПР",
                    Reason = "Прогулы (отсутствие на рабочем месте без уважительных причин в течение времени, установленного законодательством)",
                    DayType = DayType.UnexcusedAbsenteeism
                },
                new (){
                    Id = new Guid("00000000-0000-0000-0000-000000000025"),
                    CODE = 25,
                    Title = "Сокращенный рабочий день",
                    ShortTitle = "НС",
                    Reason = "Продолжительность работы в режиме неполного рабочего времени по инициативе работодателя в случаях, предусмотренных законодательством",
                    DayType = DayType.ShortDayEmployer,
                },
                new (){
                    Id = new Guid("00000000-0000-0000-0000-000000000026"),
                    CODE = 26,
                    Title = "Выходные дни",
                    ShortTitle = "В",
                    Reason = "Выходные дни (еженедельный отпуск) и нерабочие праздничные дни",
                    DayType = DayType.DayOff,
                },
                new (){
                    Id = new Guid("00000000-0000-0000-0000-000000000027"),
                    CODE = 27,
                    Title = "Доп. выходные дни (оплачиваемые)",
                    ShortTitle = "ОВ",
                    Reason = "Дополнительные выходные дни (оплачиваемые)",
                    DayType = DayType.AdditionalDayOffPaid,
                },
                new (){
                    Id = new Guid("00000000-0000-0000-0000-000000000028"),
                    CODE = 28,
                    Title = "Доп. выходные дни (неоплачиваемые)",
                    ShortTitle = "НВ",
                    Reason = "Дополнительные выходные дни (без сохранения заработной платы)",
                    DayType = DayType.AdditionalDayOffUnpaid,
                },
                new (){
                    Id = new Guid("00000000-0000-0000-0000-000000000029"),
                    CODE = 29,
                    Title = "Забастовка",
                    ShortTitle = "ЗБ",
                    Reason = "Забастовка (при условиях и в порядке, предусмотренных законом)",
                    DayType = DayType.Strike,
                },
                new (){
                    Id = new Guid("00000000-0000-0000-0000-000000000030"),
                    CODE = 30,
                    Title = "Неявка",
                    ShortTitle = "НН",
                    Reason = "Неявки по невыясненным причинам (до выяснения обстоятельств)",
                    DayType = DayType.UnknownAbsenteeism,
                },
                new (){
                    Id = new Guid("00000000-0000-0000-0000-000000000031"),
                    CODE = 31,
                    Title = "Простой (работодатель)",
                    ShortTitle = "РП",
                    Reason = "Время простоя по вине работодателя",
                    DayType = DayType.StrikeByEmployer,
                },
                new (){
                    Id = new Guid("00000000-0000-0000-0000-000000000032"),
                    CODE = 32,
                    Title = "Простой (искл.)",
                    ShortTitle = "НП",
                    Reason = "Время простоя по причинам, не зависящим от работодателя и работника",
                    DayType = DayType.StrikeOther,
                },
                new (){
                    Id = new Guid("00000000-0000-0000-0000-000000000033"),
                    CODE = 33,
                    Title = "Простой (работник)",
                    ShortTitle = "ВП",
                    Reason = "Время простоя по вине работника",
                    DayType = DayType.StrikeByEmployee,
                },
                new (){
                    Id = new Guid("00000000-0000-0000-0000-000000000034"),
                    CODE = 34,
                    Title = "Отстранение от работы с оплатой",
                    ShortTitle = "НО",
                    Reason = "Отстранение от работы (недопущение к работе) с оплатой (пособием) в соответствии с законодательством",
                    DayType = DayType.SuspensionFromWorkPaid,
                },
                new (){
                    Id = new Guid("00000000-0000-0000-0000-000000000035"),
                    CODE = 35,
                    Title = "Отстранение от работы без оплаты",
                    ShortTitle = "НБ",
                    Reason = "Отстранение от работы (недопущение к работе) по причинам, предусмотренным законодательством, без начисления заработной платы",
                    DayType = DayType.SuspensionFromWorkUnpaid,
                },
                new (){
                    Id = new Guid("00000000-0000-0000-0000-000000000036"),
                    CODE = 36,
                    Title = "Простой при задержке оплаты",
                    ShortTitle = "НЗ",
                    Reason = "Время приостановки работы в случае задержки выплаты заработной платы",
                    DayType = DayType.WorkSuspension,
                },
            };
        }
        #endregion

        #region Employees
        public static List<EmployeeEntity> Employees()
        {
            return new List<EmployeeEntity>()
            {
                new()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    LastName = "Иванов",
                    FirstName = "Иван",
                    MiddleName = "Иванович",
                    Email = "ivanov@ag.ru",
                },
                new()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000002"),
                    LastName = "Петров",
                    FirstName = "Петр",
                    MiddleName = "Петрович",
                    Email = "petrov@ag.ru",
                },
                new()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000003"),
                    LastName = "Георгиев",
                    FirstName = "Георгий",
                    MiddleName = "Георигевич",
                    Email = "georgiev@ag.ru",
                },
                new()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000004"),
                    LastName = "Васечкин",
                    FirstName = "Василий",
                    MiddleName = "Васильевич",
                    Email = "vasechkin@ag.ru",
                },
                new()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000005"),
                    LastName = "Дмитриев",
                    FirstName = "Дмитрий",
                    MiddleName = "Дмитриевич",
                    Email = "dmitriev@ag.ru",
                },
                new()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000006"),
                    LastName = "Александров",
                    FirstName = "Александр",
                    MiddleName = "Александрович",
                    Email = "alexandrov@ag.ru",
                },
                new()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000007"),
                    LastName = "Юрьев",
                    FirstName = "Юрий",
                    MiddleName = "Юрьевич",
                    Email = "yuriev@ag.ru",
                },
                new()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000008"),
                    LastName = "Сергеев",
                    FirstName = "Сергей",
                    MiddleName = "Сергеевич",
                    Email = "sergeev@ag.ru",
                },
                new()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000009"),
                    LastName = "Артемов",
                    FirstName = "Артем",
                    MiddleName = "Артемович",
                    Email = "artemov@ag.ru",
                },
                new()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000010"),
                    LastName = "Кириллов",
                    FirstName = "Кирилл",
                    MiddleName = "Кириллович",
                    Email = "kirilov@ag.ru",
                },
            };
        }
        #endregion

        #region Departments
        public static List<DepartmentEntity> Departments()
        {
            return new List<DepartmentEntity>()
            {
                new()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    Name = "Подразделение 1",
                    Header = "Иванов И. И.",
                    EstablishmentId = new Guid("00000000-0000-0000-0000-000000000001"),
                },
                new()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000002"),
                    Name = "Подразделение 2",
                    Header = "Петров П. П.",
                    EstablishmentId = new Guid("00000000-0000-0000-0000-000000000001"),
                },
                new()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000003"),
                    Name = "Подразделение 3",
                    Header = "Георгиев Г. Г.",
                    EstablishmentId = new Guid("00000000-0000-0000-0000-000000000001"),
                },
            };
        }
        #endregion

        #region Functions
        public static List<FunctionEntity> Functions() => new List<FunctionEntity>()
        {
            new()
            {
                Id = new Guid("00000000-0000-0000-1000-000000000001"),
                Name = "Директор",
                ShortName = "директор",
                Category = MANAGERS_FUNC
            },
            new()
            {
                Id = new Guid("00000000-0000-0000-1000-000000000002"),
                Name = "Исполняющий обязанности директора",
                ShortName = "и.о. директора",
                Category = MANAGERS_FUNC,
            },
            new()
            {
                Id = new Guid("00000000-0000-0000-1000-000000000003"),
                Name = "Заместитель директора",
                ShortName = "зам. директора",
                Category = MANAGERS_FUNC,
            },
            new()
            {
                Id = new Guid("00000000-0000-0000-1000-000000000004"),
                Name = "Исполняющий обязанности заместителя директора",
                ShortName = "и.о.зам. директора",
                Category = MANAGERS_FUNC,
            },
            new()
            {
                Id = new Guid("00000000-0000-0000-1000-000000000005"),
                Name = "Помощник директора",
                ShortName = "пом. директора",
                Category = MANAGERS_FUNC,
            },
            new()
            {
                Id = new Guid("00000000-0000-0000-1000-000000000006"),
                Name = "Заведующий подразделением",
                ShortName = "зав.подр.",
                Category = MANAGERS_FUNC,
            },
            new()
            {
                Id = new Guid("00000000-0000-0000-1000-000000000007"),
                Name = "Исполняющий обязанности заведующего подразделения",
                ShortName = "и.о.зав.подр.",
                Category = MANAGERS_FUNC,
            },


            new()
            {
                Id = new Guid("00000000-0000-0000-2000-100000000001"),
                Name = "Главный бухгалтер",
                ShortName = "гл.бухг.",
                Category = SPECIALISTS_FUNC,
            },
            new()
            {
                Id = new Guid("00000000-0000-0000-2000-100000000002"),
                Name = "Ведущий бухгалтер",
                ShortName = "вед.бухг.",
                Category = SPECIALISTS_FUNC,
            },
            new()
            {
                Id = new Guid("00000000-0000-0000-2000-100000000003"),
                Name = "Бухгалтер",
                ShortName = "бухг.",
                Category = SPECIALISTS_FUNC,
            },


            new()
            {
                Id = new Guid("00000000-0000-0000-2000-200000000001"),
                Name = "Ведущий специалист отдела кадров",
                ShortName = "вед.спец.отд.кадр.",
                Category = SPECIALISTS_FUNC,
            },
            new()
            {
                Id = new Guid("00000000-0000-0000-2000-200000000002"),
                Name = "Специалист отдела кадров",
                ShortName = "спец.отд.кадр.",
                Category = SPECIALISTS_FUNC,
            },



            //3000 - scientific functions
            new()
            {
                Id = new Guid("00000000-0000-0000-3000-000000000001"),
                Name = "Главный научный сотрудник",
                ShortName = "г.н.с.",
                Category = SCIENTIFIC_FUNC,
            },
            new()
            {
                Id = new Guid("00000000-0000-0000-3000-000000000002"),
                Name = "Ведущий научный сотрудник",
                ShortName = "в.н.с.",
                Category = SCIENTIFIC_FUNC,
            },
            new()
            {
                Id = new Guid("00000000-0000-0000-3000-000000000003"),
                Name = "Старший научный сотрудник",
                ShortName = "с.н.с.",
                Category = SCIENTIFIC_FUNC,
            },
            new()
            {
                Id = new Guid("00000000-0000-0000-3000-000000000004"),
                Name = "Научный сотрудник",
                ShortName = "н.с.",
                Category = SCIENTIFIC_FUNC,
            },
            new()
            {
                Id = new Guid("00000000-0000-0000-3000-000000000005"),
                Name = "Младший научный сотрудник",
                ShortName = "м.н.с.",
                Category = SCIENTIFIC_FUNC,
            },
            new()
            {
                Id = new Guid("00000000-0000-0000-3000-000000000006"),
                Name = "Инженер-исследователь",
                ShortName = "инж.-иссл.",
                Category = SCIENTIFIC_FUNC,
            },

            new()
            {
                Id = new Guid("00000000-0000-0000-4000-000000000001"),
                Name = "Инженер",
                ShortName = "инженер",
                Category = SPECIALISTS_FUNC,
            },
        };
        #endregion

        #region Establishments
        public static List<EstablishmentEntity> Establishments()
        {
            return new List<EstablishmentEntity>()
            {
                new()
                {
                    Id = DEFAULT_ESTABLISHMENT_ID,
                    FullName = "ООО \"Организация\"",
                    ShortName = "ORG",
                    Header = "Иванов И.И.",
                    HeaderFunction = "И.о. директора",
                    INN = "0000000000",
                    OGRN = "0000000000000",
                    RegistrationDate = new DateTime(2012,12,12),
                },
            };
        }
        #endregion

        #region TimesheetDefaults
        public static IEnumerable<TimesheetDefaults> TimesheetDefaults()
        {
            return new List<TimesheetDefaults>()
            {
                new()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    DepartmentId = new Guid("00000000-0000-0000-0000-000000000001"),
                    AccountingExecutor = "Иванов И. И.",
                    AccountingExecutorFunction = "Директор"
                },
                new()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000002"),
                    DepartmentId = new Guid("00000000-0000-0000-0000-000000000002"),
                    AccountingExecutor = "",
                    AccountingExecutorFunction = ""
                },
                new()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000003"),
                    DepartmentId = new Guid("00000000-0000-0000-0000-000000000003"),
                    AccountingExecutor = "",
                    AccountingExecutorFunction = ""
                },
            };
        }
        #endregion

        #region CorrectionDays
        public static List<CorrectionDayEntity> CorrectionDays() => new List<CorrectionDayEntity>()
        {
            //January
            new()
            {
                Id = new Guid("00000000-0000-0000-0001-000000000001"),
                Month = 1, Day = 1,
                Title = "Новогодние каникулы",
                Type = Core.Enums.CorrectionDayType.DayOff
            },
            new()
            {
                Id = new Guid("00000000-0000-0000-0001-000000000002"),
                Month = 1, Day = 2,
                Title = "Новогодние каникулы",
                Type = Core.Enums.CorrectionDayType.DayOff
            },
            new()
            {
                Id = new Guid("00000000-0000-0000-0001-000000000003"),
                Month = 1, Day = 3,
                Title = "Новогодние каникулы",
                Type = Core.Enums.CorrectionDayType.DayOff
            },
            new()
            {
                Id = new Guid("00000000-0000-0000-0001-000000000004"),
                Month = 1, Day = 4,
                Title = "Новогодние каникулы",
                Type = Core.Enums.CorrectionDayType.DayOff
            },
            new()
            {
                Id = new Guid("00000000-0000-0000-0001-000000000005"),
                Month = 1, Day = 5,
                Title = "Новогодние каникулы",
                Type = Core.Enums.CorrectionDayType.DayOff
            },
            new()
            {
                Id = new Guid("00000000-0000-0000-0001-000000000006"),
                Month = 1, Day = 6,
                Title = "Новогодние каникулы",
                Type = Core.Enums.CorrectionDayType.DayOff
            },
            new()
            {
                Id = new Guid("00000000-0000-0000-0001-000000000007"),
                Month = 1, Day = 7,
                Title = "Новогодние каникулы",
                Type = Core.Enums.CorrectionDayType.DayOff
            },
            new()
            {
                Id = new Guid("00000000-0000-0000-0001-000000000008"),
                Month = 1, Day = 8,
                Title = "Новогодние каникулы",
                Type = Core.Enums.CorrectionDayType.DayOff
            },
            //February
            new()
            {
                Id = new Guid("00000000-0000-0000-0002-000000000007"),
                Month = 2, Day = 23,
                Title = "День защитника Отечества",
                Type = Core.Enums.CorrectionDayType.DayOff
            },
            //March
            new()
            {
                Id = new Guid("00000000-0000-0000-0003-000000000008"),
                Month = 3, Day = 8,
                Title = "Международный женский день",
                Type = Core.Enums.CorrectionDayType.DayOff
            },
            //May
            new()
            {
                Id = new Guid("00000000-0000-0000-0005-000000000001"),
                Month = 5, Day = 1,
                Title = "Праздник Весны и Труда",
                Type = Core.Enums.CorrectionDayType.DayOff
            },
            new()
            {
                Id = new Guid("00000000-0000-0000-0005-000000000009"),
                Month = 5, Day = 9,
                Title = "День Победы",
                Type = Core.Enums.CorrectionDayType.DayOff
            },
            //June
            new()
            {
                Id = new Guid("00000000-0000-0000-0006-000000000001"),
                Month = 6, Day = 1,
                Title = "День защиты детей",
                Type = Core.Enums.CorrectionDayType.DayOff
            },

            //June
            new()
            {
                Id = new Guid("00000000-0000-0000-0006-000000000012"),
                Month = 6, Day = 12,
                Title = "День России",
                Type = Core.Enums.CorrectionDayType.DayOff
            },

            //November
            new()
            {
                Id = new Guid("00000000-0000-0000-0011-000000000012"),
                Month = 11, Day = 12,
                Title = "День народного единства",
                Type = Core.Enums.CorrectionDayType.DayOff
            },
        };
        #endregion

        #region Schedules
        public static List<ScheduleEntity> Schedules() => new List<ScheduleEntity>()
        {
            new()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000001"),
                Title = "Стандартная 40-часовая неделя",
            }
        }; 
        #endregion

        #region SchedulesDays
        public static List<ScheduleDayEntity> SchedulesDays() => new()
        {
            new()
            {
                Id = new Guid("00000000-0000-0000-0001-000000000001"),
                ScheduleId = new Guid("00000000-0000-0000-0000-000000000001"),
                DayOfWeek = DayOfWeek.Monday,
                IsDayOff = false,
                WorkBegin = (new TimeOnly(8,30,0)).Ticks,
                WorkEnd = (new TimeOnly(17,15,0)).Ticks,
                BreakBegin = (new TimeOnly(12,0,0)).Ticks,
                BreakEnd = (new TimeOnly(12,45,0)).Ticks,
            },
            new()
            {
                Id = new Guid("00000000-0000-0000-0001-000000000002"),
                ScheduleId = new Guid("00000000-0000-0000-0000-000000000001"),
                DayOfWeek = DayOfWeek.Tuesday,
                IsDayOff = false,
                WorkBegin = (new TimeOnly(8,30,0)).Ticks,
                WorkEnd = (new TimeOnly(17,15,0)).Ticks,
                BreakBegin = (new TimeOnly(12,0,0)).Ticks,
                BreakEnd = (new TimeOnly(12,45,0)).Ticks,
            },
            new()
            {
                Id = new Guid("00000000-0000-0000-0001-000000000003"),
                ScheduleId = new Guid("00000000-0000-0000-0000-000000000001"),
                DayOfWeek = DayOfWeek.Wednesday,
                IsDayOff = false,
                WorkBegin = (new TimeOnly(8,30,0)).Ticks,
                WorkEnd = (new TimeOnly(17,15,0)).Ticks,
                BreakBegin = (new TimeOnly(12,0,0)).Ticks,
                BreakEnd = (new TimeOnly(12,45,0)).Ticks,
            },
            new()
            {
                Id = new Guid("00000000-0000-0000-0001-000000000004"),
                ScheduleId = new Guid("00000000-0000-0000-0000-000000000001"),
                DayOfWeek = DayOfWeek.Thursday,
                IsDayOff = false,
                WorkBegin = (new TimeOnly(8,30,0)).Ticks,
                WorkEnd = (new TimeOnly(17,15,0)).Ticks,
                BreakBegin = (new TimeOnly(12,0,0)).Ticks,
                BreakEnd = (new TimeOnly(12,45,0)).Ticks,
            },
            new()
            {
                Id = new Guid("00000000-0000-0000-0001-000000000005"),
                ScheduleId = new Guid("00000000-0000-0000-0000-000000000001"),
                DayOfWeek = DayOfWeek.Monday,
                IsDayOff = false,
                WorkBegin = (new TimeOnly(8,30,0)).Ticks,
                WorkEnd = (new TimeOnly(17,0,0)).Ticks,
                BreakBegin = (new TimeOnly(12,0,0)).Ticks,
                BreakEnd = (new TimeOnly(12,45,0)).Ticks,
            },
            new()
            {
                Id = new Guid("00000000-0000-0000-0001-000000000006"),
                ScheduleId = new Guid("00000000-0000-0000-0000-000000000001"),
                DayOfWeek = DayOfWeek.Saturday,
                IsDayOff = true,
                WorkBegin = 0,
                WorkEnd = 0,
                BreakBegin = 0,
                BreakEnd = 0,
            },
            new()
            {
                Id = new Guid("00000000-0000-0000-0001-000000000007"),
                ScheduleId = new Guid("00000000-0000-0000-0000-000000000001"),
                DayOfWeek = DayOfWeek.Sunday,
                IsDayOff = true,
                WorkBegin = 0,
                WorkEnd = 0,
                BreakBegin = 0,
                BreakEnd = 0,
            },
        };
        #endregion

        #region Users
        public static List<UserEntity> Users() => new()
        {
            new()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000001"),
                Role = DefaultRoles.ADMIN,
                Username = "admin",
                PasswordHash = "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=",
                Email = "",
                IsActivatedAccount = true,
                IsEmailConfirmed = true,
                CreatedAt = DateTime.Now,
                LastVisit = null,
                DepartmentId = null,
                EmployeeId = null, 
            },

            new()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000002"),
                Role = DefaultRoles.HR,
                Username = "hr",
                PasswordHash = "1b52f3a2e15148731314bf167145c54565ed2385a862b5eb7771eaf719e4f82e",
                Email = "",
                IsActivatedAccount = true,
                IsEmailConfirmed = true,
                CreatedAt = DateTime.Now,
                DepartmentId = null,
                EmployeeId = null,
            },
            new()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000005"),
                Role = DefaultRoles.EMPLOYEE,
                Username = "employee",
                PasswordHash = "",
                Email = "",
                IsActivatedAccount = true,
                IsEmailConfirmed = true,
                CreatedAt = DateTime.Now,
                DepartmentId = null,
                EmployeeId = null,
            },

        };
        #endregion

        //Relationship tables
        #region EmployeeToDepartment
        public static List<EmployeeToDepartment> EmployeeWithDepartment() => new()
        {
            new()
            {
                Id              = new Guid("00000000-0000-0000-0000-000000000001"),
                EmployeeId      = new Guid("00000000-0000-0000-0000-000000000001"),
                DepartmentId    = new Guid("00000000-0000-0000-0000-000000000001"),
                FunctionId      = new Guid("00000000-0000-0000-1000-000000000006"), //зав.подр.
                ScheduleId      = new Guid("00000000-0000-0000-0000-000000000001"),
                Rate = 1F,
                AssignmentDate = new DateTime(2012,12,12),
                TimesheetNumber = 1,
                IsConcurrent = false,
                Reason = ""
            },
            new()
            {
                Id              = new Guid("00000000-0000-0000-0000-000000000002"),
                EmployeeId      = new Guid("00000000-0000-0000-0000-000000000002"),
                DepartmentId    = new Guid("00000000-0000-0000-0000-000000000002"),
                FunctionId      = new Guid("00000000-0000-0000-1000-000000000006"), //зав.подр.
                ScheduleId      = new Guid("00000000-0000-0000-0000-000000000001"),
                Rate = 1F,
                AssignmentDate = new DateTime(2013,01,18),
                TimesheetNumber = 1,
                IsConcurrent = false,
                Reason = ""
            },
            new()
            {
                Id              = new Guid("00000000-0000-0000-0000-000000000003"),
                EmployeeId      = new Guid("00000000-0000-0000-0000-000000000003"),
                DepartmentId    = new Guid("00000000-0000-0000-0000-000000000003"),
                FunctionId      = new Guid("00000000-0000-0000-1000-000000000006"), //зав.подр.
                ScheduleId      = new Guid("00000000-0000-0000-0000-000000000001"),
                Rate = 1F,
                AssignmentDate = new DateTime(2013,01,17),
                TimesheetNumber = 1,
                IsConcurrent = false,
                Reason = ""
            },
            new()
            {
                Id              = new Guid("00000000-0000-0000-0000-000000000004"),
                EmployeeId      = new Guid("00000000-0000-0000-0000-000000000004"),
                DepartmentId    = new Guid("00000000-0000-0000-0000-000000000001"),
                FunctionId      = new Guid("00000000-0000-0000-2000-100000000001"), //гл.бух.
                ScheduleId      = new Guid("00000000-0000-0000-0000-000000000001"),
                Rate = 1F,
                AssignmentDate = new DateTime(2012,12,21),
                TimesheetNumber = 2,
                IsConcurrent = false,
                Reason = ""
            },
            new()
            {
                Id              = new Guid("00000000-0000-0000-0000-000000000005"),
                EmployeeId      = new Guid("00000000-0000-0000-0000-000000000005"),
                DepartmentId    = new Guid("00000000-0000-0000-0000-000000000001"),
                FunctionId      = new Guid("00000000-0000-0000-3000-000000000002"), //в.н.с.
                ScheduleId      = new Guid("00000000-0000-0000-0000-000000000001"),
                Rate = 0.5F,
                AssignmentDate = new DateTime(2013,02,12),
                TimesheetNumber = 3,
                IsConcurrent = false,
                Reason = ""
            },
            new()
            {
                Id              = new Guid("00000000-0000-0000-0000-000000000006"),
                EmployeeId      = new Guid("00000000-0000-0000-0000-000000000006"),
                DepartmentId    = new Guid("00000000-0000-0000-0000-000000000002"),
                FunctionId      = new Guid("00000000-0000-0000-3000-000000000001"), //г.н.с.
                ScheduleId      = new Guid("00000000-0000-0000-0000-000000000001"),
                Rate = 1F,
                AssignmentDate = new DateTime(2021,06,05),
                TimesheetNumber = 2,
                IsConcurrent = false,
                Reason = ""
            },
            new()
            {
                Id              = new Guid("00000000-0000-0000-0000-000000000007"),
                EmployeeId      = new Guid("00000000-0000-0000-0000-000000000007"),
                DepartmentId    = new Guid("00000000-0000-0000-0000-000000000002"),
                FunctionId      = new Guid("00000000-0000-0000-3000-000000000006"), //инж.-иссл.
                ScheduleId      = new Guid("00000000-0000-0000-0000-000000000001"),
                Rate = 0.125F,
                AssignmentDate = new DateTime(2022,06,03),
                TimesheetNumber = 3,
                IsConcurrent = true,
                Reason = ""
            },
            new()
            {
                Id              = new Guid("00000000-0000-0000-0000-000000000008"),
                EmployeeId      = new Guid("00000000-0000-0000-0000-000000000008"),
                DepartmentId    = new Guid("00000000-0000-0000-0000-000000000002"),
                FunctionId      = new Guid("00000000-0000-0000-3000-000000000005"), //м.н.с.
                ScheduleId      = new Guid("00000000-0000-0000-0000-000000000001"),
                Rate = 0.5F,
                AssignmentDate = new DateTime(2022,09,01),
                TimesheetNumber = 4,
                IsConcurrent = false,
                Reason = ""
            },
            new()
            {
                Id              = new Guid("00000000-0000-0000-0000-000000000009"),
                EmployeeId      = new Guid("00000000-0000-0000-0000-000000000009"),
                DepartmentId    = new Guid("00000000-0000-0000-0000-000000000003"),
                FunctionId      = new Guid("00000000-0000-0000-3000-000000000003"), //с.н.с.
                ScheduleId      = new Guid("00000000-0000-0000-0000-000000000001"),
                Rate = 1F,
                AssignmentDate = new DateTime(2019,04,05),
                TimesheetNumber = 2,
                IsConcurrent = false,
                Reason = ""
            },
            new()
            {
                Id              = new Guid("00000000-0000-0000-0000-000000000010"),
                EmployeeId      = new Guid("00000000-0000-0000-0000-000000000010"),
                DepartmentId    = new Guid("00000000-0000-0000-0000-000000000003"),
                FunctionId      = new Guid("00000000-0000-0000-3000-000000000003"), //с.н.с.
                ScheduleId      = new Guid("00000000-0000-0000-0000-000000000001"),
                Rate = 0.25F,
                AssignmentDate = new DateTime(2019,05,12),
                TimesheetNumber = 3,
                IsConcurrent = true,
                Reason = ""
            },
        };
        #endregion


        #region EmployeeToFunction
        public static List<EmployeeToFunction> EmployeeWithFunction() => new()
        {
            new()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000001"),
                EmployeeId = new Guid("00000000-0000-0000-0000-000000000001"),
                FunctionId = new Guid("00000000-0000-0000-1000-000000000006"), //зав.подр.
                Rate = 1F,
                AssignmentDate = new DateTime(2012,12,12),
                TimesheetNumber = 1,
                IsConcurrent = false,
                Reason = ""
            },
            new()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000002"),
                EmployeeId = new Guid("00000000-0000-0000-0000-000000000002"),
                FunctionId = new Guid("00000000-0000-0000-1000-000000000006"), //зав.подр.
                Rate = 1F,
                AssignmentDate = new DateTime(2013,01,18),
                TimesheetNumber = 1,
                IsConcurrent = false,
                Reason = ""
            },
            new()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000003"),
                EmployeeId = new Guid("00000000-0000-0000-0000-000000000003"),
                FunctionId = new Guid("00000000-0000-0000-1000-000000000006"), //зав.подр.
                Rate = 1F,
                AssignmentDate = new DateTime(2013,01,17),
                TimesheetNumber = 1,
                IsConcurrent = false,
                Reason = ""
            },
            new()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000004"),
                EmployeeId = new Guid("00000000-0000-0000-0000-000000000004"),
                FunctionId = new Guid("00000000-0000-0000-2000-100000000001"), //гл.бух.
                Rate = 1F,
                AssignmentDate = new DateTime(2012,12,21),
                TimesheetNumber = 2,
                IsConcurrent = false,
                Reason = ""
            },
            new()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000005"),
                EmployeeId = new Guid("00000000-0000-0000-0000-000000000005"),
                FunctionId = new Guid("00000000-0000-0000-3000-000000000002"), //в.н.с.
                Rate = 0.5F,
                AssignmentDate = new DateTime(2013,02,12),
                TimesheetNumber = 3,
                IsConcurrent = false,
                Reason = ""
            },
            new()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000006"),
                EmployeeId = new Guid("00000000-0000-0000-0000-000000000006"),
                FunctionId = new Guid("00000000-0000-0000-3000-000000000001"), //г.н.с.
                Rate = 1F,
                AssignmentDate = new DateTime(2021,06,05),
                TimesheetNumber = 2,
                IsConcurrent = false,
                Reason = ""
            },
            new()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000007"),
                EmployeeId = new Guid("00000000-0000-0000-0000-000000000007"),
                FunctionId = new Guid("00000000-0000-0000-3000-000000000006"), //инж.-иссл.
                Rate = 0.125F,
                AssignmentDate = new DateTime(2022,06,03),
                TimesheetNumber = 3,
                IsConcurrent = true,
                Reason = ""
            },
            new()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000008"),
                EmployeeId = new Guid("00000000-0000-0000-0000-000000000008"),
                FunctionId = new Guid("00000000-0000-0000-3000-000000000005"), //м.н.с.
                Rate = 0.5F,
                AssignmentDate = new DateTime(2022,09,01),
                TimesheetNumber = 4,
                IsConcurrent = false,
                Reason = ""
            },
            new()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000009"),
                EmployeeId = new Guid("00000000-0000-0000-0000-000000000009"),
                FunctionId = new Guid("00000000-0000-0000-3000-000000000003"), //с.н.с.
                Rate = 1F,
                AssignmentDate = new DateTime(2019,04,05),
                TimesheetNumber = 2,
                IsConcurrent = false,
                Reason = ""
            },
            new()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000010"),
                EmployeeId = new Guid("00000000-0000-0000-0000-000000000010"),
                FunctionId = new Guid("00000000-0000-0000-3000-000000000003"), //с.н.с.
                Rate = 0.25F,
                AssignmentDate = new DateTime(2019,05,12),
                TimesheetNumber = 3,
                IsConcurrent = true,
                Reason = ""
            },
        };
        #endregion


        #region EmployeeToTimeInterval
        public static List<EmployeeToTimeInterval> EmployeeWithTimeInterval() => new()
        {
            new()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000001"),
                EmployeeId = new Guid("00000000-0000-0000-0000-000000000001"),
                TimeIntervalType = DayType.BusinessTrip, //Командировка
                Begin = new DateTime(2013, 3,12),
                End = new DateTime(2013, 3, 27)
            },
            new()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000002"),
                EmployeeId = new Guid("00000000-0000-0000-0000-000000000002"),
                TimeIntervalType = DayType.BusinessTrip, //Командировка
                Begin = new DateTime(2014, 2,15),
                End = new DateTime(2014, 2, 21)
            },
            new()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000003"),
                EmployeeId = new Guid("00000000-0000-0000-0000-000000000003"),
                TimeIntervalType = DayType.VacationPaid, //Отпуск оплачиваемый
                Begin = new DateTime(2021, 6,12),
                End = new DateTime(2021, 7, 3)
            },
            new()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000004"),
                EmployeeId = new Guid("00000000-0000-0000-0000-000000000004"),
                TimeIntervalType = DayType.VacationPaid, //Отпуск оплачиваемый
                Begin = new DateTime(2013, 9,1),
                End = new DateTime(2013, 9, 5)
            },
            new()
            {
                Id = new Guid("00000000-0000-0000-0000-000000000005"),
                EmployeeId = new Guid("00000000-0000-0000-0000-000000000005"),
                TimeIntervalType = DayType.VacationPaid, //Отпуск оплачиваемый
                Begin = new DateTime(2015, 3,12),
                End = new DateTime(2015, 3, 21)
            },
        };
        #endregion
    }
}
