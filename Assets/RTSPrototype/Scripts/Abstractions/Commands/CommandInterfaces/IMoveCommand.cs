using UnityEngine;

namespace RTSPrototype.Abstractions.Commands.CommandInterfaces
{
    public interface IMoveCommand : ICommand
    {
        Vector3 TargetPosition { get; }
    }
}
