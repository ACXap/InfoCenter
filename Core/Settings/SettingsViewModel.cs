using Core.Settings.Service;
using GalaSoft.MvvmLight;

namespace Core.Settings
{
    public class SettingsViewModel : ViewModelBase
    {
        public SettingsViewModel(ISettingsService settingsService)
        {
            _settingsSertvice = settingsService;
            Settings = _settingsSertvice.GetSettings();
        }

        #region PrivateField
        private readonly ISettingsService _settingsSertvice;

        private bool _isShowViewSettings = false;
        #endregion PrivateField

        #region PublicProperties
        public Data.Settings Settings { get; private set; }

        public bool IsShowViewSettings
        {
            get => _isShowViewSettings;
            set
            {
                Set(ref _isShowViewSettings, value);
                if (!value)
                {
                    _settingsSertvice.SaveSettings();
                }
            }
        }
        #endregion PublicProperties   
    }
}