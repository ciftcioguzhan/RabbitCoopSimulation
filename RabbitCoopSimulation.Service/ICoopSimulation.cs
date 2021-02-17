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
        /// <summary>
        /// Çiftleşesek Olan Hayvanlar 
        /// </summary>
        event MatingNotify CouplingNotify;
        /// <summary>
        /// Doğum Yapacak Olan Hayvanlar
        /// </summary>
        event BirthNotify BirthNotify;
        /// <summary>
        /// Ölüm Zamanı Gelen Hayvanlar
        /// </summary>
        event DeathNotify DeathNotify;

        /// <summary>
        /// Kümes Popilasyonu Hesaplama
        /// </summary>
        HashSet<IFowl> Population { get; set; }
        /// <summary>
        /// Ölümü Gerçekleşen hayvanların mezarlığı
        /// </summary>
        HashSet<IFowl> Graveyard { get; set; } 
        public void NextCycle();
        /// <summary>
        /// Çiftleşme 
        /// </summary>
        public void MatingTime();
        /// <summary>
        /// Doğum
        /// </summary>
        public void BirthTime();
        /// <summary>
        /// Ölüm
        /// </summary>
        public void DeathTime();
        public DateTime Time { get; set; }
        public Random Random { get; set; }
    }
}