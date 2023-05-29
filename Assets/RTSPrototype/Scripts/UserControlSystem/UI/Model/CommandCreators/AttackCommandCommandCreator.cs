using System;
using RTSPrototype.Abstractions.Commands.CommandInterfaces;
using RTSPrototype.UIModel.CommandRealization;
using RTSPrototype.Utils;
using Zenject;

namespace RTSPrototype.UIModel.CommandCreators
{
    public class AttackCommandCommandCreator : CommandCreatorBase<IAttackCommand>
    {
        [Inject] private AssetsContext _context;

        protected override void classSpecificCommandCreation(Action<IAttackCommand> creationCallback)
        {
            creationCallback?.Invoke(_context.Inject(new AttackCommand()));
        }
    }
}
