using Common.Settings;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MahApps.Metro.Controls;
using MahApps.Metro.IconPacks;
using UI.About;
using UI.Data;
using UI.Home;

namespace UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            CreateMenuItems();
            LoadPlugin();
        }

        #region PrivateField
        private readonly PluginRegister _pluginRegister = new PluginRegister();

        private HamburgerMenuItemCollection _menuItems;
        private HamburgerMenuItemCollection _menuOptionItems;

        private int _indexItem = 0;
        private RelayCommand<EntityPlugin> _commandOpenMenu;
        #endregion PrivateField

        #region PublicProperties
        public HamburgerMenuItemCollection MenuItems
        {
            get { return _menuItems; }
            private set
            {
                if (Equals(value, _menuItems)) return;
                Set(ref _menuItems, value);
            }
        }
        public HamburgerMenuItemCollection MenuOptionItems
        {
            get { return _menuOptionItems; }
            private set
            {
                if (Equals(value, _menuOptionItems)) return;
                Set(ref _menuOptionItems, value);
            }
        }
        public int IndexItem
        {
            get => _indexItem;
            set => Set(ref _indexItem, value);
        }
        #endregion PublicProperties

        #region Command
        public RelayCommand<EntityPlugin> CommandOpenMenu =>
        _commandOpenMenu ?? (_commandOpenMenu = new RelayCommand<EntityPlugin>(
            plugin =>
            {
                IndexItem = plugin.Id;
            }));
        #endregion Command

        #region PrivateMethod
        private void CreateMenuItems()
        {
            MenuItems = new HamburgerMenuItemCollection
            {
                new HamburgerMenuIconItem()
                {
                    Icon = new PackIconFontAwesome() {Kind = PackIconFontAwesomeKind.HomeSolid},
                    Label = "Начальная страница",
                    ToolTip = "Начальная страница.",
                    Tag = new HomeView()
                }
            };

            MenuOptionItems = new HamburgerMenuItemCollection
            {
                new HamburgerMenuIconItem()
                {
                    Icon = new PackIconFontAwesome() {Kind = PackIconFontAwesomeKind.CogSolid},
                    Label = "Настройки",
                    ToolTip = "Настройки программы.",
                    Tag = new SettingsView()
                },
                new HamburgerMenuIconItem()
                {
                    Icon = new PackIconFontAwesome() {Kind = PackIconFontAwesomeKind.QuestionSolid},
                    Label = "О программе",
                    ToolTip = "Информация о программе.",
                    Tag =  new AboutView()
                }
            };
        }
        private void LoadPlugin()
        {
            _pluginRegister.GetPlugin().ForEach(item =>
            {
                MenuItems.Add(new HamburgerMenuIconItem()
                {
                    Icon = item.Icon,
                    Label = item.Label,
                    ToolTip = item.ToolTip,
                    Tag = item.Tag
                });
            });
        }
        #endregion PrivateMethod
    }
}