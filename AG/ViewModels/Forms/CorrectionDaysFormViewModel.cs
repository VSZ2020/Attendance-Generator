using Core.Calendar;
using Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace AG.ViewModels.Forms
{
	public class CorrectionDaysFormViewModel: ViewModelCore
	{
		#region ctor
		public CorrectionDaysFormViewModel()
		{
		}
		#endregion

		#region fields
		private ObservableCollection<DayToCorrect> daysList = new ObservableCollection<DayToCorrect>();
		private Dictionary<DayType, string> popupDayTypes = GetAvailableDayTypes();
		private DateTime popupDate;
		private DayType popupSelectedDayType;
		private DayToCorrect? selectedDay;
		private bool isPopupShowed;
		#endregion

		#region Properties
		public ObservableCollection<DayToCorrect> DaysList { get => daysList; set { daysList = value; OnChanged(); } }
		public Dictionary<DayType, string> DayTypes { get => popupDayTypes; set { popupDayTypes = value; OnChanged(); } }
		public DateTime SelectedPopupDateTime { get=> popupDate; set {  popupDate = value; OnChanged(); } }
		public DayType SelectedPopupDayType { get => popupSelectedDayType; set {  popupSelectedDayType = value; OnChanged();} }
		public DayToCorrect? SelectedDayToCorrect { get => selectedDay; set {  selectedDay = value; OnChanged(); } }
		public bool IsPopup { get => isPopupShowed; set { isPopupShowed = value; OnChanged(); } }
		public ObjectOperationType CurrentObjectOperationType { get; set; }
		public bool CanRemoveDay { get => DaysList.Count > 0; set { OnChanged();} }
		#endregion

		#region GetAvailableDayTypes()
		public static Dictionary<DayType, string> GetAvailableDayTypes()
		{
			return Enum.GetValues<DayType>().ToDictionary(k => k, DayToCorrect.DayTypeNameTranslator);
		}
		#endregion

		#region ApplyDayAdd()
		public void ApplyDayAdd()
		{
			var newDayToCorrect = new DayToCorrect(SelectedPopupDateTime, SelectedPopupDayType);
			if (DaysList
				.Where(d => d.Date.Year == newDayToCorrect.Date.Year && d.Date.Month == newDayToCorrect.Date.Month && d.Date.Day == newDayToCorrect.Date.Day)
				.Count() == 0)
			{
				DaysList.Add(newDayToCorrect);
				OnChanged(nameof(CanRemoveDay));
			}
			else
				MessageBox.Show("Дата уже есть в списке", "", MessageBoxButton.OK, MessageBoxImage.Information);
		}
		#endregion

		#region RemoveDay()
		public void RemoveDay()
		{
			if (SelectedDayToCorrect != null)
			{
				var dayToRemove = DaysList.SingleOrDefault(d => d.Date == SelectedDayToCorrect.Date && d.DayType == SelectedDayToCorrect.DayType);
				if (dayToRemove != null)
				{
					DaysList.Remove(dayToRemove);
					OnChanged(nameof(CanRemoveDay));
				}
			}
			else
				MessageBox.Show("Сначала выберите из списка день, который нужно удалить", "", MessageBoxButton.OK, MessageBoxImage.Warning);
		}
		#endregion

		#region ApplyDayEdit()
		public void ApplyDayEdit()
		{
			if (SelectedDayToCorrect != null)
			{
				SelectedDayToCorrect.Date = SelectedPopupDateTime;
				SelectedDayToCorrect.DayType = SelectedPopupDayType;
			}
			else
				MessageBox.Show("Не выбран день для редактирования", "", MessageBoxButton.OK, MessageBoxImage.Warning);
		}
		#endregion

		public void ShowPopup(ObjectOperationType operationTag)
		{
			CurrentObjectOperationType = operationTag;
			if (operationTag == ObjectOperationType.Add)
			{
				IsPopup = true;
				SelectedPopupDateTime = DateTime.Now;
				SelectedPopupDayType = DayType.DayOff;
			}
			if (operationTag == ObjectOperationType.Edit)
			{
				if (SelectedDayToCorrect != null)
				{
					IsPopup = true;
					SelectedPopupDateTime = SelectedDayToCorrect.Date;
					SelectedPopupDayType = SelectedDayToCorrect.DayType;
				}
				else
					MessageBox.Show("Не выбран день для редактирования", "", MessageBoxButton.OK, MessageBoxImage.Warning);
			}
			if (operationTag == ObjectOperationType.Remove)
			{
				RemoveDay();
			}
		}

		public void ClosePopup(ObjectOperationType? operationType = null)
		{
			IsPopup = false;
			if (operationType == null)
			{
				if (CurrentObjectOperationType == ObjectOperationType.Add)
					ApplyDayAdd();
				if (CurrentObjectOperationType == ObjectOperationType.Edit)
					ApplyDayEdit();
			}
		}

		public IList<Day> GetCorrectionDays()
		{
			return DaysList.Select(d => new Day(d.Date, d.DayType)).ToList();
		}

		public void LoadFromFile()
		{
			//TODO: Implement loading
			throw new NotImplementedException();
		}
	}

	public class DayToCorrect: BaseModel
	{
		#region ctor
		public DayToCorrect(DateTime date, DayType type)
		{
			this.Date = date;
			this.DayType = type;
		}

		public DayToCorrect(Day day): this(day.Date, day.Type) { }
		#endregion
		#region fields
		private DateTime date;
		private DayType dayType;
		//private string dayTypeName;
		#endregion

		#region Properties
		public DateTime Date { get => date; set { date = value; OnChanged(); } }
		public DayType DayType { get => dayType; set { dayType = value; OnChanged(); } }
		public string DayTypeName { get => DayTypeNameTranslator(DayType); set { OnChanged(); } }
		#endregion

		public override string ToString()
		{
			return Date.ToShortDateString();
		}

		#region DayTypeNameTranslator
		public static string DayTypeNameTranslator(DayType dayType)
		{
			return dayType switch
			{
				DayType.DayOff => "Выходной",
				DayType.PreHoliday => "Предпраздничный",
				DayType.Holiday => "Праздничный",
				DayType.Working => "Рабочий",
				_ => throw new NotImplementedException()
			};
		}
		#endregion
	}
}
