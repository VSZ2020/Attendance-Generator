using AG.Services;
using Microsoft.Extensions.DependencyInjection;
using SQLiteRepository;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AG.ViewModels.Forms
{
    public class MainWindowViewModel: INotifyPropertyChanged
    {
        public string Username { get => SessionService.User?.Username ?? "Войти"; }

        public int EstablishmentId { get; set; } = 1;
        public MainWindowViewModel()
        {
            
        }

        #region INotifyPropertyChanged region
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
