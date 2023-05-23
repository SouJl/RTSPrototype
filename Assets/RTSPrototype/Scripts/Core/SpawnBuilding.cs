using UnityEngine;
using RTSPrototype.Abstractions;

namespace RTSPrototype.Core 
{
    public class SpawnBuilding : MonoBehaviour, IUnitProducer, ISelectable
    {
        [Header("Main Settings")]
        [SerializeField] private GameObject _unitPrefab;
        [SerializeField] private Transform _unitsParent;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private float _minRange = -10f;
        [SerializeField] private float _maxRange = 10f;

        [Header("Selectable Settings")]
        [SerializeField] private float _maxHealth;
        [SerializeField] private Sprite _icon;

        #region ISelectable

        public float CurrentHealth => _currentHealth;

        public float MaxHealth => _maxHealth;

        public Sprite Icon => _icon;

        #endregion

        private float _currentHealth = 1200f;

        public void ProduceUnit() 
        {
            var spawnPos = new Vector3(
                _spawnPoint.position.x + Random.Range(_minRange, _maxRange),
                0,
                _spawnPoint.position.z + Random.Range(_minRange, _maxRange));

            Instantiate(
                _unitPrefab,
                spawnPos, 
                Quaternion.identity,
                _unitsParent);

        }

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            if (!_spawnPoint) return;

            UnityEditor.Handles.color = Color.red;
            UnityEditor.Handles.DrawWireDisc(
                _spawnPoint.position,
                _spawnPoint.transform.up, 
                (_maxRange - _minRange) / 2f);
        }

#endif

    }
}


