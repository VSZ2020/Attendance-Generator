namespace AG.Core.Infrastructure.Validation
{
    public class ValidationMessage
    {
        public ValidationMessage(): this("","")
        {

        }

        public ValidationMessage(string message, string? title)
        {
            this.Message = message;
            this.Title = title;
        }

        public string? Title { get; set; }

        public string Message { get; set; }

        
    }
}
