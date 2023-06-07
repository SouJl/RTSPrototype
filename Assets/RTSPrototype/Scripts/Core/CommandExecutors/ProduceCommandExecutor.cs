using RTSPrototype.Abstractions.Commands.CommandInterfaces;
using RTSPrototype.Abstractions.Commands;
using Zenject;
using UnityEngine;
using RTSPrototype.Abstractions;
using UniRx;
using RTSPrototype.Utils;
using System.Threading.Tasks;
using RTSPrototype.Core.Building;
using RTSPrototype.Core.CommandRealization;

namespace RTSPrototype.Core.CommandExecutors
{
    public class ProduceCommandExecutor : CommandExecutorBase<IProduceUnitCommand>, IUnitProducer
    {
        public IReadOnlyReactiveCollection<IUnitProductionTask> Queue => _queue;

        [SerializeField] private Transform _unitsParent;
        [SerializeField] private Transform _spawnPosition;
        [SerializeField] private float _minRange = -5f;
        [SerializeField] private float _maxRange = 5f;

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

                var instance 
                    = _diContainer.InstantiatePrefab(
                        innerTask.UnitPrefab,
                        _spawnPosition.position, 
                        Quaternion.identity,
                        _unitsParent);

                var queue = instance.GetComponent<ICommandsQueue>();
                var mainBuilding = GetComponent<SpawnBuilding>();

                queue.EnqueueCommand(new MoveCommand(mainBuilding.RallyPoint));
            }

        }

        public override async Task ExecuteSpecificCommand(IProduceUnitCommand command) =>
            ProduceUnit(command);
        
        public void Cancel(int index) => removeTaskAtIndex(index);

        private void ProduceUnit(IProduceUnitCommand command)
        {
            if (_queue.Count == Const.MaximumUnitsInQueue)
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
    }
}
