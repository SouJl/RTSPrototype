using UniRx;

namespace RTSPrototype.Abstractions
{
    public interface IPauseHandler 
    {
        ReactiveProperty<bool> IsPaused { get;}

        void SetPause(bool isPaused);
    }
}
