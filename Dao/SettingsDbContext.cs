using ApplicationSimpleSettings.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ApplicationSimpleSettings.Dao
{
    public class SettingsDbContext: DbContext
    {
        private readonly SettingsServiceOptions _settingsServiceOptions = null!;
        private readonly IConfiguration _configuration = null!;

        public SettingsDbContext(DbContextOptions<SettingsDbContext> options): base(options)
        {
        }

        public SettingsDbContext(
            DbContextOptions<SettingsDbContext> options, 
            SettingsServiceOptions settingsServiceOptions, 
            IConfiguration configuration) : base(options)
        {
            _settingsServiceOptions = settingsServiceOptions;
            _configuration = configuration;
        }

        public DbSet<ApplicationSettingsEntry> ApplicationSettingsEntries { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                _configuration[_settingsServiceOptions.EnvironmentVariableWithConnectionString], builder =>
                {
                    builder.MigrationsAssembly($"ApplicationSimpleSettings");
                });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationSettingsEntry>()
                .HasKey(entity => entity.TypeName);
        }
    }
}