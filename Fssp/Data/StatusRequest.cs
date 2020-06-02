using GalaSoft.MvvmLight;

namespace Fssp.Data
{
    public class StatusRequest : ViewModelBase
    {
        private string _error = string.Empty;
        public string Error
        {
            get => _error;
            set => Set(ref _error, value);
        }

        private bool _isInProgress;
        public bool IsInProgress
        {
            get => _isInProgress;
            private set => Set(ref _isInProgress, value);
        }

        private string _status = string.Empty;
        public string Status
        {
            get => _status;
            private set => Set(ref _status, value);
        }

        private string _progress = string.Empty;
        public string Progress
        {
            get => _progress;
            set
            {
                var v = value.Replace("of", "из");
                Set(ref _progress, v);
            }
        }

        private EnumTypeStatusRequest _typeStatusRequest;
        public EnumTypeStatusRequest TypeStatusRequest
        {
            get => _typeStatusRequest;
            set
            {
                Set(ref _typeStatusRequest, value);
                switch (value)
                {
                    case EnumTypeStatusRequest.Ok:
                        IsInProgress = false;
                        Status = "Готово";
                        break;
                    case EnumTypeStatusRequest.InProgress:
                        IsInProgress = true;
                        Status = "Загрузка данных...";
                        break;
                    case EnumTypeStatusRequest.Error:
                        IsInProgress = false;
                        Status = "Ошибка";
                        break;
                    default:
                        break;
                }
            }
        }
    }
}