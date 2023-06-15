using UnityEngine;
using RTSPrototype.Abstractions.Commands.CommandInterfaces;

namespace RTSPrototype.UIModel.CommandRealization
{
    public class SetRallyPointCommand : ISetRallyPointCommand
    {
        public Vector3 RallyPoint { get; private set; }

        public SetRallyPointCommand(Vector3 rallyPoint)
        {
            RallyPoint = rallyPoint;
        }
    }
}
