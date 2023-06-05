using System;
using RTSPrototype.Utils;

namespace RTSPrototype.Abstractions
{
    public abstract partial class RTSValueBase<T> : IRTSValue<T>
    {
        public T CurrentValue { get; protected set; }

        public event Action<T> OnNewValue;

        public virtual void SetValue(T value) 
        {
            CurrentValue = value;
            OnNewValue?.Invoke(value);
        }

        public IAwaiter<T> GetAwaiter() 
            => new NewValueNotifier<T>(this);
    }
}
