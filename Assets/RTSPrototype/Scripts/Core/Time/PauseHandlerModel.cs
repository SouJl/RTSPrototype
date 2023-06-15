using UniRx;
using RTSPrototype.Abstractions;

namespace RTSPrototype.Core
{
    public class PauseHandlerModel : IPauseHandler
    {
        public ReactiveProperty<bool> IsPaused { get; private set; } 
            = new ReactiveProperty<bool>();

        public void SetPause(bool isPaused) => 
            IsPaused.Value = isPaused;
    }
}
