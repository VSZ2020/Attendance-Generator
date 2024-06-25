using AG.WPF.ViewModel;
using Services.Database;
using Services.Domains;
using Services.Infrastructure;
using Services.Infrastructure.Logger;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AG.WPF.ViewModels.Forms
{
    public class EditEstablishmentViewModel : ViewModelCore
    {
        #region ctor
        public EditEstablishmentViewModel(Establishment? est)
        {
            departmentsService = ServicesLocator.GetService<IDepartmentsService>()!;

            if (est == null)
                editedEstablishment = MakeEstablishment();
            else
                editedEstablishment = est;

            FillEstablishmentFields(editedEstablishment);
        }
        #endregion

        private readonly IDepartmentsService departmentsService;
        private readonly Establishment editedEstablishment;

        #region Properties
        public string EstablishmentName { get; set; }
        public string ShortName { get; set; }
        public string OGRN { get; set; }
        public string INN { get; set; }
        public string Phones { get; set; }
        public string Email { get; set; }
        public string? Header { get; set; }
        public string RegistrationDate { get; set; }
        public string Address { get; set; }
        #endregion

        #region MakeEstablishment
        private Establishment MakeEstablishment()
        {
            return new Establishment()
            {
                Id = default,
                Name = "Без названия",
                ShortName = "Без названия",
                Address = "",
                Email = "",
                Phones = "",
                INN = "",
                OGRN = "",
                RegistrationDate = DateTime.Now.ToShortDateString(),
                Departments = new List<Department>(),
            };
        }
        #endregion

        #region ValidateViewModel
        public bool ValidateViewModel()
        {
            ClearValidationMessages();

            if (string.IsNullOrEmpty(EstablishmentName))
                AddValidationMessage("Название организации не может быть пустым", "Полное название организации");

            if (string.IsNullOrEmpty(ShortName))
                AddValidationMessage("Сокращенное название организации не может быть пустым", "Сокращенное название организации");

            if (string.IsNullOrEmpty(Header))
                AddValidationMessage("Руководитель не задан", "Руководитель");

            if (!string.IsNullOrEmpty(Email) && !Regex.IsMatch(Email, "/w+@/w+/.(/./w+)*"))
                AddValidationMessage("Неправильно задан адрес электронной почты. Формат адреса: name@mail.ru", "E-mail");

            //TODO: Задать паттерн перечня номеров телефона
            if (!string.IsNullOrEmpty(Phones) && !Regex.IsMatch(Phones, "(/+7 /((/d/d/d)/) (/d/d/d)-(/d/d)-(/d/d);)*"))
                AddValidationMessage("Неверный формат одного из номеров телефона. Номер должен быть в формате '+7 (000) 000-00-00;'", "Номера телефонов");

            return IsValid;
        }
        #endregion

        #region FillEstablishmentFields
        private void FillEstablishmentFields(Establishment est)
        {
            EstablishmentName = est.Name;
            ShortName = est.ShortName;
            OGRN = est.OGRN;
            INN = est.INN;
            Address = est.Address;
            Email = est.Email;
            Phones = est.Phones;
            RegistrationDate = est.RegistrationDate;
            Header = est.Header;

        }
        #endregion

        #region ApplyChanges
        public async Task<bool> ApplyChanges()
        {
            if (!ValidateViewModel()) return false;

            editedEstablishment.Name = EstablishmentName;
            editedEstablishment.ShortName = ShortName;
            editedEstablishment.Email = Email;
            editedEstablishment.Address = Address;
            editedEstablishment.Header = Header;
            editedEstablishment.OGRN = OGRN;
            editedEstablishment.INN = INN;
            editedEstablishment.RegistrationDate = RegistrationDate;
            editedEstablishment.Phones = Phones;

            try
            {

                if (editedEstablishment.Id == Guid.Empty)
                    return await departmentsService.AddEstablishmentAsync(editedEstablishment);
                else
                    return await departmentsService.UpdateEstablishmentAsync(editedEstablishment);
            }
            catch (UnauthorizedAccessException uex)
            {
                Logger.Log(uex, "Establishment window");
                return false;
            }
            catch (Exception ex)
            {
                Logger.Log(ex, "Establishment window");
                return false;
            }
        }
        #endregion
    }
}
