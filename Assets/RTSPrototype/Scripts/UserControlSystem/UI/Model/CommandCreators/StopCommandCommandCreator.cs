using System;
using Zenject;
using RTSPrototype.Abstractions;
using RTSPrototype.UIModel.CommandRealization;
using RTSPrototype.Abstractions.AssetsInjector;
using RTSPrototype.Abstractions.ScriptableObjects;
using RTSPrototype.Abstractions.Commands.CommandInterfaces;

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
