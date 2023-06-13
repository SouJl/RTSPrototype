using UnityEngine;
using RTSPrototype.Abstractions;

namespace RTSPrototype.Core.Building 
{
    public class SpawnBuilding : MonoBehaviour, ISelectable, IAttackable
    {
        #region ISelectable

        public float CurrentHealth => _currentHealth;

        public float MaxHealth => _maxHealth;

        public Sprite Icon => _icon;
       
        public string Name => _name;
        public Transform PivotPoint => gameObject.transform;

        #endregion
        public Vector3 RallyPoint { get; set; }

        [SerializeField] private string _name;
        [SerializeField] private float _maxHealth;
        [SerializeField] private Sprite _icon;

        private float _currentHealth;

        private void Start()
        {
            _currentHealth = _maxHealth;
        }

        public void RecieveDamage(int amount)
        {
            if (_currentHealth <= 0)
            {
                return;
            }
            _currentHealth -= amount;
            if (_currentHealth <= 0)
            {
                Destroy(gameObject);
            }

        }
    }
}


