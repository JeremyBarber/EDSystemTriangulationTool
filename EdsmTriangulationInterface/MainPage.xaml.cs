using EdsmTriangulationInterface.ModelView;
using ListViewDemos.ViewModels;

namespace EdsmTriangulationInterface
{
    public partial class MainPage : ContentPage
    {
        private readonly TriangulationViewModel viewModel = new TriangulationViewModel();

        public MainPage()
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        public async void OnAddButtonClicked(object sender, EventArgs e)
        {
            ToggleSourcesWaiting(true);
            try
            {
                await viewModel.TryAddNewSource(systemNameEditor.Text, (int) minDistanceStepper.Value, (int) maxDistanceStepper.Value);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Attention, CMDR!", ex.Message, "OK");
            }
            ToggleSourcesWaiting(false);
        }

        public async void OnRemoveButtonClicked(object sender, EventArgs e)
        {
            //ToggleSourcesWaiting(true);
            //await viewModel.RemoveMostRecentSource();
            //ToggleSourcesWaiting(false);

            await viewModel.SetTargetDetails("Pleiades Sector GH-V c2-7");
        }

        public async void OnTargetSelected(object sender, SelectedItemChangedEventArgs args)
        {
            ToggleTargetsWaiting(true);
            var name = ((TargetViewModel)args.SelectedItem).Name;
            await viewModel.SetTargetDetails(name);
            ToggleTargetsWaiting(false);
        }

        public void OnMinDistanceValueChanged(object sender, ValueChangedEventArgs args)
        {
            var value = (int) args.NewValue;
            maxDistanceStepper.Minimum = value;
        }

        public void OnMaxDistanceValueChanged(object sender, ValueChangedEventArgs args)
        {
            var value = (int) args.NewValue;
            minDistanceStepper.Maximum = value;
        }

        private void ToggleSourcesWaiting(bool isWaiting)
        {
            addButton.IsVisible = !isWaiting;
            removeButton.IsVisible = !isWaiting;
            systemNameEditor.IsVisible = !isWaiting;
            minDistanceStepper.IsVisible = !isWaiting;
            minDistanceLabel.IsVisible = !isWaiting;
            maxDistanceStepper.IsVisible = !isWaiting;
            maxDistanceLabel.IsVisible = !isWaiting;

            sourcesListView.IsEnabled = !isWaiting;

            addSourceSpinner.IsVisible = isWaiting;

            ToggleTargetsWaiting(isWaiting);
        }

        private void ToggleTargetsWaiting(bool isWaiting)
        {
            targetsListView.IsEnabled = !isWaiting;

            targetDetailsSpinner.IsVisible = isWaiting;
        }
    }
}