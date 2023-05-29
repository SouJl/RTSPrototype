using System;
using RTSPrototype.Abstractions.Commands.CommandInterfaces;
using RTSPrototype.UIModel.CommandRealization;
using RTSPrototype.Utils;
using Zenject;

namespace RTSPrototype.UIModel.CommandCreators
{
    public class ProduceUnitCommandCommandCreator : CommandCreatorBase<IProduceUnitCommand>
    {
        [Inject] private AssetsContext _context;

        protected override void classSpecificCommandCreation(Action<IProduceUnitCommand> creationCallback)
        {
            creationCallback?.Invoke(_context.Inject(new ProduceUnitCommandHeir()));
        }
    }
}
