using System;

namespace RTSPrototype.Abstractions
{
    public interface IRTSValue<T> 
    {
        T CurrentValue { get; }

        event Action<T> OnNewValue;

        void SetValue(T value);
    }
}
