using Common.Data.Enum;
using ControlzEx.Theming;
using System;
using System.Threading.Tasks;
using System.Windows;
using Common.Settings.Repository;

namespace Common.Settings.Service
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

        private Data.AppSettings _settings;

        #endregion PrivateField

        #region PrivateMethod
        private void LoadSettings()
        {
            try
            {
                var set = _repositorySettings.LoadSettings();

                _settings = new Data.AppSettings(this)
                {
                    CollectionTheme = ThemeManager.Current.Themes,
                    CurrentTheme = ThemeManager.Current.ChangeTheme(Application.Current, set.Theme),
                    ApiKeyFssp = set.ApiKeyFssp,
                    TypeGrid = new Common.Data.TypeGrid(this) { TypeGridViewItem = (EnumTypeGridViewItem)Enum.Parse(typeof(EnumTypeGridViewItem), set.TypeGrid) }
                };
            }
            catch (Exception ex)
            {
                _settings = new Data.AppSettings(this)
                {
                    CollectionTheme = ThemeManager.Current.Themes,
                    CurrentTheme = ThemeManager.Current.DetectTheme(),
                    TypeGrid = new Common.Data.TypeGrid(this)
                };

                // тут будет логер когда нибудь
            }
        }
        #endregion PrivateMethod

        #region PublicMethod
        public Data.AppSettings GetSettings()
        {
            if (_settings == null) LoadSettings();
            return _settings;
        }
        public void SaveSettings()
        {
            if (_settings == null) return;

            Task.Run(() =>
            {
                try
                {
                    EntitySettings set = new EntitySettings()
                    {
                        Theme = _settings.CurrentTheme.Name,
                        ApiKeyFssp = _settings.ApiKeyFssp,
                        TypeGrid = _settings.TypeGrid.TypeGridViewItem.ToString()
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