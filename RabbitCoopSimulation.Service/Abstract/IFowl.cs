using RabbitCoopSimulation.Service.Abstract;
using System;
using System.Collections.Generic;

namespace RabbitCoopSimulation.Service.Abstract
{
    /// <summary>
    /// IsAdolescent => Ergenlik durum kontrolü 
    /// </summary>
    public interface IFowl
    {
        void MatingBegins(List<IFowl> fowls); 
        void DeathBegins(List<IFowl> fowls);
        void Death();
        public IFemaleFowl Mother { get; set; }
        public IMaleFowl Father { get; set; }
        DateTime BirthDate { get; set; }
        public DateTime? DeathDate { get; set; }
        public bool isAlive { get; set; }
        public bool IsAdolescent { get; }
        int Age { get; }

    }
}
