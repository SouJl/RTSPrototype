using UnityEngine;
using RTSPrototype.Abstractions.Commands.CommandInterfaces;
using RTSPrototype.Utils;

namespace RTSPrototype.UIModel.CommandRealization 
{
    public class ProduceUnitCommand : IProduceUnitCommand
    {
        
        [InjectAsset("Knight")] private GameObject _unitPrefab;
        public GameObject UnitPrefab => _unitPrefab;
    }
}


