using GalaSoft.MvvmLight;

namespace Fssp.ViewModel
{
    public class MainFsspViewModel : ViewModelBase
    {
        public MainFsspViewModel(FoundPersonFsspViewModel currentViewModel)
        {
            CurrentViewModel = currentViewModel;
        }

        #region PrivateField
        private ViewModelBase _currentViewModel;
        #endregion PrivateField

        #region PublicProperties
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set => Set(ref _currentViewModel, value);
        }
        #endregion PublicProperties
    }
}