using UnityEngine;
using RTSPrototype.Abstractions;
using RTSPrototype.Abstractions.Commands;
using RTSPrototype.Abstractions.Commands.CommandInterfaces;

namespace RTSPrototype.Core 
{
    public class SpawnBuilding : CommandExecutorBase<IProduceUnitCommand>, ISelectable, IAttackable
    {
        [Header("Main Settings")]
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


        public override void ExcecuteSpecifiedCommand(IProduceUnitCommand command) => 
            ProduceUnit(command);


        private void ProduceUnit(IProduceUnitCommand command)
        {
            var spawnPos = new Vector3(
                _spawnPoint.position.x + Random.Range(_minRange, _maxRange),
                0,
                _spawnPoint.position.z + Random.Range(_minRange, _maxRange));

            Instantiate(
                command.UnitPrefab,
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


