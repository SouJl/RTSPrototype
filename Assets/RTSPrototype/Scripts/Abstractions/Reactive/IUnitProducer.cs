using UniRx;

namespace RTSPrototype.Abstractions
{
    public interface IUnitProducer
    {
        IReadOnlyReactiveCollection<IUnitProductionTask> Queue { get; }
        public void Cancel(int index);

    }
}
