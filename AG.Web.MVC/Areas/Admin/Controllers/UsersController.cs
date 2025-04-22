using AG.Core.Policy;
using AG.Data;
using AG.Data.Entities;
using AG.Services.Repository;
using AG.Services.Utils;
using AG.Web.MVC.Areas.Admin.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AG.Web.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        #region ctor
        public UsersController
            (DataContext context, 
            ILogger<UsersController> logger, 
            AuthenticationService authService, 
            UserService userService)
        {
            _logger = logger;
            _context = context;
            _authService = authService;
            _userService = userService;
        } 
        #endregion

        private readonly DataContext _context;
        private readonly ILogger<UsersController> _logger;
        private readonly AuthenticationService _authService;
        private readonly UserService _userService;

        #region Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await PopulateUsersListAsync());
        } 
        #endregion

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View(await PrepareCreateUserModelAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (string.IsNullOrEmpty(model.Role))
                ModelState.AddModelError("Role", "Не выбрана роль пользователя в системе");

            if (await _authService.IsUsernameExistsAsync(model.Username))
                ModelState.AddModelError("Username", "Имя пользователя уже занято");

            if (ModelState.IsValid)
            {
                var entity = new UserEntity()
                {
                    Username = model.Username,
                    PasswordHash = HashUtils.HashPassword(model.Password),
                    Role = model.Role,
                    Email = model.Email,
                    CreatedAt = DateTime.Now,
                    IsEmailConfirmed = model.IsEmailConfirmed,
                    IsActivatedAccount = model.IsActivatedAccount, 
                    EmployeeId = model.AssignedEmployeeId,
                    DepartmentId = model.AssignedDepartmentId,
                };

                await _context.Users.AddAsync(entity);

                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    _logger.LogError(ex, "Can't save changes in database during creation of a new user");
                    ModelState.AddModelError("Database", "Не удалось создать нового пользователя. Подождите или обратитесь к администратору");
                }
            }

            model.AvailableRoles = PopulateRolesList();
            model.AvailableEmployees = await PopulateEmployeesAsync();
            model.AvailableDepartmetns = await PopulateDepartmentsAsync();
            return View(model);
        }
        #endregion

        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var user = await PrepareEditUserModelAsync(id);
            if (user == null)
            {
                _logger.LogWarning("Can't find user with Id " + id.ToString());
                return RedirectToAction("Index");
            }
            return View(new EditUserViewModel()
            {
                Id = id,
                Username = user.Username,
                Email = user.Email,
                IsActivatedAccount = user.IsActivatedAccount,
                IsEmailConfirmed = user.IsEmailConfirmed,

                AssignedDepartmentId = user.AssignedDepartmentId,
                AssignedEmployeeId = user.AssignedEmployeeId,

                AvailableDepartmetns = await PopulateDepartmentsAsync(),
                AvailableEmployees = await PopulateEmployeesAsync()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            var entity = await _context.Users.AsNoTracking().FirstOrDefaultAsync(e => e.Id == model.Id);
            if (entity == null)
            {
                _logger.LogWarning("Can't find user with Id " + model.Id.ToString());
                return RedirectToAction("Index");
            }

            if (!await _userService.CheckEmployeeVacantedAsync(model.AssignedEmployeeId, model.Id))
            {
                ModelState.AddModelError("EmployeeId", "Данный сотрудник уже связан с другим пользователем");
            }

            if (ModelState.IsValid)
            {
                entity.Email = model.Email;
                entity.IsEmailConfirmed = model.IsEmailConfirmed;
                entity.IsActivatedAccount = model.IsActivatedAccount;
                entity.EmployeeId = model.AssignedEmployeeId;
                entity.DepartmentId = model.AssignedDepartmentId;
                _context.Users.Update(entity);

                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("UserEditing", "Не удалось сохранить изменения. Попробуйте позже или обратитесь к администратору");
                    _logger.LogError(ex, "Can't save database while editing user data");
                }
            }

            model.AvailableRoles = PopulateRolesList();
            model.AvailableEmployees = await PopulateEmployeesAsync();
            model.AvailableDepartmetns = await PopulateDepartmentsAsync();
            return View(model);
        }
        #endregion

        #region ResetPassword
        [HttpGet]
        public async Task<IActionResult> ResetPassword(Guid id)
        {
            var entity = await _context.Users.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
            if (entity == null)
            {
                _logger.LogWarning("Can't find user with Id " + id.ToString());
                return RedirectToAction("Index");
            }

            return View(new ResetPasswordViewModel()
            {
                Id = entity.Id,
                Username = entity.Username,
            });
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            var entity = await _context.Users.AsNoTracking().FirstOrDefaultAsync(e => e.Id == model.Id);
            if (entity == null)
            {
                _logger.LogWarning("Can't find user with Id " + model.Id.ToString());
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                entity.PasswordHash = HashUtils.HashPassword(model.NewPassword);
                _context.Users.Update(entity);

                try
                {
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"The password has been reset for user ({model.Username}:{model.Id})");

                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    _logger.LogError(ex, "Can't save changes in database during password reset for user with id " + model.Id.ToString());
                }
            }

            return View(model);
        }
        #endregion

        #region ReplaceRole
        [HttpGet]
        public async Task<IActionResult> ReplaceRole(Guid id)
        {
            var entity = await _context.Users.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
            if (entity == null)
            {
                _logger.LogWarning("Can't find user with Id " + id.ToString());
                return RedirectToAction("Index");
            }

            return View(new ReplaceRoleViewModel()
            {
                Id = entity.Id,
                Username = entity.Username,
                Role = entity.Role,
                AvailableRoles = PopulateRolesList(),
            });
        }

        [HttpPost]
        public async Task<IActionResult> ReplaceRole(ReplaceRoleViewModel model)
        {
            var entity = await _context.Users.AsNoTracking().FirstOrDefaultAsync(e => e.Id == model.Id);
            if (entity == null)
            {
                _logger.LogWarning("Can't find user with Id " + model.Id.ToString());
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                entity.Role = model.Role;
                _context.Users.Update(entity);

                try
                {
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"The role has been reset for user ({model.Username}:{model.Id}). To apply changes user must logout and SignIn again");

                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    _logger.LogError(ex, $"Can't save changes in database during changing user role for {model.Username}:{model.Id}");
                }
            }

            model.AvailableRoles = PopulateRolesList();
            return View(model);
        }
        #endregion

        #region Assignment
        [HttpGet]
        public async Task<IActionResult> Assignment(Guid id)
        {
            var entity = await _context.Users.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
            if (entity == null)
            {
                _logger.LogWarning("No user with Id" + id.ToString());
                return RedirectToAction("Index");
            }

            return View(new ReassignEmployeeViewModel()
            {
                Id = id,
                Username = entity.Username,
                AssignedEmployeeId = entity.EmployeeId,
                AvailableEmployees = await PopulateEmployeesAsync()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Assignment(ReassignEmployeeViewModel model)
        {
            var entity = await _context.Users.AsNoTracking().FirstOrDefaultAsync(e => e.Id == model.Id);
            if (entity == null)
            {
                _logger.LogWarning("No user with Id" + model.Id.ToString());
                return RedirectToAction("Index");
            }

            if (!await _userService.CheckEmployeeVacantedAsync(model.AssignedEmployeeId, model.Id))
            {
                ModelState.AddModelError("EmployeeId", "Данный сотрудник уже связан с другим пользователем");
            }

            if (ModelState.IsValid)
            {

                entity.EmployeeId = model.AssignedEmployeeId;
                _context.Users.Update(entity);

                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    _logger.LogError(ex, "Can't save changes in database during reassignment of employee with user account (id: " + model.Id.ToString());
                    ModelState.AddModelError("", "Не удалось применить изменения. Попробуйте позже, либо обратитесь к администратору");
                }
            }

            model.AvailableEmployees = await PopulateEmployeesAsync();
            return View(model);
        } 
        #endregion

        #region PrepareCreateUserModelAsync
        private async Task<CreateUserViewModel> PrepareCreateUserModelAsync()
        {

            return new CreateUserViewModel()
            {
                Username = "",
                Role = "",
                IsActivatedAccount = true,
                IsEmailConfirmed = true,
                AvailableRoles = PopulateRolesList(),
                AvailableEmployees = await PopulateEmployeesAsync(),
                AvailableDepartmetns = await PopulateDepartmentsAsync()
            };
        }
        #endregion

        #region PrepareCreateUserModelAsync
        private async Task<EditUserViewModel?> PrepareEditUserModelAsync(Guid id)
        {
            var entity = await _context.Users.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
            if (entity == null)
                return null;

            return new EditUserViewModel()
            {
                Id = entity.Id,
                Email = entity.Email ?? "",
                Username = entity.Username,
                IsActivatedAccount = entity.IsActivatedAccount,
                IsEmailConfirmed = entity.IsEmailConfirmed,

                AssignedEmployeeId = entity.EmployeeId,
                AssignedDepartmentId = entity.DepartmentId,

                //AvailableRoles = PopulateRolesList(),
                AvailableEmployees = await PopulateEmployeesAsync(),
                AvailableDepartmetns = await PopulateDepartmentsAsync()
            };
        }
        #endregion

        #region PopulateUsersListAsync
        private async Task<IndexUsersViewModel> PopulateUsersListAsync()
        {
            var entities = await _context.Users.AsNoTracking().Include(e => e.Employee).Include(e => e.Department).ToListAsync();

            var users = entities.Select(e =>
            {
                return new UserViewModel()
                {
                    Id = e.Id,
                    Role = e.Role,
                    Username = e.Username,
                    Email = e.Email ?? "-",
                    IsEmailConfirmed = e.IsEmailConfirmed,
                    IsActivatedAccount = e.IsActivatedAccount,
                    CreatedAt = e.CreatedAt,
                    LastVisit = e.LastVisit,
                    
                    AssignedEmployee = e.Employee != null ? NameUtils.ToShortName(e.Employee) : "Нет",
                    AssigmedDepartment = e.Department?.Name ?? "Нет",
                };
            }).ToList();

            return new IndexUsersViewModel()
            {
                Users = users
            };
        }
        #endregion

        #region PopulateRolesList
        private SelectList PopulateRolesList()
        {
            return new SelectList(GetRoles(), "Key", "Value");
        }

        private Dictionary<string, string> GetRoles() => new Dictionary<string, string>()
        {
            {"", "Не выбрана" },
            {DefaultRoles.ADMIN, "Администратор" },
            {DefaultRoles.MODERATOR, "Модератор" },
            {DefaultRoles.HR, "Специалист по кадрам" },
            {DefaultRoles.EMPLOYEE, "Сотрудник" },
        };
        #endregion

        #region PopulateDepartmentsAsync
        private async Task<SelectList> PopulateDepartmentsAsync()
        {
            var entities = await _context.Departments.AsNoTracking().Select(d => new { Name = d.Name, Id = new Nullable<Guid>(d.Id) }).ToListAsync();
            entities.Insert(0, new { Name = "Не выбрано", Id = new Nullable<Guid>() });
            return new SelectList(entities, "Id", "Name");
        }
        #endregion

        #region PopulateEmployeesAsync
        private async Task<SelectList> PopulateEmployeesAsync()
        {
            var entities = await _context.Employees.AsNoTracking().Select(d => new { ShortName = NameUtils.ToShortName(d), Id = new Nullable<Guid>(d.Id) }).ToListAsync();
            entities.Insert(0, new { ShortName = "Не выбран", Id = new Nullable<Guid>() });

            return new SelectList(entities, "Id", "ShortName");
        } 
        #endregion
    }
}
