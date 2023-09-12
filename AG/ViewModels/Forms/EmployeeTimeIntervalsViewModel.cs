using AG.Windows;
using Core.ViewModel;
using Services.Database;
using Services.Domains;
using Services.Extensions;
using Services.Translators;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace AG.ViewModels.Forms
{
    public class EmployeeTimeIntervalsViewModel: ViewModelCore
    {
		#region ctor
		public EmployeeTimeIntervalsViewModel()
		{
			this.employeeService = ServiceLocator.GetService<IEmployeeService>()!;
			this.AvailableTimeIntervals = GetAvailableTimeIntervals();
		}
		#endregion

		#region	fields
		private int employeeId;
		private string employeeName;
		private readonly IEmployeeService employeeService;
		
		private TimeInterval? selectedTimeInterval;
		private ObservableCollection<TimeInterval> timeIntervals = new ObservableCollection<TimeInterval>();
		
		private string selectedFilterYearId;
		private int selectedFilterMonthId = 1;

		private bool isPopupVisible = false;
		private DateTime popupDateFrom;
		private DateTime popupDateTo;
		private string popupComment;
		private TimeIntervalType popupTimeIntervalType;
		private IList<TimeIntervalType> availableTimeIntervals;

		private ObjectOperationType currentOperationType;
		#endregion

		#region	Properties
		public string EmployeeName { get => employeeName; set { employeeName = value; OnChanged(); } }

		public ObservableCollection<TimeInterval> TimeIntervals { get => timeIntervals; set { timeIntervals = value; OnChanged(); } }
		public TimeInterval? SelectedTimeInterval { get => selectedTimeInterval; set { selectedTimeInterval = value; OnChanged(); } }

		public ObservableCollection<string> FilterYears { get; set; } = new ();
		public ObservableCollection<FilterMonth> FilterMonths { get; set; } = new();

		public string SelectedFilterYear { get =>  selectedFilterYearId; set { selectedFilterYearId = value; OnChanged(); } }
		public int SelectedFilterMonth { get => selectedFilterMonthId; set { selectedFilterMonthId = value; OnChanged(); } }

		public bool IsPopupVisible { get => isPopupVisible; set { isPopupVisible = value; OnChanged(); } }
		public DateTime PopupDateFrom { get => popupDateFrom; set { popupDateFrom = value; OnChanged(); } }
		public DateTime PopupDateTo { get => popupDateTo; set { popupDateTo = value; OnChanged(); } }
		public string PopupComment { get => popupComment; set { popupComment = value; OnChanged(); } }
		public TimeIntervalType PopupTimeIntervalType { get => popupTimeIntervalType; set {  popupTimeIntervalType = value; OnChanged();} }
		public IList<TimeIntervalType> AvailableTimeIntervals { get => availableTimeIntervals; set { availableTimeIntervals = value; OnChanged(); } }
		#endregion

		public void LoadInterface(Employee employee)
		{
			this.employeeId = employee.Id;
			EmployeeName = employee.FullName;
			LoadTimeIntervals();
			UpdateFilterEntries();
		}

		#region UpdateFilterEntries()
		public void UpdateFilterEntries()
		{
			var years = TimeIntervals
				.Select(ti => ti.Begin.Year)
				.Union(TimeIntervals.Select(ti_to => ti_to.End.Year).ToList())
				.Distinct()
				.Select(y => y.ToString())
				.ToList();
			var months = TimeIntervals
				.Select(ti => ti.Begin.Month)
				.Union(TimeIntervals.Select(ti_to => ti_to.End.Month).ToList())
				.Distinct()
				.Select(m => new FilterMonth(m, CalendarTranslator.TranslateMonthId(m)))
				.ToList();
			FilterYears.Clear();
			FilterYears.Add("Все");
			FilterYears.AddRange(years);

			FilterMonths.Clear();
			FilterMonths.Add(new FilterMonth(0,"Все"));
			FilterMonths.AddRange(months);

			SelectedFilterYear = "Все";
			SelectedFilterMonth = 0;
		}
		#endregion

		public void FilterTimeIntervals()
		{

		}

		private async void LoadTimeIntervals()
		{
			//var waitWnd = new WndWait("Загрузка временных интервалов пользователя");
			//waitWnd.Show();
			ShowWaitMessage("Загрузка временных интервалов пользователя", "Подождите");

			var timeIntervals = await Task.Run(() => employeeService.GetEmployeePeriods(employeeId).Results!);
			TimeIntervals.AddRange(timeIntervals);

			ClearWaitMessage();
			//waitWnd.Close();
		}

		private TimeInterval MakeTimeInterval()
		{
			return new TimeInterval()
			{
				Id = default,
				Begin = PopupDateFrom,
				End = PopupDateTo,
				TimeIntervalType = PopupTimeIntervalType,
				Comment = PopupComment
			};
		}
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

					return true;
				}
			}
			return false;
		}

		public void RemoveTimeInterval()
		{
			if (SelectedTimeInterval != null)
			{
				TimeIntervals.Remove(SelectedTimeInterval);
			}
			else
			{
				MessageBox.Show("Для того, чтобы удалить запись, необходимо сначала ее выбрать", "", MessageBoxButton.OK, MessageBoxImage.Warning);
			}
		}

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

		private bool ValidatePopupInputs(TimeInterval ti)
		{
			ClearValidationMessages();
			if (popupDateFrom > popupDateTo)
				AddValidationMessage("Начальная дата не может быть больше конечной", "Ошибка в датах");
			
			CheckTimeIntervalsCollision(ti);

			return ValidationMessages.Count == 0;
		}
		
		public void ApplyPendingChanges()
		{
			try
			{
				//employeeService.AddEmployeePeriod()
			}
			catch (Exception ex)
			{

			}

		}

		public void ShowPopup(ObjectOperationType operationType)
		{
			currentOperationType = operationType;
			switch (operationType)
			{
				case ObjectOperationType.Add:
					IsPopupVisible = true;
					PopupDateFrom = DateTime.Now;
					PopupDateTo = DateTime.Now.AddDays(7);
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

		#region GetAvailableTimeIntervals()
		public IList<TimeIntervalType> GetAvailableTimeIntervals()
		{
			var types = employeeService.GetTimeIntervalTypes().Results;
			return types != null ? types : new List<TimeIntervalType>();
		}
		#endregion
	}

	#region class FilterMonth
	public class FilterMonth: BaseModel
	{
		private string name = "";
		public int Id { get; set; }
		public string Name { get => name; set { name = value; OnChanged(); } }

		public FilterMonth(int id, string name)
		{
			this.Id = id;
			this.Name = name;
		}
	}
	#endregion
}
