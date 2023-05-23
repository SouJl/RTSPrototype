using System;
using UnityEngine;
using RTSPrototype.Abstractions;

namespace RTSPrototype.UserControlSystem
{
    [CreateAssetMenu(fileName = nameof(SelectedValue),  menuName = "RTSPrototype/" + nameof(SelectedValue))]
    public class SelectedValue : ScriptableObject
    {
        public ISelectable CurrentValue { get; set; }
        public event Action<ISelectable> OnSelected;

        public void SetValue(ISelectable value)
        {
            CurrentValue = value;
            OnSelected?.Invoke(value);
        }
    }
}
