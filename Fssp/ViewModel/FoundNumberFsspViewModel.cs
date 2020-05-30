using Common;
using Common.Settings.Service;
using Fssp.Data;
using Fssp.Service;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;

namespace Fssp.ViewModel
{
    public class FoundNumberFsspViewModel : FoundViewModelBase
    {
        public FoundNumberFsspViewModel(IFoundFsspService foundService, ISettingsService settings)
        {
            TypeGrid = settings?.GetSettings().TypeGrid;

            _serviceFound = foundService;

            FoundHeader = new Common.Data.FoundHeader()
            {
                CommandFound = new RelayCommand(() => Found(), () => !string.IsNullOrEmpty(FoundHeader.FoundText)),
                FoundFast = false,
                Header = "Поиск номера исполнительного производства",
                Watermark = "Номер исполнительного производства в формате n…n/yy/dd/rr или n…n/yy/ddddd-ИП"
            };

            CollectionRequest = _serviceFound?.CollectionRequest;
        }

        #region PrivateField
        private readonly IFoundFsspService _serviceFound;

        private ObservableCollection<RequestFound> _collectionRequest;

        private RelayCommand<RequestFound> _commandGetFile;
        #endregion PrivateField

        #region PublicProperties
        public ObservableCollection<RequestFound> CollectionRequest
        {
            get => _collectionRequest;
            private set => Set(ref _collectionRequest, value);
        }

        #endregion PublicProperties

        #region Command
        public RelayCommand<RequestFound> CommandGetFile =>
        _commandGetFile ?? (_commandGetFile = new RelayCommand<RequestFound>(
            req =>
            {
                _serviceFound.GetPersonFile(req.FileResult);
            }));
        #endregion Command

        private async void Found()
        {
            StartProcess();

            await _serviceFound.GetNumber(FoundHeader.FoundText).ConfigureAwait(false);

            StopProcess();
        }
    }
}