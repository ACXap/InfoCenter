using Egrul.ViewModel;
using GalaSoft.MvvmLight;

namespace Egrul.Data.DesignTime
{
    public class MainViewModelDesignTime : ViewModelBase
    {
        #region PrivateField
        private ViewModelBase _currentViewModel = new FoundCompanyEgrulViewModel(null, null);
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
