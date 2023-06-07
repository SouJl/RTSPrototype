using System.Threading;
using System.Threading.Tasks;
using RTSPrototype.Abstractions.Commands;
using RTSPrototype.Abstractions.Commands.CommandInterfaces;

namespace RTSPrototype.Core.CommandExecutors
{
    public class StopCommandExecutor : CommandExecutorBase<IStopCommand>
    {
        public CancellationTokenSource CancellationTokenSource { get; set; }

        public override async Task ExecuteSpecificCommand(IStopCommand command) =>
            ExecuteStop(command);

        private void ExecuteStop(IStopCommand command)
        {
            CancellationTokenSource?.Cancel();
        }
    }
}
