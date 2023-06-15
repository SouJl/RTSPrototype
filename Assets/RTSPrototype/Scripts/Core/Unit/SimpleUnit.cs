using UnityEngine;
using RTSPrototype.Abstractions;
using System.Threading.Tasks;
using RTSPrototype.Core.CommandExecutors;
using RTSPrototype.Core.CommandRealization;

namespace RTSPrototype.Core.Unit
{
    public class SimpleUnit : MonoBehaviour, IUnit, IAttackable, IDamageDealer
    {
        #region Interface implementation fields

        public float CurrentHealth => _currentHealth;
        public float MaxHealth => _maxHealth;
        public Sprite Icon => _icon;

        public string Name => _name;
        public Transform PivotPoint => gameObject.transform;

        public int Damage => _damage;

        public float AttackDistanceOffset => _attackDistanceOffset;

        #endregion

        [SerializeField] private string _name;
        [SerializeField] private float _maxHealth = 100f;
        [SerializeField] private Sprite _icon;
        [SerializeField] private float _attackDistanceOffset;
        [SerializeField] private int _damage = 25;
        [SerializeField] private AnimatorHandler _animator;
        [SerializeField] private StopCommandExecutor _stopCommand;

        private float _currentHealth;

        private void Start()
        {
            _currentHealth = _maxHealth;
        }

        public async void RecieveDamage(int amount)
        {
            if (_currentHealth <= 0)
            {
                return;
            }
            _currentHealth -= amount;
            if (_currentHealth <= 0)
            {
                await UnitDestoy();
            }
        }

        private async Task UnitDestoy()
        {
            if (this == null) return;

            await Task.Delay(100);
         
            await _stopCommand.ExecuteSpecificCommand(new StopCommand());
         
            await Task.Delay(100);

            _animator.ChangeState(AnimationType.Death);
            
            int delayTime = (int)(_animator.GetCurrentAnimationLength() + 0.5f) * 1000;
            await Task.Delay(delayTime);
            
            Destroy(gameObject);
        }
    }
}
