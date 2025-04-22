using AG.Data.Entities;

namespace AG.Web.MVC.Areas.HR.Models.Department
{
    public record class DepartmentListViewModel
    {
        public IEnumerable<DepartmentViewModel> Departments { get; set; }
    }
}
