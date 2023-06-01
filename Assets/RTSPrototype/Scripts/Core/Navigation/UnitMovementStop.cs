using System;
using RTSPrototype.Utils;
using UnityEngine;
using UnityEngine.AI;

namespace RTSPrototype.Core.Navigation
{
    public class UnitMovementStop : MonoBehaviour, IAwaitable<AsyncExtensions.Void>
    {
        public event Action OnStop;
        
        [SerializeField] private NavMeshAgent _agent;

        private void OnValidate()
        {
            _agent ??= GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (!_agent.pathPending)
            {
                if (_agent.remainingDistance <= _agent.stoppingDistance)
                {
                    if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)
                    {
                        OnStop?.Invoke();
                    }
                }
            }
        }

        public IAwaiter<AsyncExtensions.Void> GetAwaiter() => new StopAwaiter(this);
    }
}
