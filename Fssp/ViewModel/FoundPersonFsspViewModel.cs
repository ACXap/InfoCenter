using Common;
using Common.Settings.Service;
using Fssp.Data;
using Fssp.Service;
using Fssp.Service.Interface;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;

namespace Fssp.ViewModel
{
    public class FoundPersonFsspViewModel : FoundViewModelBase
    {
        public FoundPersonFsspViewModel(IFoundPersonFsspService foundervice, ISettingsService settings)
        {
            Header = "Поиск физических лиц в базе данных ФССП";
            TypeGrid = settings.GetSettings().TypeGrid;

            _serviceFound = foundervice;

            CollectionRegion = new ReadOnlyCollection<Region>(_serviceRegion.GetRegion());
        }

        #region PrivateField
        private readonly IFoundPersonFsspService _serviceFound;
        private readonly IServiceFio _serviceFio = new ServiceFio();
        private readonly IServiceRegion _serviceRegion = new ServiceRegion();

        public ObservableCollection<RequestFoundPerson> _collectionRequest;
        private ReadOnlyCollection<Region> _collectionRegion;
        private Region _currentRegion;

        private RelayCommand _commandFoundPerson;
        private RelayCommand<RequestFoundPerson> _commandGetFile;
        #endregion PrivateField

        #region PublicProperties
        public ObservableCollection<RequestFoundPerson> CollectionRequest
        {
            get => _collectionRequest;
            set => Set(ref _collectionRequest, value);
        }

        public ReadOnlyCollection<Region> CollectionRegion
        {
            get => _collectionRegion;
            private set => Set(ref _collectionRegion, value);
        }
        public FoundPerson FoundPerson { get; } = new FoundPerson();

        public Region CurrentRegion
        {
            get => _currentRegion;
            set
            {
                Set(ref _currentRegion, value);
                FoundPerson.Region = value;
                CommandFoundPerson.RaiseCanExecuteChanged();
            }
        }

        #endregion PublicProperties

        #region Command
        public RelayCommand CommandFoundPerson =>
        _commandFoundPerson ?? (_commandFoundPerson = new RelayCommand(
            async () =>
            {
                IsShowProgressBarFound = true;
                ErrorStatus = null;

                var result = await _serviceFound.GetPerson(FoundPerson);

                if (_collectionRequest == null) CollectionRequest = new ObservableCollection<RequestFoundPerson>();
                if (result.Object != null) CollectionRequest.Add(result.Object);

                ErrorStatus = result.ErrorResult;

                IsShowProgressBarFound = false;
            }, () => _serviceFio.CheckFio(FoundPerson.Fio) && FoundPerson.Region != null));
        #endregion Command
      
        public RelayCommand<RequestFoundPerson> CommandGetFile =>
        _commandGetFile ?? (_commandGetFile = new RelayCommand<RequestFoundPerson>(
            req =>
            {
                _serviceFound.GetPersonFile(req.FileResult);
            }));
    }
}