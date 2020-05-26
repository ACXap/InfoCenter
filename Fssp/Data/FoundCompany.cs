using GalaSoft.MvvmLight;
using Fssp.Data.Interface;

namespace Fssp.Data
{
    public class FoundCompany : ViewModelBase, IRequestQuery
    {
        public string FirstField { get => _name; }
        public string TowField { get => _address; }
        public string ThreeField { get => _region.Name; }

        private string _name = string.Empty;
        private string _address = string.Empty;
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