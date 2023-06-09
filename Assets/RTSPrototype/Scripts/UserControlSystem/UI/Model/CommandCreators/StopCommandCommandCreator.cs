using System;
using RTSPrototype.Abstractions;
using RTSPrototype.Abstractions.AssetsInjector;
using RTSPrototype.Abstractions.Commands.CommandInterfaces;
using RTSPrototype.Abstractions.ScriptableObjects;
using RTSPrototype.UIModel.CommandRealization;
using Zenject;

namespace RTSPrototype.UIModel.CommandCreators
{
    public class StopCommandCommandCreator : CommandCreatorBase<IStopCommand>
    {
        [Inject] private IAssetContext _context;

        protected override void classSpecificCommandCreation(Action<IStopCommand> creationCallback)
        {
            creationCallback?.Invoke(_context.Inject(new StopCommand()));
        }
    }
}
