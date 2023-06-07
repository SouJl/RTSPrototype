using UnityEngine;
using RTSPrototype.Abstractions;

namespace RTSPrototype.Core.Unit
{
    public class SimpleUnit : MonoBehaviour, ISelectable
    {
        #region ISelectable

        public float CurrentHealth => _currentHealth;
        public float MaxHealth => _maxHealth;
        public Sprite Icon => _icon;

        public string Name => _name;
        public Transform PivotPoint => gameObject.transform;
 
        #endregion

        [SerializeField] private string _name;
        [SerializeField] private float _maxHealth = 100f;
        [SerializeField] private Sprite _icon;

        private float _currentHealth = 100f;
    }
}
