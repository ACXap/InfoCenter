using Fssp.Service;
using Fssp.ViewModel;
using GalaSoft.MvvmLight;

namespace Fssp.Data.DesignTime
{
    public class MainViewModelDesignTime:ViewModelBase
    {
        public MainViewModelDesignTime()
        {
            CurrentViewModel = new FoundPersonFsspViewModel(null, null);
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