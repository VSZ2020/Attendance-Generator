using AG.Web.MVC.Areas.Account.Models.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AG.Web.MVC.Areas.Account.Controllers
{
    [Area("Account")]
    public class AccountController : Controller
    {
        #region ctor
        public AccountController(ILogger<AccountController> logger, Services.Repository.AuthenticationService authService)
        {
            _logger = logger;
            _authService = authService;
        } 
        #endregion

        private readonly ILogger<AccountController> _logger;
        private readonly Services.Repository.AuthenticationService _authService;

        #region Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!(await _authService.IsUsernameExistsAsync(model.Username)))
            {
                ModelState.AddModelError("Username", "Пользователя с таким именем не существует");
            }

            if (!ModelState.IsValid)
                return View();

            var user = await _authService.AuthenticateUserAsync(model.Username, model.Password);
            if (user == null)
                ModelState.AddModelError("", "Неверная комбинация логин-пароль");

            if (ModelState.IsValid)
            {
                Claim[] claims =
                [
                    new Claim(ClaimTypes.NameIdentifier, user!.Id.ToString()),
                    new Claim(ClaimTypes.Name, user!.Username),
                    new Claim(ClaimTypes.Role, user!.Role),
                    new Claim(ClaimTypes.Email, user!.Email ?? ""),
                ];
                ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(principal);
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
        #endregion


        #region Logout
        public async Task<IActionResult> Logout()
        {
            if (User != null && (User.Identity?.IsAuthenticated ?? false))
            {
                await HttpContext.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Login");
        } 
        #endregion
    }
}
