using RabbitCoopSimulation.Service.Abstract;

namespace RabbitCoopSimulation.Service.Abstract
{
    public interface IMaleFowl : IFowl
    {
        void Mate(IFemaleFowl femaleFowl);
    }
}
