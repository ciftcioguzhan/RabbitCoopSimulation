using RabbitCoopSimulation.Service.Abstract;
using System;
using System.Collections.Generic;

namespace RabbitCoopSimulation.Service
{
    public delegate void MatingNotify(List<IFowl> fowl); 
    public delegate void BirthNotify(List<IFemaleFowl> fowl);
    public delegate void DeathNotify(List<IFowl> fowl);

    public interface ICoopSimulation
    {
        event MatingNotify CouplingNotify;
        event BirthNotify BirthNotify;
        event DeathNotify DeathNotify;

        HashSet<IFowl> Population { get; set; }
        HashSet<IFowl> Graveyard { get; set; } 

        public void NextCycle();
        public void MatingTime();
        public void BirthTime();
        public void DeathTime();
        public DateTime Time { get; set; }
        public Random Random { get; set; }
    }
}