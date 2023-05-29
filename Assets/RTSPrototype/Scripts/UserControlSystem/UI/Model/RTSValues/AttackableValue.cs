using System;
using RTSPrototype.Abstractions;

namespace RTSPrototype.UIModel
{
    public class AttackableValue : RTSValueBase<IAttackable>
    {
        public override event Action<IAttackable> OnNewValue;

        public override void SetValue(IAttackable value)
        {
            CurrentValue = value;
            OnNewValue?.Invoke(value);
        }
    }
}
