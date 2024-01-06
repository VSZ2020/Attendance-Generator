using Microsoft.Extensions.DependencyInjection;
using Services.Calendar;
using Services.Database;
using Services.Domains;
using Services.ReportCard;
using SQLiteRepository;

namespace Services.Infrastructure
{
	public class ServicesLocator
	{
		#region static ctor
		static ServicesLocator()
		{
			BuildServices();
		}
		#endregion


		private static ServiceCollection _services;
		private static ServiceProvider _provider;

		/// <summary>
		/// Конфигурирует сервисы приложения
		/// </summary>
		public static void BuildServices()
		{
			_services = new ServiceCollection();
			_services
				.AddTransient<IDepartmentsService, DefaultDepartmentsService>()
				.AddTransient<IEmployeeService, DefaultEmployeeService>()
				.AddTransient<IUserAccountService, DefaultUserAccountService>()
				.AddTransient<ICalendarService, CalendarService>()
				.AddTransient<IReportCardService, ReportCardService>()
				.AddSingleton<UserAccount>();
			_provider = _services.BuildServiceProvider();
		}

		/// <summary>
		/// Возвращает сервис по его интерфейсу
		/// </summary>
		/// <typeparam name="TService">Интерфейс сервиса (абстракция)</typeparam>
		/// <returns></returns>
		public static TService GetService<TService>()
		{
			var service = _provider!.GetService<TService>();
			return service != null ? service : throw new NullReferenceException($"Не найден сервис {nameof(TService)}");
		}
	}
}
