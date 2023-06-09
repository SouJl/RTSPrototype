﻿using UniRx;
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
using RTSPrototype.Abstractions.ScriptableObjects;
using RTSPrototype.Abstractions.Commands.CommandInterfaces;

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

            _animator.ChangeState(AnimationType.Attack);

            target.RecieveDamage(GetComponent<IDamageDealer>().Damage);
        }

        private void StartMovingToPosition(Vector3 position)
        {
            GetComponent<NavMeshAgent>().destination = position;

            _animator.ChangeState(AnimationType.Walk);
        }

        public override async Task ExecuteSpecificCommand(IAttackCommand command) =>
             await ExecuteAttack(command);

        private async Task ExecuteAttack(IAttackCommand command) 
        {
            var targetComponent = command.Target as Component;
            
            if (targetComponent.GetComponent<IFactionMember>().FactionId 
                == GetComponent<IFactionMember>().FactionId) 
            {
                return;
            }   
            
            _targetTransform = targetComponent.transform;
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

            _animator.ChangeState(AnimationType.Idle);

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
