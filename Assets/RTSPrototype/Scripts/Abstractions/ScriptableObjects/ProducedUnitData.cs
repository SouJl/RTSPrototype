using UnityEngine;

namespace RTSPrototype.Abstractions.ScriptableObjects
{
    public interface IProducedUnitData
    {
        string UnitName { get; }
        float ProductionTime { get; }
        Sprite Icon { get; }
        GameObject UnitPrefab { get; }
    }

    [CreateAssetMenu(fileName = nameof(ProducedUnitData), menuName = "RTSPrototype/Data/" + nameof(ProducedUnitData))]
    public class ProducedUnitData : ScriptableObject, IProducedUnitData
    {
        [field: SerializeField] public string UnitName { get; private set; }

        [field: SerializeField] public Sprite Icon { get; private set; }

        [field: SerializeField] public float ProductionTime { get; private set; }

        [field: SerializeField] public GameObject UnitPrefab { get; private set; }
    }
}
