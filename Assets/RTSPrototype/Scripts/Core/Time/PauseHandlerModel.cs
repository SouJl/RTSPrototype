using System.Collections.Generic;
using RTSPrototype.Abstractions;
using UniRx;

namespace RTSPrototype.Core
{
    public class PauseHandlerModel : IPauseHandler
    {
        private readonly List<IPaused> _handlers
            = new List<IPaused>();

        public bool IsPaused { get; private set; }

        public void Register(IPaused pausedObject)
        {
            _handlers.Add(pausedObject);
        }

        public void UnRegister(IPaused pausedObject)
        {
            _handlers.Remove(pausedObject);
        }

        public void SetPause(bool isPaused)
        {
            IsPaused = isPaused;

            if (_handlers.Count == 0)
                return;

            for (int i = 0; i < _handlers.Count; i++)
            {
                _handlers[i].SetPause(isPaused);
            }
        }
    }
}
