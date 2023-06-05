namespace RTSPrototype.Abstractions
{
    public interface IPauseHandler : IPaused
    {
        bool IsPaused { get;}

        void Register(IPaused pausedObject);
        void UnRegister(IPaused pausedObject);
    }
}
