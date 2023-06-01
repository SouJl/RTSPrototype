using System;
using RTSPrototype.Utils;

namespace RTSPrototype.Abstractions
{
    public interface IRTSValue<T> : IAwaitable<T>
    {
        T CurrentValue { get; }

        event Action<T> OnNewValue;

        void SetValue(T value);
    }
}
