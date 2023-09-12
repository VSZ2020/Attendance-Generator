using AG.Services;
using Core.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Services.Database;
using Services.Infrastructure.Logger;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace AG.ViewModels.Forms
{
	public class AuthFormViewModel: ViewModelCore
	{
		#region ctor
		public AuthFormViewModel(): base() { }
		#endregion

		#region fields
		
		#endregion

		#region Properties
		
		#endregion

		public bool AuthUser(string login, string passwd)
		{
			if (ValidateLoginPass(login,passwd))
			{
				if (true)
				{
					var user = ServiceLocator.GetService<IUserAccountService>().GetUserById(1).Results.First();
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
