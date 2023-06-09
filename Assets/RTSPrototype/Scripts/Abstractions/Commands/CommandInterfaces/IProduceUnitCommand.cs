using RTSPrototype.Abstractions.ScriptableObjects;

namespace RTSPrototype.Abstractions.Commands.CommandInterfaces
{
    public interface IProduceUnitCommand : ICommand
    {
        IProducedUnitData Data { get; }
    }
}
