using System;
using RTSPrototype.Abstractions;

namespace RTSPrototype.UIModel
{
    public class SelectedValue : RTSValueBase<ISelectable>
    {
        public override event Action<ISelectable> OnNewValue;

        public override void SetValue(ISelectable value)
        {
            CurrentValue = value;
            OnNewValue?.Invoke(value);
        }
    }
}
