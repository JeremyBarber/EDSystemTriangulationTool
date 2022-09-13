using CommunityToolkit.Maui.Views;
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
            var choicePage = new ChoicePage($"{selectedSource.systemName} ({selectedSource.minRadius}-{selectedSource.radius})", "Would you like to remove the selected source?");
            var delete = (bool?) await this.ShowPopupAsync(choicePage) ?? false;
            if (!delete)
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
            this.ShowPopup(new AboutPage());
        }

        private async Task RunWithErrorHandling(Func<Task> command)
        {
            try
            {
                await command();
            }
            catch (UserFacingException ex)
            {
                this.ShowPopup(new ErrorPage("Attention, CMDR!", ex.Message, string.Empty));
            }
            catch (Exception ex)
            {
                this.ShowPopup(new ErrorPage(
                    "Oh dear, I'm really sorry CMDR", 
                    "Something appears to have broken in a way I didn't expect. " + Environment.NewLine + Environment.NewLine +
                    "Please consider opening a bug report and berating me (including a copy of the text below if you dont mind!)", ex.Message));
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