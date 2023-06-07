using System.Threading.Tasks;
using RTSPrototype.Abstractions.Commands;
using RTSPrototype.Abstractions.Commands.CommandInterfaces;
using RTSPrototype.Core.Building;
using UnityEngine;

namespace RTSPrototype.Core.CommandExecutors
{
    public class SetRallyPointCommandExecutor : CommandExecutorBase<ISetRallyPointCommand>
    {
        private Vector3 _rallyPoint;

        public override async Task ExecuteSpecificCommand(ISetRallyPointCommand command)
        {
            _rallyPoint = command.RallyPoint;
            GetComponent<SpawnBuilding>().RallyPoint = command.RallyPoint;
        }


#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            UnityEditor.Handles.color = Color.red;
            UnityEditor.Handles.DrawWireDisc(_rallyPoint, Vector3.up, 0.5f);
        }
#endif
    }
}
