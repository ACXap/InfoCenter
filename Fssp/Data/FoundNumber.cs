using GalaSoft.MvvmLight;
using Fssp.Data.Interface;

namespace Fssp.Data
{
    public class FoundNumber : ViewModelBase, IRequestQuery
    {
        public string FirstField { get => _number; }
        public string TowField { get; }
        public string ThreeField { get; }

        private string _number = string.Empty;

        public string Number
        {
            get => _number;
            set => Set(ref _number, value);
        }
    }
}