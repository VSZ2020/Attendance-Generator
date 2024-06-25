using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AG.WPF.ViewModel
{
    public class BaseModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged region
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
