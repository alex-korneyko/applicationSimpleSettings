using System.Threading.Tasks;
using ApplicationSimpleSettings.Model;

namespace ApplicationSimpleSettings.Dao
{
    public interface ISettingsDao
    {
        Task<ApplicationSettingsEntry?> GetAsync(string typeName);
        Task SaveAsync(ApplicationSettingsEntry applicationSettingsEntry);
    }
}