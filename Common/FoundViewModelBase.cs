using Common.Data;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace Common
{
    public class FoundViewModelBase : ViewModelBase
    {
        private string _header = string.Empty;
        private ErrorResult _errorStatus;
        private TypeGrid _typeGrid;
        private bool _isShowProgressBarFound = false;

        public ErrorResult ErrorStatus
        {
            get => _errorStatus;
            set => Set(ref _errorStatus, value);
        }
        public TypeGrid TypeGrid
        {
            get => _typeGrid;
            set => Set(ref _typeGrid, value);
        }
        public bool IsShowProgressBarFound
        {
            get => _isShowProgressBarFound;
            set => Set(ref _isShowProgressBarFound, value);
        }
        public string Header
        {
            get => _header;
            set => Set(ref _header, value);
        }


        private RelayCommand _commandCloseErrorView;
        public RelayCommand CommandCloseErrorView =>
        _commandCloseErrorView ?? (_commandCloseErrorView = new RelayCommand(
                    () =>
                    {
                        ErrorStatus = null;
                    }));

        public FoundHeader FoundHeader { get; set; }
    }
}