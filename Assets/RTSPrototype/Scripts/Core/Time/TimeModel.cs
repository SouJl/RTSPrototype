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

        public void Tick()
        {
            _gameTime.Value += Time.deltaTime;
        }
    }
}
