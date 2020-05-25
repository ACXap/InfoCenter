using GalaSoft.MvvmLight;

namespace Fssp.Data
{
    public class FoundPerson : ViewModelBase
    {
        private string _fio = string.Empty;
        private string _date = string.Empty;
        private Region _region;

        public string Fio
        {
            get => _fio;
            set => Set(ref _fio, value);
        }

        public string Date
        {
            get => _date;
            set => Set(ref _date, value);
        }

        public Region Region
        {
            get => _region;
            set => Set(ref _region, value);
        }
    }
}