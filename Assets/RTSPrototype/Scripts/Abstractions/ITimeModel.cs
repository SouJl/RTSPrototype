using System;

namespace RTSPrototype.Abstractions
{
    public interface ITimeModel
    {
        IObservable<int> GameTime { get; }
    }
}
