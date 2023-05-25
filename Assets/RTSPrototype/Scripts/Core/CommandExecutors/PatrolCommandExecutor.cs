using RTSPrototype.Abstractions.Commands;
using RTSPrototype.Abstractions.Commands.CommandInterfaces;
using UnityEngine;

namespace RTSPrototype.Core.CommandExecutors
{
    public class PatrolCommandExecutor: CommandExecutorBase<IPatrolCommand>
    {
        public override void ExcecuteSpecifiedCommand(IPatrolCommand command) => 
            ExecutePatrol(command);

        private void ExecutePatrol(IPatrolCommand command)
        {
            Debug.Log($"Execute Patrol by {name}");
        }
    }
}
