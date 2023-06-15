using Zenject;
using UnityEngine;
using RTSPrototype.Abstractions;
using RTSPrototype.Abstractions.Commands;
using RTSPrototype.Abstractions.Commands.CommandInterfaces;

namespace RTSPrototype.Core.Building
{
    public class BuildingCommandQueue : MonoBehaviour, ICommandsQueue
    {
        [Inject] private CommandExecutorBase<IProduceUnitCommand> _produceUnitCommandExecutor;
        [Inject] private CommandExecutorBase<ISetRallyPointCommand> _setRallyPointCommandExecutor;

        public void Clear() { }
        public async void EnqueueCommand(object command)
        {
            await _produceUnitCommandExecutor.TryExecuteCommand(command);
            await _setRallyPointCommandExecutor.TryExecuteCommand(command);
        }

    }
}
