using System.Collections.ObjectModel;

namespace Core.ViewModel
{
	public class ViewModelCore: BaseModel
	{
		#region fields
		private ObservableCollection<ValidationMessage> validationMessages = new ObservableCollection<ValidationMessage>();

		private string waitMessageTitle = "";
		private string waitMessageText = "";
		private bool isWaitMessageVisible = false;
		#endregion

		#region Properties
		public ObservableCollection<ValidationMessage> ValidationMessages { get => validationMessages; set { validationMessages = value; OnChanged(); } }

		public string WaitMessageTitle { get => waitMessageTitle; set { waitMessageTitle = value; OnChanged(); } }
		public string WaitMessageText { get => waitMessageText; set { waitMessageText = value; OnChanged(); } }
		public bool IsWaitMessageVisible { get => isWaitMessageVisible; set { isWaitMessageVisible= value; OnChanged(); } }

		public bool IsValid => ValidationMessages.Count == 0;
		#endregion

		#region AddValidationMessage
		public void AddValidationMessage(ValidationMessage message)
		{
			ValidationMessages.Add(message);
			OnChanged(nameof(IsValid));
		}

		public void AddValidationMessage(string message, string title)
		{
			AddValidationMessage(new ValidationMessage(message, title));
		}
		#endregion

		#region ClearValidationMessages
		public void ClearValidationMessages()
		{
			ValidationMessages.Clear();
			OnChanged(nameof(IsValid));
		}
		#endregion

		public void ShowWaitMessage(string message, string title)
		{
			WaitMessageTitle = title;
			WaitMessageText = message;
			IsWaitMessageVisible = true;
		}

		public void ClearWaitMessage()
		{
			IsWaitMessageVisible = false;
			WaitMessageTitle = "";
			WaitMessageText = "";
		}
	}
}
