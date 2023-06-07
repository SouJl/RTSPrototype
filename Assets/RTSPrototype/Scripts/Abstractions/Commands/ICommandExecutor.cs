using System.Threading.Tasks;

namespace RTSPrototype.Abstractions.Commands
{
    public interface ICommandExecutor
    {
        Task TryExecuteCommand(object command);
    }

    public interface ICommandExecutor<T> : ICommandExecutor where T: ICommand 
    {

    }
}
