using AG.ASP.NET.ViewModels.Establishment;
using Microsoft.AspNetCore.Mvc;
using Services.Database;

namespace AG.ASP.NET.Controllers
{
    public class EstablishmentController : Controller
    {
        private readonly ILogger<EstablishmentController> _logger;

        public EstablishmentController(ILogger<EstablishmentController> logger)
        {
            _logger = logger;
        }




        public IActionResult Privacy()
        {
            return View();
        }
    }
}
