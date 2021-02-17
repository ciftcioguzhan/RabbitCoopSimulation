using RabbitCoopSimulation.Service.Abstract;
using RabbitCoopSimulation.Service.Model;
using System.Collections.Generic;

namespace RabbitCoopSimulation.Service.Concrete
{
    public class MaleRabbit : Rabbit, IMaleFowl
    {
        public MaleRabbit(ICoopSimulation _coop) : base(_coop)
        {
            Coop = _coop;
        }

        public void Mate(IFemaleFowl femaleFowl) 
        {
            if (Coop.Random.NextDouble() <= (double)RabbitConstraints.ConceptionRate)
            {
                femaleFowl.IsPregnancy = true;
                femaleFowl.PregnancyDate = Coop.Time;
                femaleFowl.Impregnating = this;
            }
        }

        public override void MatingBegins(List<IFowl> fowls)
        {
            if (IsAdolescent == true)
            {
                base.MatingBegins(fowls);
            }
        }
    }
}

