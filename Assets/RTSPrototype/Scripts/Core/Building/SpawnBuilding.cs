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

        [SerializeField] private string _name;
        [SerializeField] private float _maxHealth;
        [SerializeField] private Sprite _icon;

        private float _currentHealth = 1200f;

        public Vector3 RallyPoint { get; set; }
    }
}


