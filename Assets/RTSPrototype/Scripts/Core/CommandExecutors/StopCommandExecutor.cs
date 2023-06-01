using System.Threading;
using RTSPrototype.Abstractions.Commands;
using RTSPrototype.Abstractions.Commands.CommandInterfaces;

namespace RTSPrototype.Core.CommandExecutors
{
    public class StopCommandExecutor : CommandExecutorBase<IStopCommand>
    {
        public CancellationTokenSource CancellationTokenSource { get; set; }

        public override void ExcecuteSpecifiedCommand(IStopCommand command) =>
            ExecuteStop(command);

        private void ExecuteStop(IStopCommand command)
        {
            CancellationTokenSource?.Cancel();
        }
    }
}
