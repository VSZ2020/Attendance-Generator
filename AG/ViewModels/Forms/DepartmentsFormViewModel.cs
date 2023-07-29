using AG.Services;
using Microsoft.Extensions.DependencyInjection;
using Services.Database;
using Services.Domains;
using Services.Extensions;
using System.Collections.ObjectModel;

namespace AG.ViewModels.Forms
{
	public class DepartmentsFormViewModel
    {
        private readonly IDepartmentsService departmentsService;
        private readonly UserAccount? user;
        public ObservableCollection<Department> Departments { get; set; } = new();

        public Department? SelectedDepartment { get; set; }

        public DepartmentsFormViewModel()
        {
            this.departmentsService = ServiceLocator.Provider.GetService<IDepartmentsService>()!;
            user = SessionService.User;

            LoadDepartmetns();
        }

        public void LoadDepartmetns()
        {
            var departments = departmentsService.GetDepartments(user, true);

            if (departments.StatusCode == DatabaseResponse<Department>.ResponseCode.Success)
            {
                Departments.Clear();
                Departments.AddRange(departments.Results);
            }
        }
    }
}
