using GalaSoft.MvvmLight;
using Spark.Service;
using Spark.ViewModel;

namespace Spark.Data.DesignTime
{
    public class MainViewModelDesignTime : ViewModelBase
    {
        public MainViewModelDesignTime()
        {
            CurrentViewModel = new FoundCompanySparkViewModel(null, null);
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
