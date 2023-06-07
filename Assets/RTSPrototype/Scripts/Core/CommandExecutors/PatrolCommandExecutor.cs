using System.Threading.Tasks;
using RTSPrototype.Abstractions.Commands;
using RTSPrototype.Abstractions.Commands.CommandInterfaces;
using UnityEngine;

namespace RTSPrototype.Core.CommandExecutors
{
    public class PatrolCommandExecutor: CommandExecutorBase<IPatrolCommand>
    {
        public override async Task ExecuteSpecificCommand(IPatrolCommand command) => 
            ExecutePatrol(command);

        private void ExecutePatrol(IPatrolCommand command)
        {
            Debug.Log($"Execute Patrol by {name} from {command.StartPosition} to {command.EndPosition}");
        }
    }
}
