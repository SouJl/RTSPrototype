using System;
using System.Threading;
using System.Threading.Tasks;
using RTSPrototype.Abstractions;
using RTSPrototype.Abstractions.Commands;
using RTSPrototype.Abstractions.Commands.CommandInterfaces;
using RTSPrototype.Abstractions.ScriptableObjects;
using RTSPrototype.Core.Navigation;
using RTSPrototype.Utils;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace RTSPrototype.Core.CommandExecutors
{
    public class AttackCommandExecutor : CommandExecutorBase<IAttackCommand>
    {
        public Vector3 SelfPosition => _selfPosition;
        public Quaternion SelfRotation => _selfRotation;
        public Vector3 TargetPosition => _targetPosition;

        public Subject<Vector3> TargetPositions => _targetPositions;
        public Subject<Quaternion> TargetRotations => _targetRotations;
        public Subject<IAttackable> AttackTargets => _attackTargets;

        [SerializeField] private AnimatorHandler _animator;
        [SerializeField] private StopCommandExecutor _stopCommandExecutor;

        [Inject(Id = "Knight")] private IHealthHolder _selfHealth;
        [Inject(Id = "Knight")] private IAttackData _data;
 
        private Vector3 _selfPosition;
        private Quaternion _selfRotation;
        private Vector3 _targetPosition;

        private readonly Subject<Vector3> _targetPositions = new Subject<Vector3>();
        private readonly Subject<Quaternion> _targetRotations = new Subject<Quaternion>();
        private readonly Subject<IAttackable> _attackTargets = new Subject<IAttackable>();
        
        private Transform _targetTransform;
        private AttackOperation _currentAttackOp;

        [Inject]
        private void Init()
        {
            _targetPositions
                .Select(value => new Vector3(
                    (float)Math.Round(value.x, 2), 
                    (float)Math.Round(value.y, 2), 
                    (float)Math.Round(value.z, 2)))
                .Distinct()
                .ObserveOnMainThread()
                .Subscribe(StartMovingToPosition);
            _attackTargets
                .ObserveOnMainThread()
                .Subscribe(StartAttackingTargets);
            _targetRotations
                .ObserveOnMainThread()
                .Subscribe(SetAttackRoation);
        }

        private void SetAttackRoation(Quaternion targetRotation) 
            => transform.rotation = targetRotation;

        private void StartAttackingTargets(IAttackable target)
        {
            GetComponent<NavMeshAgent>().isStopped = true;
            GetComponent<NavMeshAgent>().ResetPath();

            _animator.SetBoolAnimation("IsWalk", false);
            _animator.SetTriggerAnimation("PlayAttack");

            target.RecieveDamage(GetComponent<IDamageDealer>().Damage);
        }

        private void StartMovingToPosition(Vector3 position)
        {
            GetComponent<NavMeshAgent>().destination = position;

            _animator.SetBoolAnimation("IsWalk", true);
        }

        public override async Task ExecuteSpecificCommand(IAttackCommand command) =>
             await ExecuteAttack(command);

        private async Task ExecuteAttack(IAttackCommand command) 
        {
            _targetTransform = (command.Target as Component).transform;
             _currentAttackOp = new AttackOperation(this, _selfHealth, _data, command.Target);
            _stopCommandExecutor.CancellationTokenSource = new CancellationTokenSource();
            UpdatePositionsData();
            _currentAttackOp.Start();
            try
            {                
                await _currentAttackOp
                    .RunWithCancellation(
                    _stopCommandExecutor
                    .CancellationTokenSource
                    .Token);
            }
            catch
            {
                _currentAttackOp.Cancel();
            }

            _animator.SetBoolAnimation("IsWalk", false);
            _animator.SetTriggerAnimation("PlayIdle");

            _currentAttackOp = null;
            _targetTransform = null;
            _stopCommandExecutor.CancellationTokenSource = null;
        }

        private void Update()
        {
            if (_currentAttackOp == null)
            {
                return;
            }

            UpdatePositionsData();
        }

        private void UpdatePositionsData()
        {
            _selfPosition = transform.position;
            _selfRotation = transform.rotation;
            if (_targetTransform != null)
            {
                _targetPosition = _targetTransform.position;
            }
        }
    }
}
