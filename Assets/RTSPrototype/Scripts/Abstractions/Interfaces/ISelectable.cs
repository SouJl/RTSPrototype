using UnityEngine;

namespace RTSPrototype.Abstractions 
{
    public interface ISelectable: IHealthHolder
    {
        Transform PivotPoint { get; }

        Sprite Icon { get; }
    }
}
