using Edsm.Sdk;
using Edsm.Sdk.Model.Edsm.Types;
using EdsmTriangulationCore;
using EDSMTriangulationCore.Services;
using EdsmTriangulationInterface.ModelView;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ListViewDemos.ViewModels
{
    public class TriangulationViewModel : INotifyPropertyChanged
    {
        private readonly IModel _model;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<SourceViewModel> Sources
        {
            get => new(_model.Sources.Select(x => new SourceViewModel(x)));
        }

        public ObservableCollection<TargetViewModel> Targets
        {
            get => new(_model.Targets.Select(x => new TargetViewModel(x)));
        }

        private List<IBody> _targetDetailsCollection = new();
        public ObservableCollection<TargetDetailsViewModel> TargetDetails
        {
            get => new(_targetDetailsCollection.Select(x => new TargetDetailsViewModel(x)));
        }

        public int SourcesCount
        {
            get => Sources.Count;
        }

        public int TargetsCount
        {
            get => Targets.Count;
        }

        public string Version
        {
            get => Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        public TriangulationViewModel()
        {
            var client = new EdsmClient(new HttpClient());
            _model = new Model(client);
        }

        public async Task TryAddNewSource(string name, int minRadius, int maxRadius)
        {
            if(!await _model.CheckSourceExists(name))
            {
                throw new InputValidationException($"System '{name}' does not appear to exist");
            }
            
            if (minRadius > maxRadius)
            {
                throw new InputValidationException($"The minimum search radius '{minRadius}' is larger than the maxRadius '{maxRadius}'");
            }

            await _model.AddSource(name, minRadius, maxRadius);

            SyncSources();
        }

        public async Task TryRemoveSelectedSource(SourceViewModel source)
        {
            await _model.RemoveSource(source.systemName, source.minRadius, source.radius);

            SyncSources();
        }

        public async Task RemoveMostRecentSource()
        {
            await _model.RemoveLastSource();

            SyncSources();
        }

        public async Task SetTargetDetails(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                _targetDetailsCollection.Clear();
            }
            else
            {
                _targetDetailsCollection = (await _model.GetTargetDetails(name)).bodies;
            }

            SyncSources();
        }

        private void SyncSources()
        {
            OnPropertyChanged(nameof(Sources));
            OnPropertyChanged(nameof(Targets));
            OnPropertyChanged(nameof(TargetDetails));
            OnPropertyChanged(nameof(SourcesCount));
            OnPropertyChanged(nameof(TargetsCount));
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
