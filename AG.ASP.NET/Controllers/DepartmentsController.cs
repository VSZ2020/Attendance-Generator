using AG.ASP.NET.ViewModels.Establishment;
using Microsoft.AspNetCore.Mvc;
using Services.Database;

namespace AG.ASP.NET.Controllers
{
    public class DepartmentsController : Controller
    {
        [HttpGet]
        [ActionName("Index")]
        public IActionResult Index([FromServices] IDepartmentsService depService, [FromServices] IHttpContextAccessor accessor)
        {
            return View("Departments",new DepartmentsModel(depService, accessor));
        }

        [HttpGet]
        [ActionName("Edit")]
        public IActionResult EditDepartment(Guid? departmentId, [FromServices] IDepartmentsService depService, [FromServices] IEmployeeService emplService, [FromServices] IHttpContextAccessor accessor)
        {
            if (departmentId == null)
                return NotFound();

            return View("EditDepartment",new DepartmentModel(departmentId.Value, depService, emplService));
        }

        [HttpPost]
        public IActionResult EditDepartment()
        {
            return Ok("Department was submitted");
        }

        [HttpPost]
        [ActionName("Save")]
        public async void SaveDepartment(Guid departmentId)
        {
            var form = HttpContext.Request.Form;
            string name = form["department_name"];
            string head_of_lab = form["head_of_lab"];
            await HttpContext.Response.WriteAsync($"New name: {name}, header: {head_of_lab}");

        }
    }
}
