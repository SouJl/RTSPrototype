using UniRx;
using System;
using Zenject;
using UnityEngine;
using System.Threading;
using RTSPrototype.Abstractions;

namespace RTSPrototype.Core.AdditionalComponents
{
    public class GameStatus : MonoBehaviour, IGameStatus
    {
        [Inject] private IFactionManager _factionManager;

        private Subject<int> _status = new Subject<int>();
        public IObservable<int> Status => _status;

        private void Update()
        {
            ThreadPool.QueueUserWorkItem(CheckStatus);
        }

        private void CheckStatus(object state)
        {
            if (_factionManager.FactionsCount == 0)
            {
                _status.OnNext(0);
            }
            else if (_factionManager.FactionsCount == 1)
            {
                _status.OnNext(_factionManager.GetWinner());
            }
        }
    }
}
