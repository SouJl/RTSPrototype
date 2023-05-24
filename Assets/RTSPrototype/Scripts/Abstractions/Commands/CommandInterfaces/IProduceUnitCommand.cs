using UnityEngine;

namespace RTSPrototype.Abstractions.Commands.CommandInterfaces
{
    public interface IProduceUnitCommand : ICommand
    {
        GameObject UnitPrefab { get; }
    }
}
