using RabbitCoopSimulation.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitCoopSimulation.Service
{
    public class CoopSimulation : ICoopSimulation
    {
        public HashSet<IFowl> Population { get; set; }
        public DateTime Time { get; set; }
        public Random Random { get; set; }
        public HashSet<IFowl> Graveyard { get; set; }

        public event MatingNotify CouplingNotify;

        public event BirthNotify BirthNotify;

        public event DeathNotify DeathNotify;

        public CoopSimulation()
        {
            Random = new Random();
            Graveyard = new HashSet<IFowl>();
        }

        public void MatingTime()
        {
            if (CouplingNotify == null) return;

            List<IFowl> fowl = new List<IFowl>();
            CouplingNotify(fowl);

            //Her tipin(Tavşan-Tavşan) kendi arasında çiftleşme yapması için groupBy eklendi.
            foreach (var groupedFowl in fowl.GroupBy(x => x.GetType().BaseType).Select(x => x.ToList()))
            {
                var femaleList = groupedFowl.Where(x => x is IFemaleFowl).Cast<IFemaleFowl>().ToList();
                var maleList = groupedFowl.Where(x => x is IMaleFowl).Cast<IMaleFowl>().ToList();

                if (maleList.Count == 0)
                {
                    return;
                }

                Dictionary<IMaleFowl, List<IFemaleFowl>> dictionary = new Dictionary<IMaleFowl, List<IFemaleFowl>>();

                maleList = Mating(femaleList, maleList, dictionary);

                foreach (var item in dictionary)
                {
                    foreach (var femaleItem in item.Value)
                    {
                        item.Key.Mate(femaleItem);
                    }
                }
            }
        }

        private List<IMaleFowl> Mating(List<IFemaleFowl> femaleList, List<IMaleFowl> maleList, Dictionary<IMaleFowl, List<IFemaleFowl>> dictionary)
        {
            while (femaleList.Count > 0)
            {
                if (maleList.Count == 0)
                {
                    maleList = dictionary.Keys.ToList();
                }

                int maleIndex = Random.Next(0, maleList.Count);
                IMaleFowl selectedMale = maleList[maleIndex];
                maleList.RemoveAt(maleIndex);

                int femaleIndex = Random.Next(0, femaleList.Count);
                IFemaleFowl selectedFemale = femaleList[femaleIndex];
                femaleList.RemoveAt(femaleIndex);

                if (dictionary.TryGetValue(selectedMale, out List<IFemaleFowl> result))
                {
                    result.Add(selectedFemale);
                }
                else
                {
                    List<IFemaleFowl> femaleFowls = new List<IFemaleFowl>();
                    femaleFowls.Add(selectedFemale);
                    dictionary.Add(selectedMale, femaleFowls);
                }
            }

            return maleList;
        }

        public void BirthTime()
        {
            if (BirthNotify == null) return;

            List<IFemaleFowl> fowl = new List<IFemaleFowl>();
            BirthNotify(fowl);

            foreach (var item in fowl)
            {
                List<IFowl> fowlList = item.Birth();

                fowlList.ForEach(x =>
                    Population.Add(x)
                );
            }
        }

        public void DeathTime()
        {
            if (DeathNotify == null) return;

            List<IFowl> fowl = new List<IFowl>();
            DeathNotify(fowl);

            foreach (var item in fowl)
            {
                item.Death();
                Population.Remove(item);
                Graveyard.Add(item);
            }
        }

        public void NextCycle()
        {
            MatingTime();
            BirthTime();
            DeathTime();

            Time = Time.AddDays(1);
        }

    }
}
