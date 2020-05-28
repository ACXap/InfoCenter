using Common;
using Common.Settings.Service;
using Fssp.Data;
using Fssp.Service;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;

namespace Fssp.ViewModel
{
    public class FoundListFsspViewModel: FoundViewModelBase
    {
        public FoundListFsspViewModel(IFoundPersonFsspService foundervice, ISettingsService settings)
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
        }

        #region PrivateField
        private readonly IFoundPersonFsspService _serviceFound;

        private ObservableCollection<RequestFoundPerson> _collectionRequest;

        private TypeData _typeData;

        private RelayCommand _commandStart;
        private RelayCommand<RequestFoundPerson> _commandGetFile;
        #endregion PrivateField

        #region PublicProperties
        public ObservableCollection<RequestFoundPerson> CollectionRequest
        {
            get => _collectionRequest;
            private set => Set(ref _collectionRequest, value);
        }

        public TypeData TypeData
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

                        if (result.Items != null)
                        {
                            if (_collectionRequest == null)
                            {
                                CollectionRequest = new ObservableCollection<RequestFoundPerson>(result.Items);
                            }
                            else
                            {
                                foreach (var item in result.Items)
                                {
                                    CollectionRequest.Add(item);
                                }
                            }
                        }

                        StopProcess(result.ErrorResult);
                    }, () => !string.IsNullOrEmpty(FoundHeader.FoundText) && TypeData != null && TypeData.Code == 1));

        public RelayCommand<RequestFoundPerson> CommandGetFile =>
        _commandGetFile ?? (_commandGetFile = new RelayCommand<RequestFoundPerson>(
            req =>
            {
                _serviceFound.GetPersonFile(req.FileResult);
            }));
        #endregion Command

        #region PrivateMethod
        private async void OpenFile()
        {
            var file = ServiceFile.GetFile();

            if(string.IsNullOrEmpty(file) == false)
            {
                FoundHeader.FoundText = file;
                TypeData = await ServiceFile.GetTypeData(file).ConfigureAwait(false);
            }
        }
        #endregion PrivateMethod
    }
}