using Common.Data;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace Common
{
    public class FoundViewModelBase : ViewModelBase
    {
        #region PrivateField
        private string _header = string.Empty;
        private ErrorResult _errorStatus;
        private TypeGrid _typeGrid;
        private bool _isShowProgressBarFound = false;

        private RelayCommand _commandCloseErrorView;
        #endregion PrivateField

        #region PublicProperties
        public FoundHeader FoundHeader { get; set; }

        public ErrorResult ErrorStatus
        {
            get => _errorStatus;
            private set => Set(ref _errorStatus, value);
        }
        public TypeGrid TypeGrid
        {
            get => _typeGrid;
            set => Set(ref _typeGrid, value);
        }
        public bool IsShowProgressBarFound
        {
            get => _isShowProgressBarFound;
            private set => Set(ref _isShowProgressBarFound, value);
        }
        public string Header
        {
            get => _header;
            set => Set(ref _header, value);
        }
        #endregion PublicProperties

        #region Command
        public RelayCommand CommandCloseErrorView =>
        _commandCloseErrorView ?? (_commandCloseErrorView = new RelayCommand(
            () =>
            {
                ErrorStatus = null;
            }));
        #endregion Command
    
        public void StartProcess()
        {
            ErrorStatus = null;
            IsShowProgressBarFound = true;
        }

        public void StopProcess(ErrorResult error = null)
        {
            ErrorStatus = error;
            IsShowProgressBarFound = false;
        }
    }
}