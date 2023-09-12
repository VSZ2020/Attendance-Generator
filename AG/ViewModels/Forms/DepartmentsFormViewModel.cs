using Core.ViewModel;
using Services.Database;
using Services.Domains;
using Services.Extensions;
using System.Collections.ObjectModel;

namespace AG.ViewModels.Forms
{
	public class DepartmentsFormViewModel: ViewModelCore
    {
		#region ctor
		public DepartmentsFormViewModel()
		{
			this.departmentsService = ServiceLocator.GetService<IDepartmentsService>()!;
			LoadDepartmetns();
		}
		#endregion

		#region fields
		private readonly IDepartmentsService departmentsService;
        private Department? selectedDepartment;
		#endregion

		#region Properties
		public ObservableCollection<Department> Departments { get; set; } = new();

        public Department? SelectedDepartment { get => selectedDepartment; set { selectedDepartment = value; OnChanged(); } }
		#endregion
		
		public void LoadDepartmetns()
        {
            var departments = departmentsService.GetDepartments(true);

            if (departments.StatusCode == DatabaseResponse<Department>.ResponseCode.Success)
            {
                Departments.Clear();
                Departments.AddRange(departments.Results!);
            }
        }
    }
}
