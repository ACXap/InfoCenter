using Common.Settings.Repository;
using Newtonsoft.Json;
using System.IO;

namespace Common.Settings.Repository
{
    public class RepositoryJsonSettings : IRepositorySettings
    {
        #region PrivateField
        private readonly string _fileSettings = "settings.ini";
        #endregion PrivateField

        #region PublicMethod
        public EntitySettings LoadSettings()
        {
            if (File.Exists(_fileSettings))
            {
                var str = File.ReadAllText(_fileSettings);

                return JsonConvert.DeserializeObject<EntitySettings>(str);
            }
            else
            {
                throw new FileNotFoundException("Файл настроек не найден", _fileSettings);
            }
        }

        public void SaveSettings(EntitySettings settings)
        {
            var str = JsonConvert.SerializeObject(settings);
            File.WriteAllText(_fileSettings, str);
        }
        #endregion PublicMethod
    }
}