using System;
using System.Threading;
using RTSPrototype.Abstractions;
using RTSPrototype.Abstractions.ScriptableObjects;
using RTSPrototype.Core.CommandExecutors;
using RTSPrototype.Utils;
using UnityEngine;

namespace RTSPrototype.Core.Navigation
{
    public class AttackOperation : IAwaitable<AsyncExtensions.Void>
    {
        public event Action OnComplete;

        private readonly AttackCommandExecutor _attackCommandExecutor;
        private readonly IHealthHolder _selfHealth;
        private readonly IAttackData _data;
        private readonly IAttackable _target;

        private bool _isCancelled;

        public AttackOperation(
            AttackCommandExecutor attackCommandExecutor,
            IHealthHolder selfHealth,
            IAttackData data,
            IAttackable target)
        {
            _attackCommandExecutor = attackCommandExecutor;
            _selfHealth = selfHealth;
            _data = data;
            _target = target;      
        }

        public void Start()
        {
            var thread = new Thread(AttackAlgorythm);
            thread.Start();
        }

        public IAwaiter<AsyncExtensions.Void> GetAwaiter()
        {
            return new AttackOperationAwaiter(this);
        }

        public void Cancel()
        {
            _isCancelled = true;
            OnComplete?.Invoke();
        }

        private void AttackAlgorythm(object obj)
        {
            while (true)
            {
                if (CheckForProcessEnd() == true)
                {
                    OnComplete?.Invoke();
                    return;
                }

                var selfPosition = default(Vector3);
                var selfRotation = default(Quaternion);
                var targetPosition = default(Vector3);

                lock (_attackCommandExecutor)
                {
                    selfPosition = _attackCommandExecutor.SelfPosition;
                    selfRotation = _attackCommandExecutor.SelfRotation;
                    targetPosition = _attackCommandExecutor.TargetPosition;
                }

                var attackVector = targetPosition - selfPosition;
                var distanceToTarget = attackVector.magnitude;

                if (distanceToTarget > _data.AttackDistance)
                {
                    var finalDestination
                        = targetPosition - attackVector.normalized * (_data.AttackDistance * 0.9f);

                    _attackCommandExecutor
                        .TargetPositions
                        .OnNext(finalDestination);
                    
                    Thread.Sleep(50);
                }
                else if(selfRotation != Quaternion.LookRotation(attackVector))
                {
                    _attackCommandExecutor
                        .TargetRotations
                        .OnNext(Quaternion.LookRotation(attackVector));
                }
                else
                {
                    _attackCommandExecutor.AttackTargets.OnNext(_target);
                    Thread.Sleep(_data.AttackingPeriod);
                }
            }
        }


        private bool CheckForProcessEnd()
        {
            if (_attackCommandExecutor == null) return true;
            if (_selfHealth.CurrentHealth <= 0) return true;
            if (_target.CurrentHealth <= 0) return true;
            if (_isCancelled) return true;
            return false;
        }

    }
}
