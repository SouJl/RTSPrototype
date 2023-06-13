using UnityEngine;
using RTSPrototype.Abstractions;
using System.Collections;

namespace RTSPrototype.Core.Unit
{
    public class SimpleUnit : MonoBehaviour, IUnit, IAttackable, IDamageDealer
    {
        #region Interface implementation

        public float CurrentHealth => _currentHealth;
        public float MaxHealth => _maxHealth;
        public Sprite Icon => _icon;

        public string Name => _name;
        public Transform PivotPoint => gameObject.transform;

        public int Damage => _damage;

        #endregion

        [SerializeField] private string _name;
        [SerializeField] private float _maxHealth = 100f;
        [SerializeField] private Sprite _icon;
        [SerializeField] private int _damage = 25;
        [SerializeField] private AnimatorHandler _animator;

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
                StartCoroutine(ExecuteUnitDeath());
            }
        }

        private IEnumerator ExecuteUnitDeath()
        {
            _animator.SetTriggerAnimation("PlayDeath");
            yield return new WaitForSeconds(_animator.GetCurrentAnimationLength() + 0.5f);
            Destroy(gameObject);
        }
    }
}
