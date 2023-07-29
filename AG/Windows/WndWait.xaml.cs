using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace AG.Windows
{
	/// <summary>
	/// Логика взаимодействия для WndWait.xaml
	/// </summary>
	public partial class WndWait : Window, INotifyPropertyChanged
	{
		private string messageText = "";

		

		public string MessageText { get => messageText; set { messageText = value; OnChanged(); } }
		public WndWait(string? message = null)
		{
			InitializeComponent();
			this.WindowStyle = WindowStyle.None;

			this.DataContext = this;
			
			if (!string.IsNullOrEmpty(message))
				MessageText = message;
			else
				MessageText = "Ожидайте";
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		public void OnChanged([CallerMemberName]string? name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
	}
}
