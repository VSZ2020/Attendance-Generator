using AG.ViewModels.Forms;
using Core.Calendar;
using Microsoft.Extensions.DependencyInjection;
using Services.Calendar;
using Services.Database;
using Services.Domains;
using Services.Domains.ReportCard;
using Services.Infrastructure.Configuration;
using Services.Infrastructure.Configuration.Configs;
using Services.ReportCard;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AG.Windows
{
	/// <summary>
	/// Логика взаимодействия для WndSheetViewer.xaml
	/// </summary>
	public partial class WndSheetViewer : Window
	{
		#region ctor
		public WndSheetViewer(ICalendarService calendarService, IEmployeeService employeeService, IReportCardService reportCardService, WorkingWeekConfig workingWeekConfig, ReportViewerConfig reportViewerConfig)
		{
			InitializeComponent();

			this.calendarService = calendarService;
			this.employeeService = employeeService;
			this.reportCardService = reportCardService;
			//Получаем конфигурацию
			this.weekConfig = workingWeekConfig;
			this.reportViewerConfig = reportViewerConfig;

			datePicker.SelectedDate = currentDate;
			holidays = CalendarService.GetDefaultHolidays();
			correctionDays = new List<Day>()
			{
				new Day(currentDate.Year, 5, 8, DayType.DayOff),
			};

			//Событие при загрузке формы
			this.Loaded += WndSheetViewer_Loaded;
		}
		#endregion ctor

		#region fields
		private DateTime currentDate = DateTime.Now;
		private readonly WorkingWeekConfig weekConfig;
		private readonly ICalendarService calendarService;
		private readonly IEmployeeService employeeService;
		private readonly IReportCardService reportCardService;
		private readonly ReportViewerConfig reportViewerConfig;

		private IList<Day>? correctionDays;
		private IList<DateTime>? holidays;
		#endregion

		private async void WndSheetViewer_Loaded(object sender, RoutedEventArgs e)
		{
			await UpdateSheet();
		}

		public async Task UpdateSheet()
		{
			var generateTask = GenerateSheetAsync();
			var frmWait = new WndWait("Создание таблицы. Ожидайте!");
			frmWait.Show();

			await generateTask;
			frmWait.Close();
		}

		public async Task GenerateSheetAsync()
		{
			var rows = await Task.Run(async () =>
			{
				var employees = (await employeeService!.GetEmployeesAsync()).Results;
				var sheetRows = reportCardService.MakeViewSheetRows(employees, currentDate, holidays, correctionDays, weekConfig, reportViewerConfig);
				return sheetRows;
			});
			

			//На основе полученных списков заполняем DataGrid
			GenerateDataGrid(rows);
		}

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
			for(int i = 0; i < daysCount; i++)
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

		private async void btnApplyDate_Click(object sender, RoutedEventArgs e)
		{
			if (datePicker.SelectedDate.HasValue)
				currentDate = datePicker.SelectedDate.Value;
			await UpdateSheet();
		}
	}
}
