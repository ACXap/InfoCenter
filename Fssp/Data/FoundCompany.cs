using GalaSoft.MvvmLight;

namespace Fssp.Data
{
    public class FoundCompany : ViewModelBase, IRequestQuery
    {
        public string FirstField { get => _name; }
        public string TowField { get => _address; }
        public string ThreeField { get => _region.Name; }

        private string _name;
        private string _address;
        private Region _region;

        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        public string Address
        {
            get => _address;
            set => Set(ref _address, value);
        }

        public Region Region
        {
            get => _region;
            set => Set(ref _region, value);
        }
    }
}