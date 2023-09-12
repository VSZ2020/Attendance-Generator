using Microsoft.Extensions.DependencyInjection;
using Services.Calendar;
using Services.Database;
using Services.Domains;
using Services.ReportCard;
using SQLiteRepository;
using System;

namespace AG
{
	public class ServiceLocator
    {
        private static IServiceCollection Services = new ServiceCollection();
        private static IServiceProvider Provider { get;  set; }
        static ServiceLocator()
        {
            //Регистрируем сервисы
            Services
                .AddTransient<IEstablishmentItemsRepository, EstablishmentItemsRepository>()
                .AddTransient<IAppItemsRepository, AppItemsRepository>()
                .AddTransient<IUserAccountService, UserAccountService>()
                .AddSingleton<UserAccount>()
                .AddTransient<IEmployeeService, EmployeesService>()
                .AddTransient<IDepartmentsService, DepartmentsService>()
                .AddTransient<ICalendarService, CalendarService>()
                .AddTransient<IReportCardService, ReportCardService>();

			Provider = Services.BuildServiceProvider();
		}
        public static T? GetService<T>() where T : class
        {
            return Provider.GetService<T>();
        }
    }
}
