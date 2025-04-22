using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AG.Web.MVC.Areas.HR.Models.EmployeeFunction
{
    public class EditEmployeeFunctionVM: CreateEmployeeFunctionVM
    {
        public Guid Id { get; set; }
    }
}
