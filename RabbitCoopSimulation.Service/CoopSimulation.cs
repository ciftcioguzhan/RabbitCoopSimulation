using RabbitCoopSimulation.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace RabbitCoopSimulation.Service
{
    public class CoopSimulation : ICoopSimulation
    {
        private Stopwatch _stopWatch;
        private double _totalMatingTimeInMilliseconds;
        private double _totalBirthTimeTimeInMilliseconds;
        private double _totalDeathTimeTimeInMilliseconds;
        public double AverageMatingTimeInMilliseconds { get { return _totalMatingTimeInMilliseconds / CycleCounter; } }
        public double AverageBirthTimeInMilliseconds { get { return _totalBirthTimeTimeInMilliseconds / CycleCounter; } }
        public double AverageDeathTimeInMilliseconds { get { return _totalDeathTimeTimeInMilliseconds / CycleCounter; } }
        public double TotalSimulationTimeInSeconds { get { return (_totalMatingTimeInMilliseconds + _totalBirthTimeTimeInMilliseconds + _totalDeathTimeTimeInMilliseconds) / 1000; } }

        public HashSet<IFowl> Population { get; set; }
        public HashSet<IFowl> Graveyard { get; set; }

        public DateTime Time { get; set; }
        public Random Random { get; set; }
        public int CycleCounter { get; set; }

        public event MatingNotify CouplingNotify;
        public event BirthNotify BirthNotify;
        public event DeathNotify DeathNotify;

        public CoopSimulation()
        {
            Random = new Random();
            Graveyard = new HashSet<IFowl>();
            _stopWatch = new Stopwatch();
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
                BirthOperation(item);
            }
        }

        private void BirthOperation(IFemaleFowl item)
        {
            List<IFowl> fowlList = item.Birth();

            fowlList.ForEach(x =>
               AddNewFowl(x)
            );
        }

        public void DeathTime()
        {
            if (DeathNotify == null) return;

            List<IFowl> fowl = new List<IFowl>();
            DeathNotify(fowl);

            foreach (var item in fowl)
            {
                DeathOperation(item);
            }
        }

        private void DeathOperation(IFowl item)
        {
            item.Death();
            RemoveFowl(item);
        }

        private void AddNewFowl(IFowl item)
        {
            Population.Add(item);
        }

        private void RemoveFowl(IFowl item)
        {
            Population.Remove(item);
            Graveyard.Add(item);
        }

        public void NextCycle()
        {
            _stopWatch.Restart();
            MatingTime();
            _stopWatch.Stop();

            _totalMatingTimeInMilliseconds += _stopWatch.ElapsedMilliseconds;

            _stopWatch.Restart();
            BirthTime();
            _stopWatch.Stop();

            _totalBirthTimeTimeInMilliseconds += _stopWatch.ElapsedMilliseconds;

            _stopWatch.Restart();
            DeathTime();
            _stopWatch.Stop();

            _totalDeathTimeTimeInMilliseconds += _stopWatch.ElapsedMilliseconds;

            Time = Time.AddDays(1);

            CycleCounter++;
        }

    }
}
