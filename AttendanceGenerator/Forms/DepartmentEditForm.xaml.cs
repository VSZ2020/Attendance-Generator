using AttendanceGenerator.Model.Department;
using System;
using System.Collections.Generic;
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

namespace AttendanceGenerator.Forms
{
    /// <summary>
    /// Логика взаимодействия для DepartmentEditForm.xaml
    /// </summary>
    public partial class DepartmentEditForm : Window
    {
        private ItemOperationType _operation;
        private Department _department;
        private int _establishmentId;
        public Department Department { get => _department; }
        public DepartmentEditForm(ItemOperationType operation, int EstablishmentId)
        {
            InitializeComponent();
            _operation = operation;
            _establishmentId = EstablishmentId;
        }

        public DepartmentEditForm(Department department) :this(ItemOperationType.Edit, department.EstablishmentId)
        {
            _department = department;
        }
    }
}
