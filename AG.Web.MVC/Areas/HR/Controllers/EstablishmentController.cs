using AG.Data;
using AG.Data.Defaults;
using AG.Web.MVC.Areas.HR.Models.Establishment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AG.Web.MVC.Areas.HR.Controllers
{
    [Area("HR")]
    public class EstablishmentController : Controller
    {
        public EstablishmentController(DataContext context)
        {
            _context = context;
        }
        readonly DataContext _context;


        public async Task<IActionResult> Index()
        {
            var entity = await _context.Establishments.AsNoTracking().FirstOrDefaultAsync(e => e.Id == DefaultEntities.DEFAULT_ESTABLISHMENT_ID);
            if (entity == null)
                return NotFound();

            return View(new EstablishmentViewModel()
            {
                //Id = entity.Id,
                Title = entity.FullName,
                ShortTitle = entity.ShortName,
                Header = entity.Header,
                HeaderFunction = entity.HeaderFunction,
                Address = entity.Address,
                Email = entity.Email,
                INN = entity.INN,
                OGRN = entity.OGRN,
                RegistrationDate = entity.RegistrationDate,
            });
        }

        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var entity = await _context.Establishments.AsNoTracking().FirstOrDefaultAsync(e => e.Id == DefaultEntities.DEFAULT_ESTABLISHMENT_ID);
            if (entity == null)
                return NotFound();

            var estab = new EstablishmentViewModel()
            {
                Id = entity.Id,
                Title = entity.FullName,
                ShortTitle = entity.ShortName,
                Header = entity.Header,
                HeaderFunction = entity.HeaderFunction,
                Address = entity.Address,
                Email = entity.Email,
                INN = entity.INN,
                OGRN = entity.OGRN,
                RegistrationDate = entity.RegistrationDate,
            };
            return View(estab);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EstablishmentViewModel est)
        {
            if (ModelState.IsValid)
            {
                var entity = await _context.Establishments.FirstOrDefaultAsync(e => e.Id == est.Id);
                if (entity == null)
                    return NotFound();

                entity.FullName = est.Title;
                entity.ShortName = est.ShortTitle;
                entity.INN = est.INN;
                entity.OGRN = est.OGRN;
                entity.Header = est.Header;
                entity.HeaderFunction = est.HeaderFunction;
                entity.RegistrationDate = est.RegistrationDate;
                entity.Address = est.Address;
                entity.Email = est.Email;
                entity.Phones = est.Phones;

                _context.Establishments.Update(entity);

                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException uEx)
                {
                    ModelState.AddModelError("", "Не удалось сохранить изменения. Попробуйте позже");
                }
            }
            return View();
        } 
        #endregion
    }
}
