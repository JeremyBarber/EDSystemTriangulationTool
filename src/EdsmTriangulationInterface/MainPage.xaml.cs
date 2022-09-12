using EdsmTriangulationCore;
using EdsmTriangulationInterface.ModelView;
using ListViewDemos.ViewModels;

namespace EdsmTriangulationInterface
{
    public partial class MainPage : ContentPage
    {
        private readonly TriangulationViewModel viewModel = new();

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
                var previousSelectedTarget = (TargetViewModel)targetsListView.SelectedItem;
                await viewModel.TryAddNewSource(systemNameInput.Text, (int)minDistanceSlider.Value, (int)maxDistanceSlider.Value);
                if (!viewModel.Targets.Contains(previousSelectedTarget))
                {
                    await viewModel.SetTargetDetails(string.Empty);
                }
                systemNameInput.Text = string.Empty;
            }));
            ToggleSourcesWaiting(false);
        }

        public async void OnSourceSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var selectedSource = ((SourceViewModel)args.SelectedItem);
            if (!await DisplayAlert("Would you like to remove the selected source?", $"{selectedSource.systemName} ({selectedSource.minRadius}-{selectedSource.radius})", "Delete", "Cancel"))
            {
                return;
            }

            ToggleSourcesWaiting(true);
            await RunWithErrorHandling(new Func<Task>(async () =>
            {        
                await viewModel.TryRemoveSelectedSource(selectedSource);

                if (!viewModel.Sources.Any())
                {
                    await viewModel.SetTargetDetails(string.Empty);
                }

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
            var helpText = new List<string>
            {
                "A little tool that I hope was useful",
                "",
                "Bug Reports",
                "https://github.com/JeremyBarber/EDSystemTriangulationTool/issues",
                "",
                "Good luck out there CMDR"
            };

            await DisplayAlert("About", string.Join(Environment.NewLine, helpText), "Close");
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
            systemNameInput.IsVisible = !isWaiting;
            minDistanceSlider.IsVisible = !isWaiting;
            minDistanceLabel.IsVisible = !isWaiting;
            maxDistanceSlider.IsVisible = !isWaiting;
            maxDistanceLabel.IsVisible = !isWaiting;

            sourcesListView.IsVisible = !isWaiting;

            sourceListBackground.IsVisible = !isWaiting;
            addSourceBackground.IsVisible = !isWaiting;

            sourceListSpinner.IsVisible = isWaiting;
            addSourceSpinner.IsVisible = isWaiting;

            ToggleTargetsWaiting(isWaiting);
        }

        private void ToggleTargetsWaiting(bool isWaiting)
        {
            targetsListView.IsVisible = !isWaiting;
            targetListView.IsVisible = !isWaiting;
            targetListBackground.IsVisible = !isWaiting;
            targetDetailsBackground.IsVisible = !isWaiting;

            targetListSpinner.IsVisible = isWaiting;
            targetDetailsSpinner.IsVisible = isWaiting;
        }
    }
}