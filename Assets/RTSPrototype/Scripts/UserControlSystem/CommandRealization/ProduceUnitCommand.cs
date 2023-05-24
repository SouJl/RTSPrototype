using UnityEngine;
using RTSPrototype.Abstractions.Commands.CommandInterfaces;

namespace RTSPrototype.UserControlSystem.CommandRealization 
{
    public class ProduceUnitCommand : IProduceUnitCommand
    {
        [SerializeField] private GameObject _unitPrefab;
        public GameObject UnitPrefab => _unitPrefab;
    }
}


