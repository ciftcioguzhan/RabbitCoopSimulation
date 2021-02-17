using RabbitCoopSimulation.Service.Abstract;

namespace RabbitCoopSimulation.Service.Abstract
{
    public interface IMaleFowl : IFowl
    {
        /// <summary>
        /// Çiftleşme
        /// </summary>
        /// <param name="femaleFowl"></param>
        void Mate(IFemaleFowl femaleFowl);
    }
}
