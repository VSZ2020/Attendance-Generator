using Microsoft.Extensions.DependencyInjection;
using Services.Database;
using Services.Generators;
using Services.POCO;
using SQLiteRepository;
using System;

namespace AG
{
    public class ServiceLocator
    {
        public static IServiceCollection Services = new ServiceCollection();
        public static IServiceProvider Provider { get; private set; }
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
                .AddTransient<IReportGeneratorService, ReportGeneratorService>();

			Provider = Services.BuildServiceProvider();
		}
    }
}
