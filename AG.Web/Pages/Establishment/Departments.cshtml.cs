using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Database;
using Services.Domains;
using Services.Session;
using System.Security.Claims;

namespace AG.Web.Pages.Establishment
{
    [Area("Establishment")]
    public class DepartmentsModel : PageModel
    {
        public DepartmentsModel([FromServices] IDepartmentsService depService, [FromServices] IHttpContextAccessor accessor)
        {
            this.depService = depService;
            this.ctx_accessor = accessor;   
        }

        private Guid userEstablishmentId = Guid.Empty;
        private readonly IDepartmentsService depService;
        private readonly IHttpContextAccessor ctx_accessor;

        public string EstablishmentName { get; set; } = string.Empty;

        public readonly List<Department> DepartmentsList = new List<Department>();

        public void OnGet()
        {
            LoadEstablishmentData();
            LoadDepartments();
        }

        public IActionResult RemoveDepartment(Guid departmentId)
        {
            return new OkResult();
        }

        private async void LoadDepartments()
        {
            var departments = await depService.GetDepartmentsAsync(userEstablishmentId, FetchAim.Table);
            if (departments != null)
            {
                DepartmentsList.Clear();
                DepartmentsList.AddRange(departments);
            }
        }

        private async void LoadEstablishmentData()
        {
            var establishmentId_str = ctx_accessor.HttpContext?.User.FindFirstValue(CustomClaims.ClaimEstablishmentId);
            var establishmentId = Guid.Empty;
            if (Guid.TryParse(establishmentId_str, out establishmentId))
            {
                var establishment = await depService.GetEstablishmentByIdAsync(establishmentId);
                if (establishment != null)
                {
                    this.EstablishmentName = establishment.Name;
                }
            }
        }
    }
}
