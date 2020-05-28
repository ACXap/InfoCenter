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
        public FoundCompanyFsspViewModel(IFoundPersonFsspService foundervice, ISettingsService settings)
        {
            TypeGrid = settings?.GetSettings().TypeGrid;

            _serviceFound = foundervice;

            CollectionRegion = new ReadOnlyCollection<Region>(_serviceRegion.GetRegion());
        }

        #region PrivateField
        private readonly IFoundPersonFsspService _serviceFound;
        private readonly IServiceRegion _serviceRegion = new ServiceRegion();

        private ObservableCollection<RequestFoundPerson> _collectionRequest;
        private ReadOnlyCollection<Region> _collectionRegion;
        private Region _currentRegion;

        private RelayCommand _commandFoundPerson;
        private RelayCommand<RequestFoundPerson> _commandGetFile;
        #endregion PrivateField

        #region PublicProperties
        public ObservableCollection<RequestFoundPerson> CollectionRequest
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

                var result = await _serviceFound.GetCompany(FoundCompany).ConfigureAwait(true);

                if (_collectionRequest == null) CollectionRequest = new ObservableCollection<RequestFoundPerson>();
                if (result.Item != null) CollectionRequest.Add(result.Item);

                StopProcess(result.ErrorResult);
            }, () => !string.IsNullOrEmpty(FoundCompany.Name) && FoundCompany.Region != null));
        #endregion Command

        public RelayCommand<RequestFoundPerson> CommandGetFile =>
        _commandGetFile ?? (_commandGetFile = new RelayCommand<RequestFoundPerson>(
            req =>
            {
                _serviceFound.GetPersonFile(req.FileResult);
            }));
    }
}