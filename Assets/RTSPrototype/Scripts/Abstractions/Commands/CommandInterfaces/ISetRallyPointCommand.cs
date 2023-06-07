using UnityEngine;

namespace RTSPrototype.Abstractions.Commands.CommandInterfaces
{
    public interface ISetRallyPointCommand : ICommand
    {
        Vector3 RallyPoint { get; }
    }
}
