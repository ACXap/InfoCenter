using GalaSoft.MvvmLight;

namespace Fssp.Data
{
    public class RequestFoundPerson : ViewModelBase
    {
        public RequestFoundPerson(FoundPerson person)
        {
            FoundPerson = new FoundPerson()
            {
                Date = person.Date,
                Fio = person.Fio,
                Region = person.Region
            };

            StatusRequest = new StatusRequest();
        }

        public void StartRequest()
        {
            StatusRequest.TypeStatusRequest = Enum.EnumTypeStatusRequest.InProgress;
        }

        public void StopRequest()
        {
            StatusRequest.TypeStatusRequest = Enum.EnumTypeStatusRequest.Ok;
        }

        public void ErrorRequest(string error)
        {
            if (string.IsNullOrEmpty(error)) return;

            StatusRequest.TypeStatusRequest = Enum.EnumTypeStatusRequest.Error;
            StatusRequest.Error = error;
        }

        private FoundPerson _foundPerson;
        public FoundPerson FoundPerson
        {
            get => _foundPerson;
            private set => Set(ref _foundPerson, value);
        }

        private StatusRequest _statusRequest;
        public StatusRequest StatusRequest
        {
            get => _statusRequest;
            set => Set(ref _statusRequest, value);
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