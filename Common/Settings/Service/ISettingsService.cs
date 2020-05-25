namespace Common.Settings.Service
{
    public interface ISettingsService
    {
        Data.AppSettings GetSettings();
        void SaveSettings();
    }
}