using RTSPrototype.Abstractions;
using RTSPrototype.UIModel.CommandRealization;
using RTSPrototype.Abstractions.Commands.CommandInterfaces;

namespace RTSPrototype.UIModel.CommandCreators
{
    public class AttackCommandCommandCreator : CancellableCommandCreatorBase<IAttackCommand, IAttackable>
    {
        protected override IAttackCommand CreateCommand(IAttackable argument) 
            => new AttackCommand(argument);
    }
}
