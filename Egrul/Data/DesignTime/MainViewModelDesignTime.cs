using Egrul.Service;
using Egrul.ViewModel;
using GalaSoft.MvvmLight;

namespace Egrul.Data.DesignTime
{
    public class MainViewModelDesignTime : ViewModelBase
    {
        public MainViewModelDesignTime()
        {
            CurrentViewModel = new FoundCompanyEgrulViewModel(null,null);
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
