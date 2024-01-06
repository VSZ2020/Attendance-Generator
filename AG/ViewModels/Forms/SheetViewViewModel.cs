using AG.Windows;
using Core.Calendar;
using Core.ViewModel;
using Services.Database;
using Services.Domains;
using Services.Domains.ReportCard;
using Services.Extensions;
using Services.Infrastructure;
using Services.Infrastructure.Configuration;
using Services.Infrastructure.Configuration.Configs;
using Services.ReportCard;
using Services.Session;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace AG.ViewModels.Forms
{
	public class SheetViewViewModel : ViewModelCore
	{
		#region ctor
		public SheetViewViewModel()
		{
			this.employeeService = ServicesLocator.GetService<IEmployeeService>()!;
			this.reportCardService = ServicesLocator.GetService<IReportCardService>()!;
			this.departmentsService = ServicesLocator.GetService<IDepartmentsService>()!;

			//Получаем конфигурацию
			this.weekConfig = ConfigManager.Instance.Get<WorkingWeekConfig>();
			this.reportViewerConfig = ConfigManager.Instance.Get<ReportViewerConfig>();
			this.calendarConfig = ConfigManager.Instance.Get<CalendarConfig>();

			holidays = calendarConfig.GetStateHolidays();
			currentEstablishmentId = SessionService.CurrentEstablishemntId;
		} 
		#endregion

		#region fields
		private readonly WorkingWeekConfig weekConfig;
		private readonly CalendarConfig calendarConfig;
		private readonly Guid? currentEstablishmentId;

		private readonly IEmployeeService employeeService;
		private readonly IReportCardService reportCardService;
		private readonly IDepartmentsService departmentsService;
		private readonly ReportViewerConfig reportViewerConfig;

		private IList<Day>? correctionDays;
		private IList<DateTime>? holidays;

		private ObservableCollection<Department> departmentsList = new();
		private Guid selectedDepartmentId = Guid.Empty;
		private DataGrid grid;

		private DateTime selectedDate = DateTime.Now;
		#endregion

		#region Properties
		public ObservableCollection<Department> DepartmentsList { get => departmentsList; set { departmentsList = value; OnChanged(); } }
		public Guid SelectedDepartmentId { get => selectedDepartmentId; set { selectedDepartmentId = value; OnChanged(); } }

		public DateTime SelectedDate { get => selectedDate; set {  selectedDate = value; OnChanged(); } }
		#endregion

		#region InitializeViewAsync
		public async Task InitializeViewAsync(DataGrid grid)
		{
			this.grid = grid;

			await LoadCorrectionDaysAsync();
			await LoadDepartmentsAsync();
			await UpdateSheet();
		} 
		#endregion

		#region UpdateSheet
		public async Task UpdateSheet()
		{
			base.ShowWaitMessage("Идет создание отчета", "Пожалуйста, подождите");

			await GenerateSheetAsync();

			base.ClearWaitMessage();
		}
		#endregion

		#region GenerateSheetAsync
		public async Task GenerateSheetAsync()
		{
			var rows = await Task.Run(async () =>
			{
				var employees = await employeeService!.GetEmployeesAsync(selectedDepartmentId);
				var sheetRows = reportCardService.MakeViewSheetRows(employees, selectedDate, holidays, correctionDays, weekConfig, reportViewerConfig);
				return sheetRows;
			});

			//На основе полученных списков заполняем DataGrid
			GenerateDataGrid(rows);
		}
		#endregion

		#region LoadDepartmentsAsync
		private async Task LoadDepartmentsAsync()
		{
			base.ShowWaitMessage("Загрузка подразделений", "Подождите");

			var departments = await Task.Run(() => departmentsService.GetDepartmentsAsync(Guid.Empty));

			base.ClearWaitMessage();

			DepartmentsList.Clear();
			DepartmentsList.AddRange(departments);
		}
		#endregion

		#region LoadCorrectionDaysAsync
		public async Task LoadCorrectionDaysAsync()
		{
			//Список дней для ручной корректировки
			var daysFromDatabase = currentEstablishmentId != null ? await departmentsService.GetCorrectionDaysAsync(currentEstablishmentId.Value) : new List<CorrectionDay>();
			correctionDays = daysFromDatabase.Select(d => new Day(d.Date, d.Type)).ToList();
		} 
		#endregion

		#region EditCorrectionDays
		public void EditCorrectionDays()
		{
			var wnd = new WndCorrectionDayEdit();
			wnd.ShowDialog();

			correctionDays = calendarConfig.DaysToCorrect;
		} 
		#endregion

		#region DataGrid
		public void GenerateDataGrid(IList<SheetRowModel> rows)
		{
			grid.Columns.Clear();
			var colRate = new DataGridTextColumn();
			var colName = new DataGridTextColumn();
			var colFunction = new DataGridTextColumn();

			var colHalfMonthEmployeeHours = new DataGridTextColumn();
			var colTotalEmployeeHours = new DataGridTextColumn();

			//Названия колонок
			colName.Header = "Сотрудник";
			colRate.Header = "Доля ставки";
			colFunction.Header = "Должность";

			colHalfMonthEmployeeHours.Header = "Рабочих часов";
			colTotalEmployeeHours.Header = "Рабочих часов";

			//Привязки к колонкам
			colRate.Binding = new Binding("Rate");
			colName.Binding = new Binding("FullName");
			colFunction.Binding = new Binding("Function");

			colHalfMonthEmployeeHours.Binding = new Binding("HalfMonthHours");
			colTotalEmployeeHours.Binding = new Binding("TotalMonthHours");
			colHalfMonthEmployeeHours.Foreground = Brushes.Blue;
			colTotalEmployeeHours.Foreground = Brushes.Blue;
			string hoursFormat = "{0:F" + reportViewerConfig.DigitsCountInTotalHours.ToString() + "}";
			colHalfMonthEmployeeHours.Binding.StringFormat = hoursFormat;
			colTotalEmployeeHours.Binding.StringFormat = hoursFormat;

			grid.Columns.Add(colName);
			grid.Columns.Add(colFunction);
			grid.Columns.Add(colRate);

			int daysCount = DateTime.DaysInMonth(selectedDate.Year, selectedDate.Month);
			AddColumns(daysCount);

			//Вставка промежуточных колонок с суммарными часами
			grid.Columns.Insert(grid.Columns.Count - daysCount + weekConfig.MonthHalfDate, colHalfMonthEmployeeHours);
			grid.Columns.Add(colTotalEmployeeHours);

			grid.ItemsSource = rows;
		}

		//Источник: https://stackoverflow.com/questions/18452134/filling-a-datagrid-with-dynamic-columns
		private void AddColumns(int daysCount)
		{
			for (int i = 0; i < daysCount; i++)
			{
				string colName = $"{i + 1}";
				grid.Columns.Add(new DataGridTextColumn()
				{
					Header = colName,
					Binding = new Binding($"Custom[{colName}]"),
					Width = 30
				});
			}
		}
		#endregion
	}
}
