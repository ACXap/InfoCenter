using Common;
using Common.Service;
using Common.Settings.Service;
using Fssp.Data;
using Fssp.Service;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;

namespace Fssp.ViewModel
{
    public class FoundListFsspViewModel: FoundViewModelBase
    {
        public FoundListFsspViewModel(IFoundFsspService foundervice, ISettingsService settings)
        {
            TypeGrid = settings?.GetSettings().TypeGrid;

            _serviceFound = foundervice;

            FoundHeader = new Common.Data.FoundHeader()
            {
                CommandFound = new RelayCommand(() => OpenFile()),
                FoundFast = false,
                Header = "Обработка списков",
                Watermark = "Выбрать файл с данными для обработки"
            };

            CollectionRequest = _serviceFound?.CollectionRequest;
        }

        #region PrivateField
        private readonly IFoundFsspService _serviceFound;

        private ObservableCollection<RequestFound> _collectionRequest;

        private readonly ServiceFile<TypeDataFssp> _serviceFile = new ServiceFile<TypeDataFssp>();
        private TypeDataFssp _typeData;

        private RelayCommand _commandStart;
        private RelayCommand<RequestFound> _commandGetFile;
        #endregion PrivateField

        #region PublicProperties
        public ObservableCollection<RequestFound> CollectionRequest
        {
            get => _collectionRequest;
            private set => Set(ref _collectionRequest, value);
        }

        public TypeDataFssp TypeData
        {
            get => _typeData;
            set => Set(ref _typeData, value);
        }
        #endregion PublicProperties

        #region Command
        public RelayCommand CommandStart =>
        _commandStart ?? (_commandStart = new RelayCommand(
                    async () =>
                    {
                        var file = FoundHeader.FoundText;
                        FoundHeader.FoundText = "";
                        StartProcess();

                        var result = await _serviceFound.ProcessingList(file).ConfigureAwait(true);

                        StopProcess();
                    }, () => !string.IsNullOrEmpty(FoundHeader.FoundText) && TypeData != null && TypeData.Code == 1));

        public RelayCommand<RequestFound> CommandGetFile =>
        _commandGetFile ?? (_commandGetFile = new RelayCommand<RequestFound>(
            req =>
            {
                _serviceFound.GetPersonFile(req.FileResult);
            }));
        #endregion Command

        #region PrivateMethod
        private async void OpenFile()
        {
            var file = _serviceFile.GetFile();

            if(string.IsNullOrEmpty(file) == false)
            {
                FoundHeader.FoundText = file;
                TypeData = await _serviceFile.GetTypeData(file).ConfigureAwait(false);
            }
        }
        #endregion PrivateMethod
    }
}