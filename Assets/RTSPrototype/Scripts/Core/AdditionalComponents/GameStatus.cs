using UniRx;
using System;
using UnityEngine;
using System.Threading;
using RTSPrototype.Utils;
using RTSPrototype.Abstractions;

namespace RTSPrototype.Core.AdditionalComponents
{
    public class GameStatus : MonoBehaviour, IGameStatus
    {
        private Subject<int> _status = new Subject<int>();
        public IObservable<int> Status => _status;

        private void Update()
        {
            ThreadPool.QueueUserWorkItem(CheckStatus);
        }

        private void CheckStatus(object state)
        {
            if (FactionGameState.FactionsCount == 0)
            {
                _status.OnNext(0);
            }
            else if (FactionGameState.FactionsCount == 1)
            {
                _status.OnNext(FactionGameState.GetWinner());
            }
        }
    }
}
