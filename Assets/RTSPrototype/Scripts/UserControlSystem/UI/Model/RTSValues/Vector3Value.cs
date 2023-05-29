using System;
using RTSPrototype.Abstractions;
using UnityEngine;

namespace RTSPrototype.UIModel
{
    public class Vector3Value : RTSValueBase<Vector3>
    {
        public override event Action<Vector3> OnNewValue;

        public override void SetValue(Vector3 value)
        {
            CurrentValue = value;
            OnNewValue?.Invoke(value);
        }
    }
}
