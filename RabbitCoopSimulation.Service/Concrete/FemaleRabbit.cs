using RabbitCoopSimulation.Service.Abstract;
using RabbitCoopSimulation.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitCoopSimulation.Service.Concrete
{
    public class FemaleRabbit : Rabbit, IFemaleFowl
    {
        public DateTime? PregnancyDate { get; set; }
        
        public bool IsPregnancy { get; set; }
        public IMaleFowl Impregnating { get; set; }

        public FemaleRabbit(ICoopSimulation _coop) : base(_coop)
        {
            Coop = _coop;
            Coop.BirthNotify += BirthBegins;
        }

        public override void MatingBegins(List<IFowl> fowls)
        {
            if (IsAdolescent == true && IsPregnancy != true)
            {
                base.MatingBegins(fowls);
            }
        }

        public void BirthBegins(List<IFemaleFowl> fowls)
        {
            if (IsBirthTime())
            {
                fowls.Add(this);
            }
        }

        public List<IFowl> Birth()
        {
            List<IFowl> children = new List<IFowl>();

            int numberOfChildren = NumberOfChildrenCalculation();
            for (int i = 0; i < numberOfChildren; i++)
            {
                decimal randomNumber = (decimal)Coop.Random.NextDouble();

                IFowl child;
                if (randomNumber <= RabbitConstraints.FemaleBirthRate)
                {
                    child = new FemaleRabbit(Coop);
                }
                else
                {
                    child = new MaleRabbit(Coop);
                }

                child.Mother = this;
                child.Father = this.Impregnating;

                children.Add(child);
            }

            IsPregnancy = false;
            Impregnating = null;
            PregnancyDate = null;

            return children;
        }

        public bool IsBirthTime()
        {
            return IsPregnancy == true && Coop.Time.Subtract(PregnancyDate.Value).TotalDays >= RabbitConstraints.ConceptionProcessPeriodInDays;
        }

        private int NumberOfChildrenCalculation()
        {
            decimal randomNumber = (decimal)Coop.Random.NextDouble();

            decimal cumulative = 0;
            foreach (var item in RabbitConstraints.NumberOfChildren)
            {
                cumulative += item.Value;

                if (randomNumber <= cumulative)
                {
                    return item.Key;
                }
            }

            return RabbitConstraints.NumberOfChildren.Keys.FirstOrDefault();
        }

        public override void Death()
        {
            Coop.BirthNotify -= BirthBegins;
            base.Death();
        }
    }
}
