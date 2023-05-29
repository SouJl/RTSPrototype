using System;

namespace RTSPrototype.Abstractions
{
    public abstract class RTSValueBase<T> : IRTSValue<T>
    {
        public T CurrentValue { get; protected set; }

        public abstract event Action<T> OnNewValue;

        public abstract void SetValue(T value);
    }
}
