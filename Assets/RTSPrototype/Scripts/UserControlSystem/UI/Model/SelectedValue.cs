using System;
using UnityEngine;
using RTSPrototype.Abstractions;

namespace RTSPrototype.UIModel
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
