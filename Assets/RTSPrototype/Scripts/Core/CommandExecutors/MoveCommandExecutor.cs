using System.Threading;
using System.Threading.Tasks;
using RTSPrototype.Abstractions;
using RTSPrototype.Abstractions.Commands;
using RTSPrototype.Abstractions.Commands.CommandInterfaces;
using RTSPrototype.Core.Navigation;
using RTSPrototype.Utils;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace RTSPrototype.Core.CommandExecutors
{
    public class MoveCommandExecutor : CommandExecutorBase<IMoveCommand>, IPaused
    { 
        [SerializeField] private AnimatorHandler _animatorHandler;
        [SerializeField] private UnitMovementStop _movementStop;
        [SerializeField] private StopCommandExecutor _stopCommandExecutor;
        private NavMeshAgent _curentAgent;

        [Inject]
        private void Init(IPauseHandler pauseHandler) 
        {
            pauseHandler.Register(this);
        }

        private void Awake()
        {
            _curentAgent = GetComponent<NavMeshAgent>();

            if (TryGetComponent<UnitMovementStop>(out var movementStop))
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

        public override void ExcecuteSpecifiedCommand(IMoveCommand command) => 
            ExceuteMove(command);

        private async void ExceuteMove(IMoveCommand command)
        {
            _curentAgent.destination = command.TargetPosition;
            _animatorHandler.SetBoolAnimation("IsWalk", true);
            _stopCommandExecutor.CancellationTokenSource = new CancellationTokenSource();
            try
            {
                await _movementStop
                    .WithCancellation(
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
            _animatorHandler.SetBoolAnimation("IsWalk", false);
        }

        public void SetPause(bool isPaused)
        {
            _curentAgent.isStopped = isPaused;
        }
    }
}
