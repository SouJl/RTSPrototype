namespace RTSPrototype.Abstractions.Commands
{
    public interface ICommandExecutor
    {
        void ExecuteCommand(object command);
    }
}
