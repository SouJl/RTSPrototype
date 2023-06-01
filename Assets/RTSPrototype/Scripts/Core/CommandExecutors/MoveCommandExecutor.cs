using RTSPrototype.Abstractions.Commands;
using RTSPrototype.Abstractions.Commands.CommandInterfaces;
using UnityEngine;
using UnityEngine.AI;

namespace RTSPrototype.Core.CommandExecutors
{
    public class MoveCommandExecutor : CommandExecutorBase<IMoveCommand>
    {
        public override void ExcecuteSpecifiedCommand(IMoveCommand command) => 
            ExceuteMove(command);

        private void ExceuteMove(IMoveCommand command)
        {
            GetComponent<NavMeshAgent>().destination = command.TargetPosition;
        }
    }
}
