using Microsoft.Extensions.DependencyInjection;
using RabbitCoopSimulation.Service;
using System;
using static RabbitCoopSimulation.ConsoleApp.Program;

namespace RabbitCoopSimulation.ConsoleApp
{
    public class ContanierIOC
    {

        public static ServiceProvider _serviceProvider;
        public static void DisposeServices()
        {
            if (_serviceProvider == null)
            {
                return;
            }
            if (_serviceProvider is IDisposable)
            {
                ((IDisposable)_serviceProvider).Dispose();
            }
        }
        public static void RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<ICoopSimulation, CoopSimulation>();
            services.AddSingleton<ConsoleApplication>();
            _serviceProvider = services.BuildServiceProvider(true);
        }
    }
}
