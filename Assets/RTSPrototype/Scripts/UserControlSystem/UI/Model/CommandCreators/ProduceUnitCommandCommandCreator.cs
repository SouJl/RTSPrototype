using System;
using RTSPrototype.Abstractions;
using RTSPrototype.Abstractions.AssetsInjector;
using RTSPrototype.Abstractions.Commands.CommandInterfaces;
using RTSPrototype.Abstractions.ScriptableObjects;
using RTSPrototype.UIModel.CommandRealization;
using Zenject;

namespace RTSPrototype.UIModel.CommandCreators
{
    public class ProduceUnitCommandCommandCreator : CommandCreatorBase<IProduceUnitCommand>
    {
        [Inject] private IAssetContext _context;
        [Inject] private DiContainer _diContainer;

        protected override void classSpecificCommandCreation(Action<IProduceUnitCommand> creationCallback)
        {
            var produceUnitCommand = _context.Inject(new ProduceUnitCommandHeir());
            _diContainer.Inject(produceUnitCommand);
            creationCallback?.Invoke(produceUnitCommand);
        }
    }
}
