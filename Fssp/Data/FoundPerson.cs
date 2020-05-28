using GalaSoft.MvvmLight;

namespace Fssp.Data
{
    public class FoundPerson : ViewModelBase, IRequestQuery
    {
        public string FirstField { get =>_fio; }
        public string TowField { get =>_date; }
        public string ThreeField { get =>_region.Name; }


        private string _fio;
        private string _date;
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