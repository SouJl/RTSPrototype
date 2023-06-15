using UniRx;
using System;
using UnityEngine;
using UnityEngine.AI;
using RTSPrototype.Utils;
using RTSPrototype.Abstractions;

namespace RTSPrototype.Core.Operations
{
    public class MovementStopOperation : MonoBehaviour, IAwaitable<AsyncExtensions.Void>
    {     
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private CollisionDetector _collisionDetector;
        [SerializeField] private int _throttleFrames = 60;
        [SerializeField] private int _continuityThreshold = 10;

        private event Action OnComplete;

        private void OnValidate()
        {
            _agent ??= GetComponent<NavMeshAgent>();
        }

        private void Awake()
        {
            _collisionDetector.Collisions
                .Where(_ => _agent.hasPath)
                .Where(collision => collision.collider.GetComponentInParent<IUnit>() != null)
                .Select(_ => Time.frameCount)
                .Distinct()
                .Buffer(_throttleFrames)
                .Where(buffer =>
                {
                    for (int i = 1; i < buffer.Count; i++)
                    {
                        if (buffer[i] - buffer[i - 1] > _continuityThreshold)
                        {
                            return false;
                        }
                    }
                    return true;
                })
                .Subscribe(_ =>
                {
                    _agent.isStopped = true;
                    _agent.ResetPath();
                    OnComplete?.Invoke();
                })
                .AddTo(this);
        }

        private void Update()
        {
            if (!_agent.pathPending)
            {
                if (_agent.remainingDistance <= _agent.stoppingDistance)
                {
                    if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)
                    {
                        OnComplete?.Invoke();
                    }
                }
            }
        }

        public IAwaiter<AsyncExtensions.Void> GetAwaiter() => new MovementStopAwaiter(this);

        private sealed class MovementStopAwaiter : AwaiterBase<AsyncExtensions.Void>
        {
            private readonly MovementStopOperation _unitMovementStop;

            public MovementStopAwaiter(MovementStopOperation unitMovementStop)
            {
                _unitMovementStop = unitMovementStop;
                _unitMovementStop.OnComplete += OnStop;
            }

            private void OnStop()
            {
                _unitMovementStop.OnComplete -= OnStop;
                OnWaitFinish(new AsyncExtensions.Void());
            }
        }
    }
}
