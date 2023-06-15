using UniRx;
using System;

namespace RTSPrototype.Abstractions
{
    public abstract class ReactiveRTSValueBase<T> : RTSValueBase<T>, IObservable<T>
    {
        private ReactiveProperty<T> _reactiveValue
            = new ReactiveProperty<T>();

        public override void SetValue(T value)
        {
            base.SetValue(value);
            _reactiveValue.Value = value;
        }

        public IDisposable Subscribe(IObserver<T> observer) 
            => _reactiveValue.Subscribe(observer);

    }
}
