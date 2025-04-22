using AG.Data;
using AG.Data.Entities;
using AG.Web.MVC.Models.CorrectionDay;
using AG.Web.MVC.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AG.Web.MVC.Controllers
{
    public class CorrectionDayController: Controller
    {
        #region ctor
        public CorrectionDayController(DataContext ctx)
        {
            _context = ctx;
        } 
        #endregion

        readonly DataContext _context;

        #region Index
        [HttpGet]
        public async Task<IActionResult> Index(int? FilterYear = null, int? FilterMonth = null)
        {
            var items = _context.CorrectionDays.AsNoTracking();
            if (FilterYear != null)
            {
                items = items.Where(e => e.Year == FilterYear.Value);
            }
            if (FilterMonth != null)
            {
                items = items.Where(e => e.Month == FilterMonth);
            }
                
            var days = await items
                .Select(e => new CorrectionDayVM()
                {
                    Id = e.Id,
                    Day = e.Day,
                    Month = e.Month,
                    Year = e.Year,
                    Title = e.Title,
                    Hours = e.Hours,
                    Date = $"{e.Day} {CommonLists.GetCaseChangedMonth(e.Month)} {e.Year}".TrimEnd(),
                    DayType = CommonLists.NamesOfCorrectionDays[e.Type],
                }).OrderBy(e => e.Day).OrderBy(e => e.Month).ToListAsync();

            return View(new IndexCorrectionDayVM()
            {
                FilterMonth = FilterMonth,
                FilterYear = FilterYear,
                CorrectionDays = days,
                AvailableMonths = PopulateMonths(),
            });
        } 
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateCorrectionDayVM()
            {
                Day = DateTime.Now.Day,
                Month = DateTime.Now.Month,
                Type = Core.Enums.CorrectionDayType.DayOff,
                Hours = 0,

                AvailableMonths = PopulateMonths(),
                AvailableDayTypes = PopulateCorrectionDayTypes(),
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCorrectionDayVM model)
        {
            var simularEntity = await _context.CorrectionDays.AsNoTracking().FirstOrDefaultAsync(e => e.Year == model.Year && e.Month == model.Month && e.Day == e.Day);
            if (simularEntity != null)
                ModelState.AddModelError("", $"Указанная дата уже есть в списке под названием '{simularEntity.Title}'");

            if (ModelState.IsValid)
            {
                var entity = new CorrectionDayEntity()
                {
                    Day = model.Day,
                    Month = model.Month,
                    Year = model.Year,
                    Title = model.Title,
                    Hours = model.Hours,
                    Type = model.Type,
                };
                _context.CorrectionDays.Add(entity);

                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "CorrectionDay");
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Не удалось выполнить сохранение изменений. Попробуйте позже или обратитесь к администратору");
                }
            }

            model.AvailableMonths = PopulateMonths();
            model.AvailableDayTypes = PopulateCorrectionDayTypes();
            return View(model);
        } 
        #endregion

        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var entity = await _context.CorrectionDays
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
            if (entity == null)
                return RedirectToAction("Index", "CorrectionDay");

            return View(new EditCorrectionDayVM()
            {
                Id = id,
                Day = entity.Day,
                Month = entity.Month,
                Year = entity.Year,
                Title = entity.Title,
                Type = entity.Type,
                Hours= entity.Hours,

                AvailableMonths = PopulateMonths(),
                AvailableDayTypes = PopulateCorrectionDayTypes(),
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCorrectionDayVM model)
        {
            var entity = await _context.CorrectionDays.AsNoTracking().FirstOrDefaultAsync(e => e.Id == model.Id);
            if (entity == null)
                return RedirectToAction("Index", "CorrectionDay");

            var simularEntity = await _context.CorrectionDays
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Year == model.Year && e.Month == model.Month && e.Day == model.Day && e.Id != model.Id);
            if (simularEntity != null)
                ModelState.AddModelError("", $"Указанная дата уже есть в списке под названием '{simularEntity.Title}'");

            if (ModelState.IsValid)
            {
                entity.Day = model.Day;
                entity.Month = model.Month;
                entity.Year = model.Year;
                entity.Title = model.Title;
                entity.Type = model.Type;
                entity.Hours = model.Hours;

                _context.CorrectionDays.Update(entity);

                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "CorrectionDay");
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Не удалось выполнить сохранение изменений. Попробуйте позже или обратитесь к администратору");
                }
            }

            model.AvailableMonths = PopulateMonths();
            model.AvailableDayTypes = PopulateCorrectionDayTypes();
            return View(model);
        }
        #endregion

        #region Remove
        [HttpPost]
        public async Task<IActionResult> Remove(Guid id)
        {
            var entity = await _context.CorrectionDays.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
            if (entity != null)
            {
                _context.CorrectionDays.Remove(entity);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    //TODO: Log here
                }
            }

            return RedirectToAction("Index");
        } 
        #endregion


        private List<SelectListItem> PopulateMonths() => CommonLists.Months.Select(e => new SelectListItem(e.Value, e.Key.ToString())).ToList();
        private List<SelectListItem> PopulateCorrectionDayTypes() => CommonLists.NamesOfCorrectionDays.Select(e => new SelectListItem(e.Value, e.Key.ToString())).ToList();
    }
}
