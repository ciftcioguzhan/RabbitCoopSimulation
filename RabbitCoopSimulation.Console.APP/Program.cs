using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitCoopSimulation.Service;
using RabbitCoopSimulation.Service.Abstract;
using RabbitCoopSimulation.Service.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

                int graveyardCount = _coopSimulation.Graveyard.Count;
                int populationCount = _coopSimulation.Population.Count;
                int femaleRabbitCount = _coopSimulation.Population.Where(x=> x is IFemaleFowl).ToList().Count;
                int maleRabitCount = _coopSimulation.Population.Where(x=> x is IFemaleFowl).ToList().Count;
                string time = _coopSimulation.Time.ToShortDateString();

                Console.WriteLine("Zaman={0} - Popülasyon  = {1} -Erkek Tavşan Sayısı = {2} -Dişi Tavşan Sayısı = {3} - Ölü Tavşan Sayısı  = {4}", time, populationCount,femaleRabbitCount,maleRabitCount,graveyardCount);
                Console.Read();
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
