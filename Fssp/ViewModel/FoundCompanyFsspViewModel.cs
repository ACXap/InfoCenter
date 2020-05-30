using Common;
using Common.Settings.Service;
using Fssp.Data;
using Fssp.Service;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;

namespace Fssp.ViewModel
{
    public class FoundCompanyFsspViewModel : FoundViewModelBase
    {
        public FoundCompanyFsspViewModel(IFoundFsspService foundervice, ISettingsService settings)
        {
            TypeGrid = settings?.GetSettings().TypeGrid;

            _serviceFound = foundervice;

            CollectionRegion = new ReadOnlyCollection<Region>(_serviceRegion.GetRegion());
            CollectionRequest = _serviceFound?.CollectionRequest;
        }

        #region PrivateField
        private readonly IFoundFsspService _serviceFound;
        private readonly IServiceRegion _serviceRegion = new ServiceRegion();

        private ObservableCollection<RequestFound> _collectionRequest;
        private ReadOnlyCollection<Region> _collectionRegion;
        private Region _currentRegion;

        private RelayCommand _commandFoundPerson;
        private RelayCommand<RequestFound> _commandGetFile;
        #endregion PrivateField

        #region PublicProperties
        public ObservableCollection<RequestFound> CollectionRequest
        {
            get => _collectionRequest;
            private set => Set(ref _collectionRequest, value);
        }

        public ReadOnlyCollection<Region> CollectionRegion
        {
            get => _collectionRegion;
            set => Set(ref _collectionRegion, value);
        }
        public FoundCompany FoundCompany { get; } = new FoundCompany();

        public Region CurrentRegion
        {
            get => _currentRegion;
            set
            {
                Set(ref _currentRegion, value);
                FoundCompany.Region = value;
                CommandFoundPerson.RaiseCanExecuteChanged();
            }
        }

        #endregion PublicProperties

        #region Command
        public RelayCommand CommandFoundPerson =>
        _commandFoundPerson ?? (_commandFoundPerson = new RelayCommand(
            async () =>
            {
                StartProcess();

                await _serviceFound.GetCompany(FoundCompany).ConfigureAwait(false);

                StopProcess();
            }, () => !string.IsNullOrEmpty(FoundCompany.Name) && FoundCompany.Region != null));
        #endregion Command

        public RelayCommand<RequestFound> CommandGetFile =>
        _commandGetFile ?? (_commandGetFile = new RelayCommand<RequestFound>(
            req =>
            {
                _serviceFound.GetPersonFile(req.FileResult);
            }));
    }
}