using AG.WPF.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Database;
using Services.Infrastructure;
using Services.Infrastructure.Logger;
using Services.Session;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace AG.WPF.ViewModels.Forms
{
    public class AuthFormViewModel : ViewModelCore
    {
        #region ctor
        public AuthFormViewModel() : base()
        {
            userAccountService = ServicesLocator.GetService<IUserAccountService>();
        }
        #endregion

        #region fields
        private readonly IUserAccountService userAccountService;
        #endregion

        #region Properties

        #endregion

        public bool AuthUser(string login, string passwd)
        {
            if (ValidateLoginPass(login, passwd))
            {
                if (true)
                {
                    var user = userAccountService.GetUserAccountsAsync().Result.First();
                    SessionService.User = user;
                    return true;
                }
                else
                {
                    Logger.Log($"Attempt to authenthicate user with Login: {login} and Password: {passwd}");
                }
            }

            return false;
        }

        public bool ValidateLoginPass(string login, string passwd)
        {
            ClearValidationMessages();

            if (login.Length < 3)
                AddValidationMessage("Неправильный формат логина. Длина логина должна быть не менее 3 символов", "Логин");
            if (!Regex.Match(login, "\\w+\\d*\\w*").Success)
                AddValidationMessage("Неправильный формат логина. Логин должен состоять только из букв и цифр и начинаться с буквы", "Логин");
            if (passwd.Length < 6)
                AddValidationMessage("Неправильный формат пароля. Длина пароля должна быть не менее 6 символов", "Пароль");

            return ValidationMessages.Count == 0;
        }
    }
}
