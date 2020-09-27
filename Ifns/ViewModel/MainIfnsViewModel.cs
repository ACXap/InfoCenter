using Common;
using GalaSoft.MvvmLight;

namespace Ifns.ViewModel
{
    public class MainIfnsViewModel : ViewModelBase
    {
        public MainIfnsViewModel(FoundListIfnsViewModel foundList, FoundAllIfnsViewModel foundAll)
        {
            Header = "Определение реквизитов ИФНС";
            _foundAll = foundAll;
            _foundList = foundList;

            CurrentViewModel = _foundAll;
        }

        #region PrivateField
        private FoundViewModelBase _currentViewModel;
        private readonly FoundViewModelBase _foundList;
        private readonly FoundViewModelBase _foundAll;

        private int _currentIndexTab;
        private string _header = string.Empty;
        #endregion PrivateField

        #region PublicProperties
        public FoundViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            private set => Set(ref _currentViewModel, value);
        }

        public int CurrentIndexTab
        {
            get => _currentIndexTab;
            set
            {
                Set(ref _currentIndexTab, value);
                switch (value)
                {
                    case 0:
                        CurrentViewModel = _foundAll;
                        break;
                    case 1:
                        CurrentViewModel = _foundList;
                        break;
                    default:
                        CurrentViewModel = _foundAll;
                        break;
                }
            }
        }

        public string Header
        {
            get => _header;
            private set => Set(ref _header, value);
        }
        #endregion PublicProperties
    }
}