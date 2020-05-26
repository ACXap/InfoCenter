using GalaSoft.MvvmLight;

namespace Fssp.ViewModel
{
    public class MainFsspViewModel : ViewModelBase
    {
        public MainFsspViewModel(FoundPersonFsspViewModel foundPerson, 
                                FoundCompanyFsspViewModel foundCompany,
                                FoundNumberFsspViewModel foundNumber)
        {
            Header = "Поиск в базе данных ФССП";
            _foundPerson = foundPerson;
            _foundCompany = foundCompany;
            _foundNumber = foundNumber;

            CurrentViewModel = foundPerson;
        }

        #region PrivateField
        private ViewModelBase _currentViewModel;

        private readonly ViewModelBase _foundPerson;
        private readonly ViewModelBase _foundCompany;
        private readonly ViewModelBase _foundNumber;
        private readonly ViewModelBase _foundList;

        private int _currentIndexTab;
        private string _header = string.Empty;
        #endregion PrivateField

        #region PublicProperties
        public ViewModelBase CurrentViewModel
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
                        CurrentViewModel = _foundPerson;
                        break;
                    case 1:
                        CurrentViewModel = _foundCompany;
                        break;
                    case 2:
                        CurrentViewModel = _foundNumber;
                        break;
                    case 3:
                        CurrentViewModel = _foundList;
                        break;
                    default:
                        CurrentViewModel = _foundPerson;
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