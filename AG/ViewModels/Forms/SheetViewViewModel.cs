using Core.Calendar;
using Core.ViewModel;
using Services.Database;
using Services.Infrastructure.Configuration.Configs;
using Services.Infrastructure.Configuration;
using Services.ReportCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using AG.Windows;
using Services.Domains;
using Services.Domains.ReportCard;
using System.Windows.Data;
using System.Windows.Media;
using System.Collections.ObjectModel;

namespace AG.ViewModels.Forms
{
	public class SheetViewViewModel : ViewModelCore
	{
		public SheetViewViewModel()
		{
			this.employeeService = ServiceLocator.GetService<IEmployeeService>()!;
			this.reportCardService = ServiceLocator.GetService<IReportCardService>()!;
			this.departmentsService = ServiceLocator.GetService<IDepartmentsService>()!;

			//Получаем конфигурацию
			this.weekConfig = ConfigManager.Instance.Get<WorkingWeekConfig>();
			this.reportViewerConfig = ConfigManager.Instance.Get<ReportViewerConfig>();
			var calendarConfig = ConfigManager.Instance.Get<CalendarConfig>();

			holidays = calendarConfig.GetStateHolidays();

			//Список дней для ручной корректировки
			correctionDays = new List<Day>();
		}

		#region fields

		private DateTime currentDate = DateTime.Now;
		private readonly WorkingWeekConfig weekConfig;
		private readonly IEmployeeService employeeService;
		private readonly IReportCardService reportCardService;
		private readonly IDepartmentsService departmentsService;
		private readonly ReportViewerConfig reportViewerConfig;

		private IList<Day>? correctionDays;
		private IList<DateTime>? holidays;

		private ObservableCollection<Department> departmentsList = new();
		private int selectedDepartmentId = 0;
		private DataGrid grid;

		private DateTime selectedDate = DateTime.Now;
		#endregion

		#region Properties
		public ObservableCollection<Department> DepartmentsList { get => departmentsList; set { departmentsList = value; OnChanged(); } }
		public int SelectedDepartmentId { get => selectedDepartmentId; set { selectedDepartmentId = value; OnChanged(); } }

		public DateTime SelectedDate { get => selectedDate; set {  selectedDate = value; OnChanged(); } }
		#endregion

		public async Task InitializeViewAsync(DataGrid grid)
		{
			this.grid = grid;

			await LoadDepartmentsAsync();
			await UpdateSheet();
		}

		public async Task UpdateSheet()
		{
			ShowWaitMessage("Идет создание таблицы", "Ожидайте");
			await GenerateSheetAsync();
			ClearWaitMessage();
		}

		public async Task GenerateSheetAsync()
		{
			var rows = await Task.Run(async () =>
			{
				var employees = (await employeeService!.GetEmployeesAsync(selectedDepartmentId)).Results;
				var sheetRows = reportCardService.MakeViewSheetRows(employees, currentDate, holidays, correctionDays, weekConfig, reportViewerConfig);
				return sheetRows;
			});

			//На основе полученных списков заполняем DataGrid
			GenerateDataGrid(rows);
		}

		private async Task LoadDepartmentsAsync()
		{
			ShowWaitMessage("Загрузка подразделений", "Подождите");
			var departmentsResponse = await Task.Run(() => departmentsService.GetDepartments());
			ClearWaitMessage();

			DepartmentsList.Clear();
			if (departmentsResponse.StatusCode == DatabaseResponse<Department>.ResponseCode.Success)
			{
				foreach (var department in departmentsResponse.Results!)
					DepartmentsList.Add(department);
			}
		}

		public void EditCorrectionDays()
		{
			var wnd = new WndCorrectionDayEdit();
			wnd.ShowDialog();
			correctionDays = wnd.GetCorrectionDays();
		}


		#region DataGrid
		public void GenerateDataGrid(IList<SheetRowModel> rows)
		{
			grid.Columns.Clear();
			var colId = new DataGridTextColumn();
			var colRate = new DataGridTextColumn();
			var colName = new DataGridTextColumn();
			var colFunction = new DataGridTextColumn();

			var colHalfMonthEmployeeHours = new DataGridTextColumn();
			var colTotalEmployeeHours = new DataGridTextColumn();

			//Названия колонок
			colId.Header = "п/п";
			colRate.Header = "Доля ставки";
			colName.Header = "Сотрудник";
			colFunction.Header = "Должность";

			colHalfMonthEmployeeHours.Header = "Рабочих часов";
			colTotalEmployeeHours.Header = "Рабочих часов";

			//Привязки к колонкам
			colId.Binding = new Binding("Id");
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

			grid.Columns.Add(colId);
			grid.Columns.Add(colRate);
			grid.Columns.Add(colName);
			grid.Columns.Add(colFunction);

			int daysCount = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);
			AddColumns(daysCount);
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
