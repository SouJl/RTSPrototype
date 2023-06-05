using System;
using RTSPrototype.Abstractions;
using UniRx;
using UnityEngine;
using Zenject;

namespace RTSPrototype.Core
{
    public class TimeModel : ITimeModel, ITickable
    {
        private ReactiveProperty<float> _gameTime = new ReactiveProperty<float>();

        public IObservable<int> GameTime => _gameTime.Select(t => (int)t);

        private bool _isActiveTick = true;

        public void Tick()
        {
            if (!_isActiveTick)
                return;

            _gameTime.Value += Time.deltaTime;
        }

        [Inject]
        private void Init(IPauseHandler pauseHandler)
        {
            pauseHandler.IsPaused.Subscribe(OnPause);
        }

        private void OnPause(bool isPaused)
        {
            _isActiveTick = !isPaused;
        }
    }
}
