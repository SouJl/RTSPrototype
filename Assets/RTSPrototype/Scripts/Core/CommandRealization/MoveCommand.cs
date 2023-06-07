
using RTSPrototype.Abstractions.Commands.CommandInterfaces;
using UnityEngine;

namespace RTSPrototype.Core.CommandRealization
{
    public class MoveCommand : IMoveCommand
    {
        public Vector3 TargetPosition { get; }

        public MoveCommand(Vector3 targetPos)
        {
            TargetPosition = targetPos;
        }
    }
}
