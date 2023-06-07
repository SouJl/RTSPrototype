using System.Threading.Tasks;
using RTSPrototype.Abstractions.Commands;
using RTSPrototype.Abstractions.Commands.CommandInterfaces;
using UnityEngine;

namespace RTSPrototype.Core.CommandExecutors
{
    public class AttackCommandExecutor : CommandExecutorBase<IAttackCommand>
    {
        public override async Task ExecuteSpecificCommand(IAttackCommand command) =>
             ExecuteAttack(command);

        private void ExecuteAttack(IAttackCommand command) 
        {
            Debug.Log($"Execute Attack by {name} " +
                $"and hit {command.Target} with {command.Target.CurrentHealth}");
        }
    }
}
