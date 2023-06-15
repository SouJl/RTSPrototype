using UniRx;
using System;
using Zenject;
using UnityEngine;
using UnityEngine.AI;
using System.Threading;
using RTSPrototype.Utils;
using System.Threading.Tasks;
using RTSPrototype.Abstractions;
using RTSPrototype.Core.Operations;
using RTSPrototype.Abstractions.Commands;
using RTSPrototype.Abstractions.Commands.CommandInterfaces;

namespace RTSPrototype.Core.CommandExecutors
{
    public class PatrolCommandExecutor: CommandExecutorBase<IPatrolCommand>
    {
        [SerializeField] private AnimatorHandler _animator;
        [SerializeField] private MovementStopOperation _movementStop;
        [SerializeField] private StopCommandExecutor _stopCommandExecutor;

        [Inject] private IPauseHandler _pauseHandler;

        private NavMeshAgent _curentAgent;
        private IDisposable _pauseEvent;

        private void Start()
        {
            _pauseEvent = _pauseHandler.IsPaused.Subscribe(OnPause);
        }

        private void OnDestroy()
        {
            _pauseEvent.Dispose();
        }

        private void Awake()
        {
            _curentAgent = GetComponent<NavMeshAgent>();

            if (TryGetComponent<MovementStopOperation>(out var movementStop))
            {
                _movementStop ??= movementStop;
            }
            else
            {
                Debug.Log($"Cant't find UnitMovementStop on {name}");
            }

            if (TryGetComponent<StopCommandExecutor>(out var stopCommandExecutor))
            {
                _stopCommandExecutor ??= stopCommandExecutor;
            }
            else
            {
                Debug.Log($"Cant't find StopCommandExecutor on {name}");
            }
        }

        public override async Task ExecuteSpecificCommand(IPatrolCommand command) => 
            await ExecutePatrol(command);

        private async Task ExecutePatrol(IPatrolCommand command)
        {
            var startPoint = command.StartPosition;
            var endPoint = command.EndPosition;

            while (true)
            {
                _curentAgent.destination = endPoint;
                
                Vector3 targetDirection = endPoint - transform.position;
                transform.rotation = Quaternion.LookRotation(targetDirection);

                _animator.ChangeState(AnimationType.Walk);
                _stopCommandExecutor.CancellationTokenSource = new CancellationTokenSource();
                try
                {
                    await _movementStop
                        .RunWithCancellation(
                        _stopCommandExecutor
                        .CancellationTokenSource
                        .Token);
                }
                catch
                {
                    DoOnStop();
                    break;
                }

                var temp = startPoint;
                startPoint = endPoint;
                endPoint = temp;
                
                try
                {
                    _animator.ChangeState(AnimationType.Idle);
                    await Task.Delay(1000, _stopCommandExecutor.CancellationTokenSource.Token);
                }
                catch
                {
                    DoOnStop();
                    break;
                }
            }

            _stopCommandExecutor.CancellationTokenSource = null;
            _animator.ChangeState(AnimationType.Idle);
        }

        private void DoOnStop()
        {
            _curentAgent.isStopped = true;
            _curentAgent.ResetPath();
        }


        private void OnPause(bool pasue)
        {
            _curentAgent.isStopped = pasue;
        }
    }
}
