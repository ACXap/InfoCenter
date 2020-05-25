using GalaSoft.MvvmLight;
using Common.Settings.Service;
using Common.Settings.Repository;

namespace Common.Settings.Data.DesignTime
{
    public class SettingsViewModelDesignTime : ViewModelBase
    {
        public SettingsViewModelDesignTime()
        {
            _settingsSertvice = new Service.SettingsService(new RepositoryJsonSettings());
            Settings = _settingsSertvice.GetSettings();
        }

        #region PrivateField
        private readonly ISettingsService _settingsSertvice;
        #endregion PrivateField

        #region PublicProperties
        public Data.AppSettings Settings { get; private set; }
        #endregion PublicProperties   
    }
}