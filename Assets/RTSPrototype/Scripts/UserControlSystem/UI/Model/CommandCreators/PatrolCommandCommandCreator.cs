using RTSPrototype.Abstractions;
using RTSPrototype.Abstractions.Commands.CommandInterfaces;
using RTSPrototype.UIModel.CommandRealization;
using UnityEngine;
using Zenject;

namespace RTSPrototype.UIModel.CommandCreators
{
    public class PatrolCommandCommandCreator : CancellableCommandCreatorBase<IPatrolCommand, Vector3>
    {
        [Inject] private IRTSValue<ISelectable> _selectable;

        protected override IPatrolCommand CreateCommand(Vector3 argument) 
            => new PatrolCommand(_selectable.CurrentValue.PivotPoint.position, argument);
    }
}
