using System.Collections.Generic;

namespace RabbitCoopSimulation.Domain.Model
{
    public static class RabbitConstraints
    {
        /// <summary>
        /// Gebe Kalma Oranı
        /// </summary>
        public const decimal ConceptionRate = 0.15M;
        /// <summary>
        /// Ergenliğe Erişim Süresi
        /// </summary>
        public const int PubescencePeriodInDays = 510; 
        /// <summary>
        /// Gebelik Süreci
        /// </summary>
        public const int ConceptionProcessPeriodInDays = 40;
        /// <summary>
        /// Çiftleşmeye Katıma Oranı
        /// </summary>
        public const decimal RateOfAngagementInMating = 0.25M; 
        /// <summary>
        /// Dişi Doğum Oranı
        /// </summary>
        public const decimal FemaleBirthRate = 0.50M; 
        /// <summary>
        /// Yaşam Ömrü
        /// </summary>
        public const int RabbitMaxAge = 9; 
        /// <summary>
        /// Çocuk Sayı Oranı
        /// </summary>
        public static Dictionary<int, decimal> NumberOfChildren = new Dictionary<int, decimal>
        {
            { 4, 0.35M },
            { 5, 0.24M },
            { 6, 0.20M },
            { 7, 0.06M },
            { 8, 0.05M },
            { 9, 0.04M },
            { 10, 0.03M },
            { 11, 0.02M },
            { 12, 0.01M },
        };
        /// <summary>
        /// Ölüm Oranı
        /// </summary>
        public static Dictionary<int, decimal> DeathRate = new Dictionary<int, decimal>
        {
            { 0, 0.03M / 365},
            { 1, 0.02M / 365},
            { 2, 0.03M / 365},
            { 3, 0.05M / 365},
            { 4, 0.10M / 365},
            { 5, 0.12M / 365},
            { 6, 0.13M / 365},
            { 7, 0.16M / 365},
            { 8, 0.20M / 365},
            { 9, 0.26M / 365},
        };
    }
}
