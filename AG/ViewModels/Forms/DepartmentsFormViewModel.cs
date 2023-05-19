using AG.Services;
using Microsoft.Extensions.DependencyInjection;
using Services.Database;
using Services.Extensions;
using Services.POCO;
using SQLiteRepository;
using System.Collections.ObjectModel;

namespace AG.ViewModels.Forms
{
    public class DepartmentsFormViewModel
    {
        private readonly DepartmentsService departmentsService;
        private readonly UserAccount? user;
        public ObservableCollection<Department> Departments { get; set; } = new();

        public Department? SelectedDepartment { get; set; }

        public DepartmentsFormViewModel()
        {
            var provider = ServiceLocator.Services.BuildServiceProvider();
            this.departmentsService = new DepartmentsService(provider.GetService<IEstablishmentItemsRepository>());
            user = SessionService.User;

            LoadDepartmetns();
        }

        public void LoadDepartmetns()
        {
            var departments = departmentsService.GetAvailableDepartments(user, true);

            if (departments.StatusCode == DatabaseResponse<Department>.ResponseCode.Success)
            {
                Departments.Clear();
                Departments.AddRange(departments.Results);
            }
        }
    }
}
