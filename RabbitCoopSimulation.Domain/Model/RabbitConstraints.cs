using System.Collections.Generic;

namespace RabbitCoopSimulation.Domain.Model
{
    public static class RabbitConstraints
    {
        //sonuna tipini yaz inMonth gibi

        /// <summary>
        ///ConceptionRate => Gebe Kalma Oranı
        ///PubescencePeriodInDays => Ergenliğe Erişim Süresi
        ///ConceptionProcessPeriodInDays => Hamilelik Süreci
        ///RateOfAngagementInMating => Çiftleşmeye Katılma Oranı
        ///FemaleBirthRate => Dişi Doğum Oranı
        /// </summary>
        public const decimal ConceptionRate = 0.30M; 
        public const int PubescencePeriodInDays = 300; 
        public const int ConceptionProcessPeriodInDays = 40;
        public const decimal RateOfAngagementInMating = 0.60M; 
        public const decimal FemaleBirthRate = 0.50M; 
        public const int RabbitMaxAge = 9; 

        public static Dictionary<int, decimal> NumberOfChildren = new Dictionary<int, decimal>
        {
            { 4, 0.10M },
            { 5, 0.12M },
            { 6, 0.16M },
            { 7, 0.20M },
            { 8, 0.12M },
            { 9, 0.11M },
            { 10, 0.09M },
            { 11, 0.07M },
            { 12, 0.03M },
        };

        public static Dictionary<int, decimal> DeathRate = new Dictionary<int, decimal>
        {
            { 0, 0.01M / 365},
            { 1, 0.01M / 365},
            { 2, 0.03M / 365},
            { 3, 0.05M / 365},
            { 4, 0.10M / 365},
            { 5, 0.12M / 365},
            { 6, 0.13M / 365},
            { 7, 0.16M / 365},
            { 8, 0.20M / 365},
            { 9, 0.29M / 365},
        };
    }
}
