using System.Text.Json;
using System.Threading.Tasks;
using ApplicationSimpleSettings.Dao;
using ApplicationSimpleSettings.Model;

namespace ApplicationSimpleSettings
{
    public class SettingsService<T> where T: class, ISettings
    {
        private readonly ISettingsDao _settingsDao;

        public SettingsService( 
            ISettingsDao settingsDao)
        {
            _settingsDao = settingsDao;
        }
        
        public async Task<T?> GetSettings()
        {
            var typeName = typeof(T).FullName;

            if (typeName == null)
                return null;

            var applicationSettingsEntity = await _settingsDao.GetAsync(typeName);
            
            return applicationSettingsEntity != null 
                ? JsonSerializer.Deserialize<T>(applicationSettingsEntity.JsonValue) 
                : null;
        }
        
        public async Task<T> SaveSettings(T settingsEntity)
        {
            var value = JsonSerializer.Serialize(settingsEntity);

            var applicationSettingsEntity = new ApplicationSettingsEntry
            {
                TypeName = typeof(T).FullName ?? "",
                JsonValue = value
            };

            await _settingsDao.SaveAsync(applicationSettingsEntity);

            return settingsEntity;
        }
    }
}