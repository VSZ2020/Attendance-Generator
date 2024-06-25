namespace AG.WPF.ViewModel
{
    public class ValidationMessage : BaseModel
    {
        public ValidationMessage(string? message, string title)
        {
            Message = message;
            Title = title;
        }

        private string message;
        private string title;

        public string Message { get => message; set { message = value; OnChanged(nameof(Message)); } }
        public string Title { get => title; set { title = value; OnChanged(); } }
    }
}
