using AttendanceGenerator.Model.Department;
using AttendanceGenerator.Model.Session.UserAccount;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace AttendanceGenerator.Forms
{
    public class MainWndViewModel : INotifyPropertyChanged
    {
        #region InfoMessage
        private string _infoMessageHeader = string.Empty;
        private string _infoMessageText = string.Empty;
        private bool _infoMessageVisible = true;
        public string InfoMessageHeader { get => _infoMessageHeader; 
            set {
                _infoMessageHeader = value;
                OnPropertyChanged(nameof(InfoMessageHeader));
            } 
        }
        public string InfoMessageText { get => _infoMessageText; 
            set { 
                _infoMessageText = value;
                OnPropertyChanged(nameof(InfoMessageText));
            } 
        }
        public bool InfoMessageVisible { get => _infoMessageVisible;
            set { 
                _infoMessageVisible = value;
                OnPropertyChanged(nameof(InfoMessageVisible));
            }
        }
        
        public void ShowMessage(string Header, string Text, bool IsVisible = true)
        {
            InfoMessageHeader = Header;
            InfoMessageText = Text;
            InfoMessageVisible = IsVisible;
        }
        public void ClearMessage()
        {
            InfoMessageHeader = string.Empty;
            InfoMessageText = string.Empty;
            InfoMessageVisible = false;
        }
        public void HideMessage() => InfoMessageVisible = false;
        #endregion InfoMessage

        #region UserAccount
        private string _accountUsername = string.Empty;
        private bool _usernameFieldEnable = true;
        public string Username { get => _accountUsername; 
            set
            {
                _accountUsername = value;
                OnPropertyChanged(nameof(Username));
            } 
        }
        public bool IsUsernameEnabled
        {
            get => _usernameFieldEnable;
            set
            {
                _usernameFieldEnable = value;
                OnPropertyChanged(nameof(IsUsernameEnabled));
            }
        }

        public bool IsUserOptionsVisible
        {
            get => _usernameFieldEnable;
            set
            {
                _usernameFieldEnable = value;
                OnPropertyChanged(nameof(IsUserOptionsVisible));
            }
        }
        private bool _adminAccount = false;
        public bool IsAdministratorAccount { get => _adminAccount; 
            set{
                _adminAccount = value;
                OnPropertyChanged(nameof(IsAdministratorAccount));
            } 
        }
        public List<Department> AttachedDepartments { get; set; } = new List<Department>();
        public Department? SelectedDepartment { get; set; }
        private string _userrolename = "None";
        public string UserRoleName { get => _userrolename; 
            set
            {
                _userrolename = value;
                OnPropertyChanged(nameof(UserRoleName));
            }
        }
        public void UserLoggedOut()
        {
            Username = "Войти";
            UserRoleName = "Роль: None";
            IsUserOptionsVisible = false; 
            AttachedDepartments.Clear();
        }

        public void UserLoggedIn(UserAccount account)
        {
            Username = account.Username;
            UserRoleName = "Роль: " + account.UserRole.Name;
            if (account.Departments != null)
            {
                AttachedDepartments.Clear();
                AttachedDepartments.AddRange(account.Departments);
                if (AttachedDepartments.Count > 0)
                {
                    SelectedDepartment = AttachedDepartments[0];
                }
            }
            IsUserOptionsVisible = true;
            if (account.RoleId == 1) //if  Administrator account
            {
                IsAdministratorAccount = true;
            }
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion INotifyPropertyChanged
    }
}
