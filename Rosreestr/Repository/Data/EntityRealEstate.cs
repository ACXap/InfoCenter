using GalaSoft.MvvmLight;

namespace Rosreestr.Repository.Data
{
    public class EntityRealEstate:ViewModelBase
    {
        public string Id { get; set; }

        private IRealEstate _estate;
        public IRealEstate Estate
        {
            get => _estate;
            set => Set(ref _estate, value);
        }
    }
}