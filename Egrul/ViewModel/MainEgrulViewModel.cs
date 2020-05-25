using GalaSoft.MvvmLight;

namespace Egrul.ViewModel
{
    public class MainEgrulViewModel : ViewModelBase
    {
        public MainEgrulViewModel(FoundCompanyEgrulViewModel currentView)
        {
            CurrentViewModel = currentView;
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