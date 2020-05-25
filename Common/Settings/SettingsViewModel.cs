using GalaSoft.MvvmLight;
using Common.Settings.Service;

namespace Common.Settings
{
    public class SettingsViewModel : ViewModelBase
    {
        public SettingsViewModel(ISettingsService settingsService)
        {
            _settingsSertvice = settingsService;
            Settings = _settingsSertvice?.GetSettings();
        }

        #region PrivateField
        public Data.AppSettings Settings { get; private set; }
        private readonly ISettingsService _settingsSertvice;

        #endregion PrivateField

        #region PublicProperties

        #endregion PublicProperties

        #region PrivateMethod

        #endregion PrivateMethod
    }
}