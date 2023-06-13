using UnityEngine;

namespace RTSPrototype.Abstractions.ScriptableObjects
{
    public interface IAttackData
    {
       float AttackDistance { get; }
       int AttackingPeriod { get; }
    }

    [CreateAssetMenu(fileName = nameof(UnitAttackData), menuName = "RTSPrototype/Data/" + nameof(UnitAttackData))]
    public class UnitAttackData : ScriptableObject, IAttackData
    {
        [field: SerializeField] public float AttackDistance { get; private set; }

        [field: SerializeField] public int AttackingPeriod { get; private set; }
    }
}
