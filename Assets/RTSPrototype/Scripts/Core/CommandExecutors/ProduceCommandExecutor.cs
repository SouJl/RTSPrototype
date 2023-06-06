using RTSPrototype.Abstractions.Commands.CommandInterfaces;
using RTSPrototype.Abstractions.Commands;
using Zenject;
using UnityEngine;
using RTSPrototype.Abstractions;
using UniRx;

namespace RTSPrototype.Core.CommandExecutors
{
    public class ProduceCommandExecutor : CommandExecutorBase<IProduceUnitCommand>, IUnitProducer
    {
        public IReadOnlyReactiveCollection<IUnitProductionTask> Queue => _queue;

        [SerializeField] private Transform _unitsParent;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private float _minRange = -5f;
        [SerializeField] private float _maxRange = 5f;
        [SerializeField] private int _maximumUnitsInQueue = 6;

        [Inject] private DiContainer _diContainer;

        private ReactiveCollection<IUnitProductionTask> _queue = new ReactiveCollection<IUnitProductionTask>();
   
        private void Start() => Observable.EveryUpdate().Subscribe(_ => OnUpdate());

        private void OnUpdate()
        {
            if (_queue.Count == 0)
            {
                return;
            }

            var innerTask = (UnitProductionTask)_queue[0];

            innerTask.TimeLeft -= Time.deltaTime;

            if (innerTask.TimeLeft <= 0)
            {
                removeTaskAtIndex(0);

                var spawnPos = new Vector3(
                    _spawnPoint.position.x + Random.Range(_minRange, _maxRange),
                    0,
                    _spawnPoint.position.z + Random.Range(_minRange, _maxRange));

                   _diContainer.InstantiatePrefab(
                       innerTask.UnitPrefab,
                       spawnPos,
                       Quaternion.identity,
                       _unitsParent);
            }

        }

        public override void ExcecuteSpecifiedCommand(IProduceUnitCommand command) =>
            ProduceUnit(command);
        
        public void Cancel(int index) => removeTaskAtIndex(index);

        private void ProduceUnit(IProduceUnitCommand command)
        {
            if (_queue.Count == _maximumUnitsInQueue)
            {
                return;
            }
            _queue.Add(new UnitProductionTask(command.Data));

        }

        private void removeTaskAtIndex(int index)
        {
            for (int i = index; i < _queue.Count - 1; i++)
            {
                _queue[i] = _queue[i + 1];
            }
            _queue.RemoveAt(_queue.Count - 1);
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
