using System;

namespace RTSPrototype.Abstractions
{
    public interface IRTSValue<T>
    {
        T CurrentValue { get; }

        event Action<T> OnSelected;

        void SetValue(T value);
    }
}
