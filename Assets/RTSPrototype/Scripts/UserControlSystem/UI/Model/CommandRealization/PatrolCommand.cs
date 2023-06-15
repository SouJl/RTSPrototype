using UnityEngine;
using RTSPrototype.Abstractions.Commands.CommandInterfaces;

namespace RTSPrototype.UIModel.CommandRealization
{
    public class PatrolCommand : IPatrolCommand
    {
        public Vector3 StartPosition { get; }

        public Vector3 EndPosition { get; }

        public PatrolCommand(Vector3 startPosition, Vector3 endPosition) 
        {
            StartPosition = startPosition;
            EndPosition = endPosition;
        }
    }
}
