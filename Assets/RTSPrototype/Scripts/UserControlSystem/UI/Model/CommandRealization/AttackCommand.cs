using RTSPrototype.Abstractions;
using RTSPrototype.Abstractions.Commands.CommandInterfaces;

namespace RTSPrototype.UIModel.CommandRealization
{
    public class AttackCommand: IAttackCommand
    {
        public IAttackable Target { get; }
        public AttackCommand(IAttackable target)
        {
            Target = target;
        }

    }
}
