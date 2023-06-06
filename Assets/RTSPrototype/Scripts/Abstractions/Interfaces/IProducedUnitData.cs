using UnityEngine;

namespace RTSPrototype.Abstractions
{
    public interface IProducedUnitData
    {
        string UnitName { get; }
        float ProductionTime { get; }
        Sprite Icon { get; }
        GameObject UnitPrefab { get; }
    }
}
