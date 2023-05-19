using AG.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AG.ViewModels.Forms
{
	public class MainWindowViewModel: INotifyPropertyChanged
    {
		#region ctor
		public MainWindowViewModel()
		{

		}
		#endregion ctor

		#region properties
		public string Username { get => SessionService.User?.Username ?? "Войти"; }

        public int EstablishmentId { get; set; } = 1;

        #endregion properties

        #region INotifyPropertyChanged region
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
