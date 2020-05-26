using Common;
using Common.Settings.Service;
using Fssp.Data;
using Fssp.Service.Interface;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Fssp.ViewModel
{
    public class FoundNumberFsspViewModel : FoundViewModelBase
    {
        public FoundNumberFsspViewModel(IFoundPersonFsspService foundervice, ISettingsService settings)
        {
            TypeGrid = settings?.GetSettings().TypeGrid;

            _serviceFound = foundervice;

            FoundHeader = new Common.Data.FoundHeader()
            {
                CommandFound = new RelayCommand(() => Found(), () => !string.IsNullOrEmpty(FoundHeader.FoundText)),
                FoundFast = false,
                Header = "Поиск номера исполнительного производства",
                Watermark = "Номер исполнительного производства в формате n…n/yy/dd/rr или n…n/yy/ddddd-ИП"
            };
        }

        #region PrivateField
        private readonly IFoundPersonFsspService _serviceFound;

        private ObservableCollection<RequestFoundPerson> _collectionRequest;

        private RelayCommand<RequestFoundPerson> _commandGetFile;
        #endregion PrivateField

        #region PublicProperties
        public ObservableCollection<RequestFoundPerson> CollectionRequest
        {
            get => _collectionRequest;
            private set => Set(ref _collectionRequest, value);
        }

        #endregion PublicProperties

        #region Command
        public RelayCommand<RequestFoundPerson> CommandGetFile =>
        _commandGetFile ?? (_commandGetFile = new RelayCommand<RequestFoundPerson>(
            req =>
            {
                _serviceFound.GetPersonFile(req.FileResult);
            }));
        #endregion Command

        private async void Found()
        {
            IsShowProgressBarFound = true;
            ErrorStatus = null;

            var result = await _serviceFound.GetNumber(FoundHeader.FoundText);

            if (_collectionRequest == null) CollectionRequest = new ObservableCollection<RequestFoundPerson>();
            if (result.Object != null) CollectionRequest.Add(result.Object);

            ErrorStatus = result.ErrorResult;

            IsShowProgressBarFound = false;
        }
    }
}