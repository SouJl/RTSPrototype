using UnityEngine;
using RTSPrototype.Abstractions;

namespace RTSPrototype.Core
{
    public class SimpleUnit : MonoBehaviour, ISelectable
    {
        public float CurrentHealth => _currentHealth;
        public float MaxHealth => _maxHealth;
        public Sprite Icon => _icon;

        public Transform PivotPoint => gameObject.transform;


        [SerializeField] private float _maxHealth = 100f;
        [SerializeField] private Sprite _icon;

        private float _currentHealth = 100f;
    }
}
