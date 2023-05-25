using RTSPrototype.Abstractions.Commands;
using RTSPrototype.Abstractions.Commands.CommandInterfaces;
using UnityEngine;

namespace RTSPrototype.Core.CommandExecutors
{
    public class StopCommandExecutor : CommandExecutorBase<IStopCommand>
    {
        public override void ExcecuteSpecifiedCommand(IStopCommand command) =>
            ExecuteStop(command);

        private void ExecuteStop(IStopCommand command)
        {
            Debug.Log($"Execute Stop by {name}");
        }
    }
}
