namespace EdsmTriangulationInterface.ModelView
{
    public class ChoiceViewModel
    {
        public string Title { get; private set; }
        public string Message { get; private set; }

        public ChoiceViewModel(string title, string message)
        {
            Title = title;
            Message = message;
        }
    }
}
