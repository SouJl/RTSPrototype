using UnityEngine;
using RTSPrototype.Abstractions;
using RTSPrototype.UIModel.CommandRealization;
using RTSPrototype.Abstractions.Commands.CommandInterfaces;

namespace RTSPrototype.UIModel.CommandCreators
{
    public class MoveCommandCommandCreator : CancellableCommandCreatorBase<IMoveCommand, Vector3>
    {
        protected override IMoveCommand CreateCommand(Vector3 argument) => new MoveCommand(argument);
    }
}
