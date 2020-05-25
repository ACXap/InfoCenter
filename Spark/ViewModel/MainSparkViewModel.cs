using GalaSoft.MvvmLight;

namespace Spark.ViewModel
{
    public class MainSparkViewModel : ViewModelBase
    {
        public MainSparkViewModel(FoundCompanySparkViewModel currentViewModel)
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