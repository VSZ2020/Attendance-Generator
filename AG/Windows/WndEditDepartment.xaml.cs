using AG.WPF.Domains;
using AG.WPF.ViewModels.Forms;
using System;
using System.Windows;

namespace AG.Windows
{
    /// <summary>
    /// Логика взаимодействия для WndEditDepartment.xaml
    /// </summary>
    public partial class WndEditDepartment : Window
    {
        #region ctor
        public WndEditDepartment(Guid establishmentId, Department? department = null)
        {
            InitializeComponent();

            vm = new EditDepartmentViewModel(establishmentId, department);
            DataContext = vm;
        }
        #endregion

        private EditDepartmentViewModel vm;

		#region btnAccept_Click
		private async void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (await vm.AcceptChanges())
                this.Close();
        } 
        #endregion
    }
}
