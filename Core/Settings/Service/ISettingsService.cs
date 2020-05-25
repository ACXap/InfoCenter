namespace Core.Settings.Service
{
    public interface ISettingsService
    {
        Data.Settings GetSettings();
        void SaveSettings();
    }

}