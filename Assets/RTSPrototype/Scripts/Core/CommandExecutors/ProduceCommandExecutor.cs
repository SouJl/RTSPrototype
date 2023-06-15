using UniRx;
using Zenject;
using UnityEngine;
using RTSPrototype.Utils;
using System.Threading.Tasks;
using RTSPrototype.Abstractions;
using RTSPrototype.Core.Building;
using RTSPrototype.Abstractions.Commands;
using RTSPrototype.Core.CommandRealization;
using RTSPrototype.Abstractions.Commands.CommandInterfaces;

namespace RTSPrototype.Core.CommandExecutors
{
    public class ProduceCommandExecutor : CommandExecutorBase<IProduceUnitCommand>, IUnitProducer
    {
        public IReadOnlyReactiveCollection<IUnitProductionTask> Queue => _queue;

        [SerializeField] private Transform _unitsParent;
        [SerializeField] private Transform _spawnPosition;
        [SerializeField] private Transform _startPosition;


        [Inject] private DiContainer _diContainer;

        private ReactiveCollection<IUnitProductionTask> _queue = new ReactiveCollection<IUnitProductionTask>();

        private void Start()
        {
            GetComponent<SpawnBuilding>().RallyPoint = _startPosition.position;
            Observable.EveryUpdate().Subscribe(_ => OnUpdate());
        }

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
                        _spawnPosition.rotation,
                        _unitsParent);

                var queue = instance.GetComponent<ICommandsQueue>();
                var factionMember = instance.GetComponent<IFactionMember>();
              
                var mainBuilding = GetComponent<SpawnBuilding>();
                var selfFaction = GetComponent<IFactionMember>();
                
                factionMember.SetFactionId(selfFaction.FactionId);
                factionMember.SetFactionColor(selfFaction.FactionColor);

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
