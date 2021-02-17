using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitCoopSimulation.Service;
using RabbitCoopSimulation.Service.Abstract;
using RabbitCoopSimulation.Service.Concrete;
using System;
using System.Collections.Generic;
using System.IO;

namespace RabbitCoopSimulation.ConsoleApp
{
    public class Program
    {
        public class ConsoleApplication
        {
            private readonly ICoopSimulation _coopSimulation;
            public DateTime StartDate { get; set; } = new DateTime(2021, 01, 01);
            public int SimulationCyclesInMonth { get; set; }
            public DateTime EndDate { get; set; }
            public ConsoleApplication(ICoopSimulation coopSimulation)
            {
                IConfiguration config = new ConfigurationBuilder().SetBasePath(Path.Combine(AppContext.BaseDirectory))
               .AddJsonFile("appsettings.json", true, true)
               .Build();
              
                SimulationCyclesInMonth = Convert.ToInt32(config.GetSection("SimulationCyclesInMonth").Value);
                _coopSimulation = coopSimulation;
                _coopSimulation.Time = StartDate;
                _coopSimulation.Population = new HashSet<IFowl>()
                {
                    new FemaleRabbit(_coopSimulation) { BirthDate = StartDate.AddYears(-1)},
                    new MaleRabbit(_coopSimulation) { BirthDate = StartDate.AddYears(-1)},
                };

                EndDate = StartDate.AddMonths(SimulationCyclesInMonth);
            }

            public void Run()
            {
                while (_coopSimulation.Time < EndDate)
                {
                    _coopSimulation.NextCycle();
                }
            }
        }

        static void Main(string[] args)
        {
            ContanierIOC.RegisterServices();
            IServiceScope scope = ContanierIOC._serviceProvider.CreateScope();
            scope.ServiceProvider.GetRequiredService<ConsoleApplication>().Run();
            ContanierIOC.DisposeServices();
        }

    }
}
