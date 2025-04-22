using AG.Data;
using Microsoft.AspNetCore.Mvc;

namespace AG.Web.MVC.Areas.HR.Controllers
{
    [Area("HR")]
    public class TimeIntervalController : Controller
    {
        public TimeIntervalController(DataContext context)
        {
            _context = context;
        }
        readonly DataContext _context;

        //[Authorize(Policy = "AdminModHR")]
        public async Task<IActionResult> Index(Guid EmployeeId, int? filterMonth = null, int? filterYear = null)
        {
            return View();
        }



    }
}
