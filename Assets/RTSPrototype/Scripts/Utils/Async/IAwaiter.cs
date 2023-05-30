using System.Runtime.CompilerServices;

namespace RTSPrototype.Utils
{
    public interface IAwaiter<T> : INotifyCompletion
    {
        bool IsCompleted { get; }

        T GetResult();
    }
}