using EdsmTriangulationCore;
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
            await RunWithErrorHandling(new Func<Task>(async () =>
            {
                await viewModel.TryAddNewSource(systemNameEditor.Text, (int)minDistanceSlider.Value, (int)maxDistanceSlider.Value);
                systemNameEditor.Text = string.Empty;
            }));
            ToggleSourcesWaiting(false);
        }

        public async void OnRemoveButtonClicked(object sender, EventArgs e)
        {
            ToggleSourcesWaiting(true);
            await RunWithErrorHandling(new Func<Task>(async () =>
            {
                await viewModel.RemoveMostRecentSource();
            }));
            ToggleSourcesWaiting(false);
        }

        public async void OnTargetSelected(object sender, SelectedItemChangedEventArgs args)
        {
            ToggleTargetsWaiting(true);
            await RunWithErrorHandling(new Func<Task>(async () =>
            {
                var name = ((TargetViewModel)args.SelectedItem).Name;
                await viewModel.SetTargetDetails(name);
            }));
            ToggleTargetsWaiting(false);
        }

        public void OnMinDistanceValueChanged(object sender, ValueChangedEventArgs args)
        {
            var value = (int) args.NewValue;

            if (value > maxDistanceSlider.Value)
            {
                maxDistanceSlider.Value = value;
            }
        }

        public void OnMaxDistanceValueChanged(object sender, ValueChangedEventArgs args)
        {
            var value = (int) args.NewValue;

            if (value < minDistanceSlider.Value)
            {
                minDistanceSlider.Value = value;
            }
        }

        public async void OnAboutButtonClicked(object sender, EventArgs e)
        {
            await DisplayAlert("About", $"A silly little program thrown together.{Environment.NewLine}" +
                $"If you're feeling generous, and it hasn't crashed too much, you can support the author here", "Close");
        }

        private async Task RunWithErrorHandling(Func<Task> command)
        {
            try
            {
                await command();
            }
            catch (UserFacingException ex)
            {
                await DisplayAlert("Attention, CMDR!", ex.Message, "Continue");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Attention, CMDR!", $"Something has gone wrong. I apologise for the convenience. The error is '{ex.Message}'", "Continue");
            }
        }

        private void ToggleSourcesWaiting(bool isWaiting)
        {
            addButton.IsVisible = !isWaiting;
            //removeButton.IsVisible = !isWaiting;
            systemNameEditor.IsVisible = !isWaiting;
            minDistanceSlider.IsVisible = !isWaiting;
            minDistanceLabel.IsVisible = !isWaiting;
            maxDistanceSlider.IsVisible = !isWaiting;
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