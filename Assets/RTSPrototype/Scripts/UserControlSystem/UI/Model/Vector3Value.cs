using System;
using UnityEngine;

namespace RTSPrototype.UIModel
{
    [CreateAssetMenu(fileName = nameof(Vector3Value), menuName = "RTSPrototype/" + nameof(Vector3Value))]
    public class Vector3Value : ScriptableObject
    {
        public Vector3 CurrentValue { get; private set; }
        public Action<Vector3> OnNewValue;
        public void SetValue(Vector3 value)
        {
            CurrentValue = value;
            OnNewValue?.Invoke(value);
        }
    }
}
