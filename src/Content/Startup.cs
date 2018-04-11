using System;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Company.ConsoleApplication1
{
    internal static class Startup
    {
        public static IServiceProvider CreateServiceProvider()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            ConfigureOptions(services);

            var builder = new ContainerBuilder();
            builder.Populate(services);

            builder.RegisterAssemblyTypes(typeof(Startup).GetTypeInfo().Assembly)
                .AssignableTo<ICommand>()
                .AsImplementedInterfaces()
                .InstancePerDependency();

            var container = builder.Build();
            var serviceProvider = container.Resolve<IServiceProvider>();
            ConfigureLogging(serviceProvider.GetRequiredService<ILoggerFactory>());
            return serviceProvider;
        }
        
        private static void ConfigureLogging(ILoggerFactory loggerFactory)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.LiterateConsole(LogEventLevel.Information)
                .CreateLogger();
            loggerFactory.AddSerilog();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddLogging();
            services.AddTransient<Application>();
        }

        private static void ConfigureOptions(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory))
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            
            var interfaceType = typeof(IOptionsConfigurer);
            foreach(var optionsConfigurerType in typeof(Startup).GetTypeInfo().Assembly
                .GetTypes().Where(t => t.IsClass && interfaceType.IsAssignableFrom(t)))
            {
                var configurer = Activator.CreateInstance(optionsConfigurerType) as IOptionsConfigurer;
                configurer?.Configure(services, configuration);
            }
        }
    }
}