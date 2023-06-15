using Zenject;
using UnityEngine;
using RTSPrototype.Abstractions;
using RTSPrototype.UIModel.CommandRealization;
using RTSPrototype.Abstractions.Commands.CommandInterfaces;

namespace RTSPrototype.UIModel.CommandCreators
{
    public class PatrolCommandCommandCreator : CancellableCommandCreatorBase<IPatrolCommand, Vector3>
    {
        [Inject] private IRTSValue<ISelectable> _selectable;

        protected override IPatrolCommand CreateCommand(Vector3 argument) 
            => new PatrolCommand(_selectable.CurrentValue.PivotPoint.position, argument);
    }
}
