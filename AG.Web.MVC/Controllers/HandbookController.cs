using AG.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AG.Web.MVC.Controllers
{
    public class HandbookController : Controller
    {
        public HandbookController(DataContext ctx)
        {
            _context = ctx;    
        }
        readonly DataContext _context;
        

        [HttpGet]
        public IActionResult Notations()
        {
            var timeIntervals = _context.TimeIntervals.AsNoTracking().OrderBy(e => e.CODE).ToList();
            return View(timeIntervals);
        }

        [HttpGet]
        public IActionResult Functions()
        {
            var items = _context.Functions.AsNoTracking().OrderBy(e => e.Name).OrderBy(e => e.Category).ToList();
            return View(items);
        }
    }
}
