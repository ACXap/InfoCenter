using Common;
using Common.Service;
using GalaSoft.MvvmLight.CommandWpf;
using Ifns.Data;
using Ifns.Service;
using System.Collections.ObjectModel;

namespace Ifns.ViewModel
{
    public class FoundListIfnsViewModel : FoundViewModelBase
    {
        public FoundListIfnsViewModel(IFoundIfnsService foundService)
        {
            _foundService = foundService;

            FoundHeader = new Common.Data.FoundHeader()
            {
                CommandFound = new RelayCommand(() => OpenFile()),
                FoundFast = false,
                Header = "Обработка списков",
                Watermark = "Выбрать файл с данными для обработки",
            };
            CollectionIfns = new ReadOnlyObservableCollection<EntityIfns>(_foundService.CollectionIfns);
        }
     
        #region PrivateField
        private readonly IFoundIfnsService _foundService;
        private ServiceFile<TypeDataIfns> _serviceFile = new ServiceFile<TypeDataIfns>();

        private TypeDataIfns _typeData;

        private RelayCommand _commandStart;
        #endregion PrivateField

        #region PublicProperties
        public ReadOnlyObservableCollection<EntityIfns> CollectionIfns { get; }
        public TypeDataIfns TypeData
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

              var result = await _foundService.ProcessList(file).ConfigureAwait(true);

              StopProcess(result.ErrorResult);
          }, () => !string.IsNullOrEmpty(FoundHeader.FoundText) && TypeData != null && TypeData.Code == 1));
        #endregion Command

        #region PrivateMethod
        private async void OpenFile()
        {
            var file = _serviceFile.GetFile();

            if (string.IsNullOrEmpty(file) == false)
            {
                FoundHeader.FoundText = file;
                TypeData = await _serviceFile.GetTypeData(file);
                CommandStart.RaiseCanExecuteChanged();
            }
        }
        #endregion PrivateMethod
    }
}