using System;
using System.Collections.Generic;

namespace RabbitCoopSimulation.Service.Abstract
{
    public interface IFemaleFowl : IFowl
    {
        /// <summary>
        /// Doğum Başlangıcı
        /// </summary>
        /// <param name="fowls"></param>
        void BirthBegins(List<IFemaleFowl> fowls);
        /// <summary>
        /// Doğum Zamanı
        /// </summary>
        /// <returns></returns>
        bool IsBirthTime();
        /// <summary>
        /// Doğum
        /// </summary>
        /// <returns></returns>
        List<IFowl> Birth();
        /// <summary>
        /// Gebe Bırakan
        /// </summary>
        IMaleFowl Impregnating { get; set; }
        /// <summary>
        /// Gebelik Tarihi
        /// </summary>
        DateTime? PregnancyDate { get; set; }
        /// <summary>
        /// Gebelik Kontrolü
        /// </summary>
        bool IsPregnancy { get; set; }
    }
}
