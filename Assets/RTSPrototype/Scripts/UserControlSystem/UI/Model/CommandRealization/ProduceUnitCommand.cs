using RTSPrototype.Abstractions.Commands.CommandInterfaces;
using Zenject;
using RTSPrototype.Abstractions.ScriptableObjects;

namespace RTSPrototype.UIModel.CommandRealization 
{
    public class ProduceUnitCommand : IProduceUnitCommand
    {
        [Inject(Id = "Knight")] private IProducedUnitData _data;

        public IProducedUnitData Data => _data;
    }
}


