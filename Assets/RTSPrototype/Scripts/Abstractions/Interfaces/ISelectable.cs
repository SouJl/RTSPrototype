using UnityEngine;

namespace RTSPrototype.Abstractions 
{
    public interface ISelectable: IHealthHolder
    {
        Sprite Icon { get; }
    }
}
