using ControlzEx.Theming;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.Windows;

namespace Core.Settings.Data
{
    public class Settings : ViewModelBase
    {
        #region PrivateField
        private Theme _currentTheme;
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
            }
        }
        #endregion PublicProperties
    }
}