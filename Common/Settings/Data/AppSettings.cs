using Common.Data;
using ControlzEx.Theming;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.Windows;
using Common.Settings.Service;

namespace Common.Settings.Data
{
    public class AppSettings : ViewModelBase
    {
        public AppSettings(ISettingsService service)
        {
            _service = service;
        }

        #region PrivateField
        private readonly ISettingsService _service;
        private Theme _currentTheme;
        private string _apiKeyFssp = string.Empty;
        private TypeGrid _typeGrid;

        private bool _canMemorizeMenu = false;
        #endregion PrivateField

        #region PublicProperties
        public ReadOnlyObservableCollection<Theme> CollectionTheme { get; set; }

        public Theme CurrentTheme
        {
            get => _currentTheme;
            set
            {
                Set(ref _currentTheme, value);
                ThemeManager.Current.ChangeTheme(Application.Current, value);

                if (_typeGrid != null)
                {
                    var old = _typeGrid.TypeGridViewItem;
                    _typeGrid.TypeGridViewItem = old == EnumTypeGridViewItem.Card ? EnumTypeGridViewItem.DataGrid : EnumTypeGridViewItem.Card;

                    _typeGrid.TypeGridViewItem = old;
                }

                _service?.SaveSettings();
            }
        }

        public TypeGrid TypeGrid
        {
            get => _typeGrid;
            set
            {
                Set(ref _typeGrid, value);
                _service?.SaveSettings();
            }
        }

        public string ApiKeyFssp
        {
            get => _apiKeyFssp;
            set
            {
                Set(ref _apiKeyFssp, value);
                _service?.SaveSettings();
            }
        }
       
        public bool CanMemorizeMenu
        {
            get => _canMemorizeMenu;
            set
            {
                Set(ref _canMemorizeMenu, value);
                _service?.SaveSettings();
            }
        }


        private int _lastMenu = 0;
        public int LastMenu
        {
            get => _lastMenu;
            set
            {
                Set(ref _lastMenu, value);
                _service?.SaveSettings();
            }
        }

        #endregion PublicProperties
    }
}