using System.Threading.Tasks;
using ApplicationSimpleSettings.Model;
using Microsoft.EntityFrameworkCore;

namespace ApplicationSimpleSettings.Dao
{
    public class SettingsDao: ISettingsDao
    {
        private readonly SettingsDbContext _settingsDbContext;

        public SettingsDao(SettingsDbContext settingsDbContext)
        {
            _settingsDbContext = settingsDbContext;
        }

        public async Task<ApplicationSettingsEntry?> GetAsync(string typeName)
        {
            return await _settingsDbContext.ApplicationSettingsEntries.FirstOrDefaultAsync(settings => settings.TypeName == typeName);
        }

        public async Task SaveAsync(ApplicationSettingsEntry applicationSettingsEntry)
        {
            if (string.IsNullOrEmpty(applicationSettingsEntry.TypeName))
                return;

            var settingsEntity = await GetAsync(applicationSettingsEntry.TypeName);

            if (settingsEntity == null)
            {
                _settingsDbContext.Add(applicationSettingsEntry);
            }
            else
            {
                settingsEntity.JsonValue = applicationSettingsEntry.JsonValue;
                _settingsDbContext.Update(settingsEntity);
            }

            await _settingsDbContext.SaveChangesAsync();
        }
    }
}