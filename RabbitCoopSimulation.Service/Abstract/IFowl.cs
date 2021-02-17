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
        public Guid ID { get; set; }
        void MatingBegins(List<IFowl> fowls); 
        /// <summary>
        /// Ölüm Başlangıcı
        /// </summary>
        /// <param name="fowls"></param>
        void DeathBegins(List<IFowl> fowls);
        /// <summary>
        ///Ölüm(Ölüm gerçekleştiğinde kümes içerisinden çıakrtılır)
        /// </summary>
        void Death();
        public IFemaleFowl Mother { get; set; }
        public IMaleFowl Father { get; set; }
        DateTime BirthDate { get; set; }
        /// <summary>
        /// Ölüm Zamanı
        /// </summary>
        public DateTime? DeathDate { get; set; }
        public bool IsAlive { get; set; }
        /// <summary>
        /// Erişkin mi ?
        /// </summary>
        public bool IsAdolescent { get; }
        /// <summary>
        /// Yaş
        /// </summary>
        int Age { get; }

    }
}
