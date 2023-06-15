using System;

namespace RTSPrototype.Abstractions
{
    public interface IGameStatus
    {
        IObservable<int> Status { get; }
    }
}
