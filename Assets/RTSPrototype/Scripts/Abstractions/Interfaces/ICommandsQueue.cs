namespace RTSPrototype.Abstractions
{
    public interface ICommandsQueue
    {
        int CurrentCommandInQueue { get; }
        void EnqueueCommand(object command);
        void Clear();
    }
}
