using System;
using RTSPrototype.Abstractions;
using RTSPrototype.Abstractions.Commands.CommandInterfaces;
using RTSPrototype.UIModel.CommandRealization;
using RTSPrototype.Utils;
using UnityEngine;
using Zenject;

namespace RTSPrototype.UIModel.CommandCreators
{
    public class PatrolCommandCommandCreator : CommandCreatorBase<IPatrolCommand>
    {
        [Inject] private AssetsContext _context;
        [Inject] private IRTSValue<ISelectable> _selectable;

        private Action<IPatrolCommand> _creationCallback;

        public override void ProcessCancel()
        {
            base.ProcessCancel();
            _creationCallback = null;
        }

        [Inject]
        private void Init(IRTSValue<Vector3> patrolClicks)
        {
            patrolClicks.OnNewValue += OnNewValue;
        }

        private void OnNewValue(Vector3 patrol)
        {
            _creationCallback?.Invoke(_context
                .Inject(new PatrolCommand(
                    _selectable.CurrentValue.PivotPoint.position, 
                    patrol)));

            _creationCallback = null;
        }

        protected override void classSpecificCommandCreation(Action<IPatrolCommand> creationCallback)
        {
            _creationCallback = creationCallback;
        }
    }
}
