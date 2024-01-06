using Services.Database;
using Services.Domains;
using Services.Session;
using System.Security.Claims;

namespace AG.ASP.NET.ViewModels.Establishment
{
    public class DepartmentsModel
    {
        public DepartmentsModel(IDepartmentsService depService, IHttpContextAccessor accessor)
        {
            this.depService = depService;
            this.ctx_accessor = accessor;

            LoadEstablishmentData();
            LoadDepartments();
        }

        private Guid userEstablishmentId = Guid.Empty;
        private readonly IDepartmentsService depService;
        private readonly IHttpContextAccessor ctx_accessor;

        public string EstablishmentName { get; set; } = string.Empty;

        public readonly List<Department> DepartmentsList = new List<Department>();

        public async Task LoadDepartments()
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
