namespace Common.Settings.Repository
{
    public interface IRepositorySettings
    {
        EntitySettings LoadSettings();
        void SaveSettings(EntitySettings settings);
    }
}