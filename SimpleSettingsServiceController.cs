using System.Threading.Tasks;
using ApplicationSimpleSettings.Model;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationSimpleSettings
{
    [ApiController]
    public abstract class SimpleSettingsServiceController<T>: Controller where T: class, ISettings
    {
        private readonly SettingsService<T> _settingsService;

        protected SimpleSettingsServiceController(SettingsService<T> settingsService)
        {
            _settingsService = settingsService;
        }

        [HttpPost("get")]
        public virtual async Task<T?> Get()
        {
            var settings = await _settingsService.GetSettings();
            
            return settings;
        }

        [HttpPost("set")]
        public virtual async Task<T> Set(T settingsObject)
        {
            var saveSettings = await _settingsService.SaveSettings(settingsObject);

            return saveSettings;
        }
    }
}