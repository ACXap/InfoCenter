using ControlzEx.Theming;
using Core.Settings.Repository;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace Core.Settings.Service
{
    public class SettingsService : ISettingsService
    {
        public SettingsService(IRepositorySettings repositorySettings)
        {
            _repositorySettings = repositorySettings;

            LoadSettings();
        }

        #region PrivateField
        private readonly IRepositorySettings _repositorySettings;
        private readonly object _lock = new object();

        private Data.Settings _settings;

        #endregion PrivateField

        #region PrivateMethod
        private void LoadSettings()
        {
            _settings = new Data.Settings()
            {
                CollectionTheme = ThemeManager.Current.Themes,
                CurrentTheme = ThemeManager.Current.DetectTheme()
            };

            try
            {
                var set = _repositorySettings.LoadSettings();

                _settings.CurrentTheme = ThemeManager.Current.ChangeTheme(Application.Current, set.Theme);
            }
            catch (Exception ex)
            {
                // тут будет логер когда нибудь
            }
        }
        #endregion PrivateMethod

        #region PublicMethod
        public Data.Settings GetSettings()
        {
            if (_settings == null) LoadSettings();
            return _settings;
        }
        public void SaveSettings()
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    EntitySettings set = new EntitySettings()
                    {
                        Theme = _settings.CurrentTheme.Name
                    };
                    lock (_lock)
                    {
                        _repositorySettings.SaveSettings(set);
                    }
                }
                catch (Exception ex)
                {
                    // тут будет логер когда нибудь
                }
            });
        }
        #endregion PublicMethod
    }
}