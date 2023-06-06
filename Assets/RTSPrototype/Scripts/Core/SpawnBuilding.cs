using UnityEngine;
using RTSPrototype.Abstractions;

namespace RTSPrototype.Core 
{
    public class SpawnBuilding : MonoBehaviour, ISelectable, IAttackable
    {
        #region ISelectable

        public float CurrentHealth => _currentHealth;

        public float MaxHealth => _maxHealth;

        public Sprite Icon => _icon;

        public Transform PivotPoint => gameObject.transform;

        #endregion

        [SerializeField] private float _maxHealth;
        [SerializeField] private Sprite _icon;

        private float _currentHealth = 1200f;
    }
}


