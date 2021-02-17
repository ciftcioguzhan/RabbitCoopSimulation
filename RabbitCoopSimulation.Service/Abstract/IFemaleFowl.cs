using System;
using System.Collections.Generic;

namespace RabbitCoopSimulation.Service.Abstract
{
    /// <summary>
    /// Impregnating => Hamile Bırakan
    /// </summary>
    public interface IFemaleFowl : IFowl
    {
        void BirthBegins(List<IFemaleFowl> fowls);
        bool IsBirthTime();
        List<IFowl> Birth();
        IMaleFowl Impregnating { get; set; }
        DateTime? PregnancyDate { get; set; }
        bool IsPregnancy { get; set; }
    }
}
