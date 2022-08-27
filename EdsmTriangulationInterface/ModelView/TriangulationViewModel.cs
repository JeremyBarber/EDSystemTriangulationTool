using Edsm.Sdk;
using Edsm.Sdk.Model.Edsm.Types;
using EDSMTriangulationCore.Services;
using EdsmTriangulationInterface.ModelView;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ListViewDemos.ViewModels
{
    public class TriangulationViewModel : INotifyPropertyChanged
    {
        private readonly IModel _model;

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


        public TriangulationViewModel()
        {
            var client = new EdsmClient(new HttpClient());
            _model = new Model(client);
        }

        public async Task TryAddNewSource(string name, int minRadius, int maxRadius)
        {
            if(await _model.CheckSourceExists(name))
            {
                await _model.AddSource(name, minRadius, maxRadius);

                SyncSources();
            }
            else
            {
                throw new ArgumentException($"System name not recognised", nameof(name));
            }
        }

        public async Task RemoveMostRecentSource()
        {
            await _model.DeleteLastSource();

            SyncSources();
        }

        public async Task SetTargetDetails(string name)
        {
            _targetDetailsCollection = (await _model.GetTargetDetails(name)).bodies;

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

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
