using Common;
using GalaSoft.MvvmLight;

namespace Rosreestr.ViewModel
{
    public class MainRosreestrViewModel : ViewModelBase
    {
        public MainRosreestrViewModel(FoundNumberRosreestrViewModel foundNumber,
                                FoundListRosreestrViewModel foundList)
        {
            Header = "Справочная информация по объектам недвижимости";
            _foundNumber = foundNumber;
            _foundList = foundList;

            CurrentViewModel = foundNumber;
        }

        #region PrivateField
        private FoundViewModelBase _currentViewModel;
        private readonly FoundViewModelBase _foundNumber;
        private readonly FoundViewModelBase _foundList;

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
                        CurrentViewModel = _foundNumber;
                        break;
                    case 1:
                        CurrentViewModel = _foundList;
                        break;
                    default:
                        CurrentViewModel = _foundNumber;
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