using UnityEngine;
using RTSPrototype.Abstractions;
using RTSPrototype.UIModel.CommandRealization;
using RTSPrototype.Abstractions.Commands.CommandInterfaces;


namespace RTSPrototype.UIModel.CommandCreators
{
    public class SetRallyPointCommandCreator : CancellableCommandCreatorBase<ISetRallyPointCommand, Vector3>
    {
        protected override ISetRallyPointCommand CreateCommand(Vector3 argument) => 
            new SetRallyPointCommand(argument);
    }
}
