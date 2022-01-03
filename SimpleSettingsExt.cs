using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using ApplicationSimpleSettings.Dao;
using ApplicationSimpleSettings.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ApplicationSimpleSettings
{
    public static class SimpleSettingsExt
    {
        public static void AddSimpleSettingsService(this IServiceCollection serviceCollection, Action<SettingsServiceOptions> options)
        {
            var settingsServiceOptions = new SettingsServiceOptions();
            options(settingsServiceOptions);
            serviceCollection.AddSingleton(settingsServiceOptions);
            
            serviceCollection.AddHostedService<AutoMigrator>();

            serviceCollection.AddDbContext<SettingsDbContext>();

            serviceCollection.AddScoped<ISettingsDao, SettingsDao>();

            var settingsTypes = new List<TypeInfo>();

            Assembly.GetEntryAssembly()?.DefinedTypes
                .Where(typeInfo => typeInfo.ImplementedInterfaces.Contains(typeof(ISettings)))
                .ToList()
                .ForEach(settingsType => settingsTypes.Add(settingsType));

            settingsTypes.ForEach(settings =>
            {
                var settingsServiceType = typeof(SettingsService<>);
                var genericSettingsServiceType = settingsServiceType.MakeGenericType(settings);

                serviceCollection.AddScoped(genericSettingsServiceType);
            });
        }
    }

    public class SettingsServiceOptions
    {
        public string EnvironmentVariableWithConnectionString { get; set; } = "";
    }

    public class AutoMigrator : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public AutoMigrator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var serviceScope = _serviceProvider.CreateScope();
            var settingsDbContext = serviceScope.ServiceProvider.GetService<SettingsDbContext>();

            settingsDbContext?.Database.Migrate();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}