using CommunityToolkit.Maui.Views;
using EdsmTriangulationInterface.ModelView;

namespace EdsmTriangulationInterface
{
    public partial class ErrorPage : Popup
    {
        private readonly ErrorViewModel viewModel;

        public ErrorPage(string title, string message, string details)
        {
            viewModel = new(title, message, details);

            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}