namespace EdsmTriangulationInterface.ModelView
{
    public class ErrorViewModel
    {
        public string Title { get; private set; }
        public string Message { get; private set; }
        public string Details { get; private set; }
        public bool DetailsMissing
        {
            get => string.IsNullOrWhiteSpace(Details);
        }
        public bool DetailsPresent
        {
            get => !DetailsMissing;
        }

        public ErrorViewModel(string title, string message, string details = "")
        {
            Title = title;
            Message = message;
            Details = details;
        }
    }
}
