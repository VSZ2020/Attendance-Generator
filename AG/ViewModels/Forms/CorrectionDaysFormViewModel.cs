using AG.WPF.ViewModel;
using AG.WPF.ViewModels;
using Core.Calendar;
using Services.Database;
using Services.Domains;
using Services.Extensions;
using Services.Infrastructure;
using Services.Infrastructure.Logger;
using Services.Session;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace AG.WPF.ViewModels.Forms
{
    public class CorrectionDaysFormViewModel : ViewModelCore
    {
        #region ctor
        public CorrectionDaysFormViewModel()
        {
            departmentsService = ServicesLocator.GetService<IDepartmentsService>()!;
            currentEstablishmentId = SessionService.CurrentEstablishemntId ?? Guid.Empty;

            LoadCorrectionDays();
        }
        #endregion

        #region fields
        private IDepartmentsService departmentsService;
        private Guid currentEstablishmentId;

        private ObservableCollection<CorrectionDay> daysList = new();
        private Dictionary<DayType, string> popupDayTypes = GetAvailableDayTypes();
        private DateTime popupDate;
        private DayType popupSelectedDayType;
        private CorrectionDay? selectedDay;
        private bool isPopupShowed;
        #endregion

        #region Properties
        public bool IsPopup { get => isPopupShowed; set { isPopupShowed = value; OnChanged(); } }
        public bool CanRemoveDay { get => DaysList.Count > 0; set { OnChanged(); } }
        public ObjectOperationType CurrentObjectOperationType { get; set; }
        public ObservableCollection<CorrectionDay> DaysList { get => daysList; set { daysList = value; OnChanged(); } }
        public Dictionary<DayType, string> DayTypes { get => popupDayTypes; set { popupDayTypes = value; OnChanged(); } }
        public DateTime SelectedPopupDateTime { get => popupDate; set { popupDate = value; OnChanged(); } }
        public DayType SelectedPopupDayType { get => popupSelectedDayType; set { popupSelectedDayType = value; OnChanged(); } }
        public CorrectionDay? SelectedDayToCorrect { get => selectedDay; set { selectedDay = value; OnChanged(); } }
        #endregion

        #region GetAvailableDayTypes()
        public static Dictionary<DayType, string> GetAvailableDayTypes()
        {
            return Enum.GetValues<DayType>().ToDictionary(k => k, DepartmentServiceUtils.DayTypeNameTranslator);
        }
        #endregion

        #region ApplyDayAdd()
        public void ApplyDayAdd()
        {
            var newDayToCorrect = new CorrectionDay(SelectedPopupDateTime, SelectedPopupDayType, currentEstablishmentId);

            if (DaysList.Where(d => d.Date.Year == newDayToCorrect.Date.Year && d.Date.Month == newDayToCorrect.Date.Month && d.Date.Day == newDayToCorrect.Date.Day).Count() == 0)
            {
                DaysList.Add(newDayToCorrect);
                OnChanged(nameof(CanRemoveDay));
            }
            else
                MessageBox.Show("Дата уже есть в списке", "", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion

        #region RemoveDay()
        public async void RemoveDay()
        {
            if (SelectedDayToCorrect != null)
            {
                try
                {
                    var removeResult = await departmentsService.RemoveCorrectionDay(SelectedDayToCorrect);
                    if (removeResult)
                    {
                        DaysList.Remove(SelectedDayToCorrect);
                        OnChanged(nameof(CanRemoveDay));
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(ex, "Remove correction day operation");
                    MessageBox.Show($"Не удалось удалить день. Причина: {ex.Message}");
                }

            }
            else
                MessageBox.Show("Сначала выберите из списка день, который нужно удалить", "", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        #endregion

        #region ApplyDayEdit()
        public void ApplyDayEdit()
        {
            if (SelectedDayToCorrect != null)
            {
                SelectedDayToCorrect.Date = SelectedPopupDateTime;
                SelectedDayToCorrect.Type = SelectedPopupDayType;
            }
            else
                MessageBox.Show("Не выбран день для редактирования", "", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        #endregion

        #region ShowPopup
        public void ShowPopup(ObjectOperationType operationTag)
        {
            CurrentObjectOperationType = operationTag;
            if (operationTag == ObjectOperationType.Add)
            {
                IsPopup = true;
                SelectedPopupDateTime = DateTime.Now;
                SelectedPopupDayType = DayType.DayOff;
            }
            if (operationTag == ObjectOperationType.Edit)
            {
                if (SelectedDayToCorrect != null)
                {
                    IsPopup = true;
                    SelectedPopupDateTime = SelectedDayToCorrect.Date;
                    SelectedPopupDayType = SelectedDayToCorrect.Type;
                }
                else
                    MessageBox.Show("Не выбран день для редактирования", "", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            if (operationTag == ObjectOperationType.Remove)
            {
                RemoveDay();
            }
        }
        #endregion

        #region ClosePopup
        public void ClosePopup(ObjectOperationType? operationType = null)
        {
            IsPopup = false;
            if (operationType == null)
            {
                if (CurrentObjectOperationType == ObjectOperationType.Add)
                    ApplyDayAdd();
                if (CurrentObjectOperationType == ObjectOperationType.Edit)
                    ApplyDayEdit();
            }
        }
        #endregion

        #region LoadCorrectionDays
        public async void LoadCorrectionDays()
        {
            var days = await departmentsService.GetCorrectionDaysAsync(currentEstablishmentId);

            DaysList.Clear();
            DaysList.AddRange(days);
        }
        #endregion

        #region SaveCorrectionDays
        public async void SaveCorrectionDays()
        {
            try
            {
                var existingCorrectionDays = (await departmentsService.GetCorrectionDaysAsync(currentEstablishmentId)).Select(e => e.Id);
                foreach (var day in DaysList)
                {
                    if (existingCorrectionDays.Contains(day.Id))
                    {
                        await departmentsService.UpdateCorrectionDay(day);
                    }
                    else
                        await departmentsService.AddCorrectionDay(day);
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex, "Save Correction Days operation");
                MessageBox.Show($"Не удалось сохранить список дней. Причина {ex.Message}");
            }
        }
        #endregion
    }
}
