using System;
using System.Threading;
using System.Threading.Tasks;
using RTSPrototype.Abstractions;
using RTSPrototype.Abstractions.Commands;
using RTSPrototype.Abstractions.Commands.CommandInterfaces;
using RTSPrototype.Core.Operations;
using RTSPrototype.Utils;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace RTSPrototype.Core.CommandExecutors
{
    public class MoveCommandExecutor : CommandExecutorBase<IMoveCommand>
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
    
        public override async Task ExecuteSpecificCommand(IMoveCommand command) => 
             await ExceuteMove(command);

        private async Task ExceuteMove(IMoveCommand command)
        {
            _curentAgent.destination = command.TargetPosition;
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
                _curentAgent.isStopped = true;
                _curentAgent.ResetPath();
            }
            _stopCommandExecutor.CancellationTokenSource = null;
            _animator.ChangeState(AnimationType.Idle);
        }

        private void OnPause(bool isPaused)
        {
            _curentAgent.isStopped = isPaused;
        }
    }
}
