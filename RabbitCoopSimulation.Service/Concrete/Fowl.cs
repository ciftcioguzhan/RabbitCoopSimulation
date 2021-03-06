﻿using RabbitCoopSimulation.Service.Abstract;
using System;
using System.Collections.Generic;

namespace RabbitCoopSimulation.Service.Concrete
{
    public abstract class Fowl : IFowl
    {
        public Guid ID { get; set; }
        public ICoopSimulation Coop { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime? DeathDate { get; set; }
        public int Age => Math.Min(Coop.Time.Year, DeathDate?.Year ?? int.MaxValue) - BirthDate.Year;
        public IFemaleFowl Mother { get; set; }
        public IMaleFowl Father { get; set; }
        public bool IsAlive { get; set; }

        public abstract bool IsAdolescent { get; }

        public Fowl(ICoopSimulation _coop)
        {
            ID = Guid.NewGuid();
            Coop = _coop;
            Coop.CouplingNotify += MatingBegins;
            Coop.DeathNotify += DeathBegins;
            BirthDate = Coop.Time;
            IsAlive = true;
        }

        public abstract void MatingBegins(List<IFowl> fowls);

        public abstract void DeathBegins(List<IFowl> fowls);

        public virtual void Death()
        {
            Coop.CouplingNotify -= MatingBegins;
            Coop.DeathNotify -= DeathBegins;
            DeathDate = Coop.Time;
            IsAlive = false;
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }
    }
}
