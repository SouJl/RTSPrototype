using UnityEngine;

namespace RTSPrototype.Abstractions 
{
    public interface ISelectable
    {
        float CurrentHealth { get; }
        float MaxHealth { get; }
        Sprite Icon { get; }

    }
}
