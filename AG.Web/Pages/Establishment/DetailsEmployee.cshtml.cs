using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Database;
using Services.Domains;

namespace AG.Web.Pages.Establishment
{
    public class DetailsEmployeeModel : PageModel
    {
        public DetailsEmployeeModel([FromServices]IEmployeeService eService) 
        {
            this.eService = eService;
        }

        private readonly IEmployeeService eService;

        public Employee CurrentEmployee { get; private set; }

        public async Task<IActionResult> OnGetAsync(Guid employeeId)
        {
            if (employeeId == Guid.Empty)
                return NotFound();

            var employee = await eService.GetEmployeeByIdAsync(employeeId, FetchAim.Card);
            if (employee != null)
                this.CurrentEmployee = employee;

            return Page();
        }
    }
}
