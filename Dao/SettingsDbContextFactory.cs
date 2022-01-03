using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ApplicationSimpleSettings.Dao
{
    public class SettingsDbContextFactory: IDesignTimeDbContextFactory<SettingsDbContext>
    {
        
        public SettingsDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SettingsDbContext>();
            optionsBuilder.UseSqlServer(args[0], builder =>
            {
                builder.MigrationsAssembly($"ApplicationSimpleSettings");
            });

            return new SettingsDbContext(optionsBuilder.Options);
        }
    }
}