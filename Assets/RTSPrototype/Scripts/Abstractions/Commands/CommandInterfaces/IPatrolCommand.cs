using UnityEngine;

namespace RTSPrototype.Abstractions.Commands.CommandInterfaces
{
    public interface IPatrolCommand : ICommand
    {
        public Vector3 StartPosition { get; }
        public Vector3 EndPosition { get; }

    }
}
