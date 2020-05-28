using GalaSoft.MvvmLight;

namespace Fssp.Data
{
    public class FoundNumber : ViewModelBase, IRequestQuery
    {
        public string FirstField { get => _number; }
        public string TowField => null;
        public string ThreeField => null;

        private string _number;

        public string Number
        {
            get => _number;
            set => Set(ref _number, value);
        }
    }
}