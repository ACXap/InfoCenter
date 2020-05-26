using Common.Settings.Repository;
using Common.Settings.Service;
using GalaSoft.MvvmLight;
using Spark.Repository;
using Spark.Service;
using Spark.ViewModel;

namespace Spark.Data.DesignTime
{
    public class MainViewModelDesignTime : ViewModelBase
    {
        public MainViewModelDesignTime()
        {
            CurrentViewModel = new FoundCompanySparkViewModel(new FoundCompanyService(new RepositorySparkSite()), new SettingsService(new RepositoryJsonSettings()));
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
