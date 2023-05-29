using System;
using RTSPrototype.Abstractions.Commands.CommandInterfaces;
using RTSPrototype.UIModel.CommandRealization;
using RTSPrototype.Utils;
using Zenject;

namespace RTSPrototype.UIModel.CommandCreators
{
    public class MoveCommandCommandCreator : CommandCreatorBase<IMoveCommand>
    {
        [Inject] private AssetsContext _context;

        protected override void classSpecificCommandCreation(Action<IMoveCommand> creationCallback)
        {
            creationCallback?.Invoke(_context.Inject(new MoveCommand()));
        }
    }
}
