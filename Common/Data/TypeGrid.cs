using Common.Settings.Service;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace Common.Data
{
    public class TypeGrid : ViewModelBase
    {
        public TypeGrid(ISettingsService settings)
        {
            _settingsService = settings;
        }
        private ISettingsService _settingsService;
        private EnumTypeGridViewItem _typeGridViewItem;
        public EnumTypeGridViewItem TypeGridViewItem
        {
            get => _typeGridViewItem;
            set => Set(ref _typeGridViewItem, value);
        }

        private RelayCommand<EnumTypeGridViewItem> _commandChangeType;
        public RelayCommand<EnumTypeGridViewItem> CommandChangeType =>
        _commandChangeType ?? (_commandChangeType = new RelayCommand<EnumTypeGridViewItem>(
            type =>
            {
                TypeGridViewItem = type;
                _settingsService?.SaveSettings();
            }));
    }
}