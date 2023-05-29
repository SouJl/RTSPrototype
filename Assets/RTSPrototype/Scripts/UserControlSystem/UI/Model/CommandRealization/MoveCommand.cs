using RTSPrototype.Abstractions.Commands.CommandInterfaces;
using UnityEngine;

namespace RTSPrototype.UIModel.CommandRealization
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
