using RabbitCoopSimulation.Domain.Model;
using RabbitCoopSimulation.Service.Abstract;
using RabbitCoopSimulation.Service.Concrete;
using System.Collections.Generic;

namespace RabbitCoopSimulation.Service
{
    public abstract class Rabbit : Fowl
    {
        public override bool IsAdolescent { get { return Coop.Time.Subtract(BirthDate).TotalDays >= RabbitConstraints.PubescencePeriodInDays; } }
        public Rabbit(ICoopSimulation coop) : base(coop)
        {

        }

        public override void MatingBegins(List<IFowl> fowls)
        {
            if (Coop.Random.NextDouble() <= (double)RabbitConstraints.RateOfAngagementInMating)
            {
                fowls.Add(this);
            }
        }

        public override void DeathBegins(List<IFowl> fowls)
        {
            if (Age > RabbitConstraints.RabbitMaxAge || (decimal)Coop.Random.NextDouble() <= RabbitConstraints.DeathRate[Age])
            {
                fowls.Add(this);
            }
        }
    }
}
