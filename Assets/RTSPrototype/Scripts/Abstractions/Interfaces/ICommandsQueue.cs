namespace RTSPrototype.Abstractions
{
    public interface ICommandsQueue
    {
        void EnqueueCommand(object command);
        void Clear();
    }
}
