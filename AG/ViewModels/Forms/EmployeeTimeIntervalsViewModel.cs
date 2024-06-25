using AG.WPF.ViewModel;
using AG.WPF.ViewModels;
using Services.Database;
using Services.Domains;
using Services.Extensions;
using Services.Infrastructure;
using Services.Infrastructure.Logger;
using Services.Translators;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace AG.WPF.ViewModels.Forms
{
    public class EmployeeTimeIntervalsViewModel : ViewModelCore
    {
        #region ctor
        public EmployeeTimeIntervalsViewModel()
        {
            employeeService = ServicesLocator.GetService<IEmployeeService>()!;
            AvailableTimeIntervals = GetAvailableTimeIntervals();
        }
        #endregion

        #region	fields
        private const int AllMonthEntryId = 0;
        private const string AllYearsEntryName = "Все";

        private Guid employeeId;
        private string employeeName;
        private readonly IEmployeeService employeeService;

        private TimeInterval? selectedTimeInterval;
        private ObservableCollection<TimeInterval> timeIntervals = new ObservableCollection<TimeInterval>();
        private IList<Guid> editedTimeIntervalsIds = new List<Guid>();
        private IList<Guid> removedTimeIntervalsIds = new List<Guid>();

        private string selectedFilterYearId;
        private int selectedFilterMonthId = 1;

        private bool isPopupVisible = false;
        private DateTime popupDateFrom;
        private DateTime popupDateTo;
        private string popupComment;
        private TimeIntervalType popupTimeIntervalType;
        private IList<TimeIntervalType> availableTimeIntervals;

        private ObjectOperationType currentOperationType;

        private ObservableCollection<string> filterYears = new();
        private ObservableCollection<FilterMonth> filterMonths = new();

        private ICollectionView employeeTimeIntervals;
        #endregion

        #region	Properties
        public string EmployeeName { get => employeeName; set { employeeName = value; OnChanged(); } }
        public ICollectionView EmployeeTimeIntervals { get => employeeTimeIntervals; set { employeeTimeIntervals = value; OnChanged(); } }
        public ObservableCollection<TimeInterval> TimeIntervals { get => timeIntervals; set { timeIntervals = value; OnChanged(); } }
        public TimeInterval? SelectedTimeInterval { get => selectedTimeInterval; set { selectedTimeInterval = value; OnChanged(); } }

        public ObservableCollection<string> FilterYears { get => filterYears; set { filterYears = value; OnChanged(); } }
        public ObservableCollection<FilterMonth> FilterMonths { get => filterMonths; set { filterMonths = value; OnChanged(); } }

        public string SelectedFilterYear { get => selectedFilterYearId; set { selectedFilterYearId = value; OnChanged(); } }
        public int SelectedFilterMonth { get => selectedFilterMonthId; set { selectedFilterMonthId = value; OnChanged(); } }

        public bool IsPopupVisible { get => isPopupVisible; set { isPopupVisible = value; OnChanged(); } }
        public DateTime PopupDateFrom { get => popupDateFrom; set { popupDateFrom = value; OnChanged(); } }
        public DateTime PopupDateTo { get => popupDateTo; set { popupDateTo = value; OnChanged(); } }
        public string PopupComment { get => popupComment; set { popupComment = value; OnChanged(); } }
        public TimeIntervalType PopupTimeIntervalType { get => popupTimeIntervalType; set { popupTimeIntervalType = value; OnChanged(); } }
        public IList<TimeIntervalType> AvailableTimeIntervals { get => availableTimeIntervals; set { availableTimeIntervals = value; OnChanged(); } }
        #endregion

        #region LoadInterface
        public void LoadInterface(Employee employee)
        {
            employeeId = employee.Id;
            EmployeeName = employee.FullName;

            LoadTimeIntervals();
            UpdateFilterEntries();
        }
        #endregion

        #region UpdateFilterEntries
        public void UpdateFilterEntries()
        {

            var previouslyChosenYearName = SelectedFilterYear;
            var previouslyChosenMonthId = SelectedFilterMonth;

            var years = TimeIntervals
                .Select(ti => ti.Begin.Year)
                .Union(TimeIntervals.Select(ti_to => ti_to.End.Year).ToList())
                .Distinct()
                .Select(y => y.ToString())
                .OrderBy(y => y)
                .ToList();
            var months = TimeIntervals
                .Select(ti => ti.Begin.Month)
                .Union(TimeIntervals.Select(ti_to => ti_to.End.Month).ToList())
                .Distinct()
                .Select(m => new FilterMonth(m, CalendarTranslator.TranslateMonthId(m)))
                .ToList();

            FilterYears.Clear();
            FilterYears.Add(AllYearsEntryName);
            FilterYears.AddRange(years);

            FilterMonths.Clear();
            FilterMonths.Add(new FilterMonth(AllMonthEntryId, AllYearsEntryName));
            FilterMonths.AddRange(months);

            SelectedFilterYear = !FilterYears.Contains(previouslyChosenYearName) ? AllYearsEntryName : previouslyChosenYearName;
            SelectedFilterMonth = filterMonths.Where(m => m.Id == previouslyChosenMonthId).Count() == 0 ? 0 : previouslyChosenMonthId;
        }
        #endregion

        #region FilterTimeIntervals
        public void FilterTimeIntervals()
        {
            //LoadTimeIntervals();

            //if (SelectedFilterYear != AllYearsEntryName || SelectedFilterMonth > AllMonthEntryId)
            //{
            //	var filteredTI = TimeIntervals.ToList();
            //	if (SelectedFilterYear != AllYearsEntryName)
            //	{
            //		filteredTI = filteredTI.Where(ti => ti.Begin.Year.ToString() == SelectedFilterYear).ToList();
            //	}
            //	if (SelectedFilterMonth > AllMonthEntryId)
            //	{
            //		filteredTI = filteredTI.Where(ti => ti.Begin.Month == SelectedFilterMonth).ToList();
            //	}

            //	TimeIntervals = new ObservableCollection<TimeInterval>(filteredTI);
            //}

            employeeTimeIntervals.Filter = TimeIntervalsFilter;
            employeeTimeIntervals.Refresh();
        }
        #endregion

        #region TimeIntervalsFilter
        private bool TimeIntervalsFilter(object item)
        {
            var ti = item as TimeInterval;
            var condition_1 = SelectedFilterYear != AllYearsEntryName ? ti!.Begin.Year.ToString() == SelectedFilterYear : true;
            var condition_2 = SelectedFilterMonth > AllMonthEntryId ? ti!.Begin.Month == SelectedFilterMonth : true;
            return condition_1 && condition_2;
        }
        #endregion

        #region LoadTimeIntervals
        private async void LoadTimeIntervals()
        {
            ShowWaitMessage("Загрузка временных интервалов пользователя", "Подождите");

            var employeeTimeIntervals = await employeeService.GetTimeIntervalAsync(employeeId, FetchAim.Table);

            TimeIntervals.Clear();
            TimeIntervals.AddRange(employeeTimeIntervals);

            EmployeeTimeIntervals = CollectionViewSource.GetDefaultView(TimeIntervals);

            ClearWaitMessage();
        }
        #endregion

        #region MakeTimeInterval
        private TimeInterval MakeTimeInterval()
        {
            return new TimeInterval()
            {
                Id = default,
                Begin = PopupDateFrom,
                End = PopupDateTo,
                TimeIntervalType = PopupTimeIntervalType,
                TimeIntervalTypeId = PopupTimeIntervalType.Id,
                Comment = PopupComment,
                EmployeeId = employeeId
            };
        }
        #endregion

        #region AddTimeInterval
        private bool AddTimeInterval()
        {
            var newTI = MakeTimeInterval();
            if (ValidatePopupInputs(newTI))
            {
                TimeIntervals.Add(newTI);

                return true;
            }
            return false;
        }
        #endregion

        #region EditTimeInterval
        private bool EditTimeInterval()
        {
            if (SelectedTimeInterval != null)
            {
                if (ValidatePopupInputs(SelectedTimeInterval))
                {
                    SelectedTimeInterval.Begin = PopupDateFrom;
                    SelectedTimeInterval.End = PopupDateTo;
                    SelectedTimeInterval.TimeIntervalType = PopupTimeIntervalType;
                    SelectedTimeInterval.Comment = PopupComment;

                    if (SelectedTimeInterval.Id != Guid.Empty)
                        editedTimeIntervalsIds.Add(SelectedTimeInterval.Id);
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region RemoveTimeInterval
        public void RemoveTimeInterval()
        {
            if (SelectedTimeInterval != null)
            {
                removedTimeIntervalsIds.Add(SelectedTimeInterval.Id);
                TimeIntervals.Remove(SelectedTimeInterval);
            }
            else
            {
                MessageBox.Show("Для того, чтобы удалить запись, необходимо сначала ее выбрать", "", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        #endregion

        #region CheckTimeIntervalsCollision
        private bool CheckTimeIntervalsCollision(TimeInterval timeInterval)
        {
            if (currentOperationType == ObjectOperationType.Edit && timeInterval.Begin.Date == SelectedTimeInterval.Begin.Date && timeInterval.End.Date == SelectedTimeInterval.End.Date)
                return true;

            var collisions = TimeIntervals.GetCollisionsWith(timeInterval);
            if (collisions.Count > 0)
            {
                foreach (var collision in collisions)
                    AddValidationMessage(
                        $"Пересечение с интервалом ({collision.Begin.Date.ToShortDateString()}-{collision.End.Date.ToShortDateString()})!",
                        "Пересечение временных интервалов");
                return true;
            }
            return false;
        }
        #endregion

        #region ValidatePopupInputs
        private bool ValidatePopupInputs(TimeInterval ti)
        {
            ClearValidationMessages();
            if (popupDateFrom > popupDateTo)
                AddValidationMessage("Начальная дата не может быть больше конечной", "Ошибка в датах");

            CheckTimeIntervalsCollision(ti);

            return ValidationMessages.Count == 0;
        }
        #endregion

        #region ApplyPendingChanges
        public async Task<bool> ApplyPendingChanges()
        {
            ShowWaitMessage("Выполняется запись неявок в базу", "Пожалуйста, подождите");
            var ret = true;

            try
            {
                var existingTI = await employeeService.GetTimeIntervalAsync(employeeId, FetchAim.Table);
                var ids = existingTI.Select(ti => ti.Id).ToList();

                //Статистика обработки изменений
                var addedCount = 0;
                var editedCount = 0;
                var removedCount = 0;

                foreach (var item in TimeIntervals)
                {
                    bool operationStatus = true;
                    if (ids.Contains(item.Id))
                    {
                        if (editedTimeIntervalsIds.Contains(item.Id))
                        {
                            operationStatus = await employeeService.UpdateTimeIntervalAsync(item);
                            editedCount++;
                        }
                    }
                    else
                    {
                        if (removedTimeIntervalsIds.Contains(item.Id))
                        {
                            operationStatus = await employeeService.DeleteTimeIntervalAsync(item.Id);
                            removedCount++;
                        }
                        else
                        {
                            operationStatus = await employeeService.AddTimeIntervalAsync(item);
                            addedCount++;
                        }
                    }

                    if (!operationStatus)
                    {
                        AddValidationMessage($"Не удалось добавить временной интервал {item.TimeIntervalType?.LongName ?? "Интервал"}: {item.Begin.ToShortDateString()}-{item.End.ToShortDateString()}", "Ошибка записи в базу");
                    }
                }

                MessageBox.Show($"Успешно добавлено {addedCount}, отредактировано {editedCount} и удалено {removedCount} неявок!");
            }
            catch (Exception ex)
            {
                Logger.Log(ex, "Database Time Intervals saving");
                MessageBox.Show($"Ошибка записи в базу перечня интервалов. Причина: {ex.Message}", "Ошибка");
                ret = false;
            }
            finally
            {
                ClearWaitMessage();
            }

            return ret;
        }
        #endregion

        #region ShowPopup
        public void ShowPopup(ObjectOperationType operationType)
        {
            currentOperationType = operationType;
            switch (operationType)
            {
                case ObjectOperationType.Add:
                    IsPopupVisible = true;
                    PopupDateFrom = DateTime.Now.Date;
                    PopupDateTo = DateTime.Now.Date.AddDays(7);
                    PopupTimeIntervalType = AvailableTimeIntervals.FirstOrDefault() ?? new TimeIntervalType();
                    PopupComment = "";
                    break;
                case ObjectOperationType.Edit:
                    if (SelectedTimeInterval != null)
                    {
                        IsPopupVisible = true;
                        PopupDateFrom = SelectedTimeInterval.Begin.Date;
                        PopupDateTo = SelectedTimeInterval.End.Date;
                        PopupTimeIntervalType = AvailableTimeIntervals.SingleOrDefault(t => t.Id == SelectedTimeInterval.TimeIntervalType.Id) ?? new TimeIntervalType();
                        PopupComment = SelectedTimeInterval.Comment;
                    }
                    else MessageBox.Show("Для редактирования выберите запись из списка", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
            }
        }
        #endregion

        #region ClosePopup
        public void ClosePopup(bool isCancel = false)
        {
            if (isCancel)
            {
                ClearValidationMessages();
                IsPopupVisible = false;
                return;
            }

            switch (currentOperationType)
            {
                case ObjectOperationType.Add:
                    IsPopupVisible = !AddTimeInterval();
                    break;
                case ObjectOperationType.Edit:
                    IsPopupVisible = !EditTimeInterval();
                    break;
            }
            UpdateFilterEntries();
        }
        #endregion

        #region GetAvailableTimeIntervals
        public IList<TimeIntervalType> GetAvailableTimeIntervals()
        {
            var types = employeeService.GetTimeIntervalTypesAsync().Result;
            return types != null ? types : new List<TimeIntervalType>();
        }
        #endregion
    }

    #region class FilterMonth
    public class FilterMonth : BaseModel
    {
        private string name = "";
        public int Id { get; set; }
        public string Name { get => name; set { name = value; OnChanged(); } }

        public FilterMonth(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
    #endregion
}
