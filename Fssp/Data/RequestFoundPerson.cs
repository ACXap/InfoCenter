using GalaSoft.MvvmLight;

namespace Fssp.Data
{
    public class RequestFoundPerson : ViewModelBase
    {
        public RequestFoundPerson(IRequestQuery query)
        {
            Query = query;

            StatusRequest = new StatusRequest();
        }

        public void StartRequest()
        {
            StatusRequest.TypeStatusRequest = EnumTypeStatusRequest.InProgress;
        }

        public void StopRequest()
        {
            StatusRequest.TypeStatusRequest = EnumTypeStatusRequest.Ok;
        }

        public void ErrorRequest(string error)
        {
            if (string.IsNullOrEmpty(error)) return;

            StatusRequest.TypeStatusRequest = EnumTypeStatusRequest.Error;
            StatusRequest.Error = error;
        }

        private IRequestQuery _query;
        public IRequestQuery Query
        {
            get => _query;
            private set => Set(ref _query, value);
        }

        private StatusRequest _statusRequest;
        public StatusRequest StatusRequest
        {
            get => _statusRequest;
            private set => Set(ref _statusRequest, value);
        }

        private string _token = string.Empty;
        public string Token
        {
            get => _token;
            set => Set(ref _token, value);
        }

        private string _fileResult = string.Empty;
        public string FileResult
        {
            get => _fileResult;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    Set(ref _fileResult, value);
                }

                StopRequest();
            }
        }
    }
}