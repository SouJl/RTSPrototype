using RTSPrototype.Abstractions.Commands.CommandInterfaces;
using RTSPrototype.Abstractions.Commands;
using Zenject;
using UnityEngine;
using System.Threading.Tasks;

namespace RTSPrototype.Core.CommandExecutors
{
    public class ProduceCommandExecutor : CommandExecutorBase<IProduceUnitCommand>
    {
        [SerializeField] private Transform _unitsParent;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private float _minRange = -5f;
        [SerializeField] private float _maxRange = 5f;

        [Inject] private DiContainer _diContainer;

        public override void ExcecuteSpecifiedCommand(IProduceUnitCommand command) => 
            ProduceUnit(command);


        private async void ProduceUnit(IProduceUnitCommand command)
        {
            await SpawnDelay(1.5f);

            var spawnPos = new Vector3(
                _spawnPoint.position.x + Random.Range(_minRange, _maxRange),
                0,
                _spawnPoint.position.z + Random.Range(_minRange, _maxRange));

            _diContainer.InstantiatePrefab(
                command.UnitPrefab,
                spawnPos,
                Quaternion.identity,
                _unitsParent);
        }

        private async Task SpawnDelay(float delay)
        {
            var endTime = Time.time + delay;

            while (Time.time < endTime)
            {
                await Task.Yield();
            }
        }


#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            if (!_spawnPoint) return;

            UnityEditor.Handles.color = Color.red;
            UnityEditor.Handles.DrawWireDisc(
                _spawnPoint.position,
                _spawnPoint.transform.up,
                (_maxRange - _minRange) / 2f);
        }
#endif
    }
}
