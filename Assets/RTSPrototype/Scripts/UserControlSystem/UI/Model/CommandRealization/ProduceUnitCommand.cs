using Zenject;
using RTSPrototype.Abstractions.ScriptableObjects;
using RTSPrototype.Abstractions.Commands.CommandInterfaces;

namespace RTSPrototype.UIModel.CommandRealization 
{
    public class ProduceUnitCommand : IProduceUnitCommand
    {
        [Inject(Id = "Knight")] private IProducedUnitData _data;

        public IProducedUnitData Data => _data;
    }
}


