using CommunityToolkit.Maui.Views;
using EdsmTriangulationInterface.ModelView;

namespace EdsmTriangulationInterface
{
    public partial class ChoicePage : Popup
    {
        private readonly ChoiceViewModel viewModel;

        public ChoicePage(string title, string message)
        {
            viewModel = new(title, message);

            InitializeComponent();
            BindingContext = viewModel;
        }

        public void OnYesButtonClicked(object sender, EventArgs e) => Close(true);

        public void OnNoButtonClicked(object sender, EventArgs e) => Close(false);
    }
}