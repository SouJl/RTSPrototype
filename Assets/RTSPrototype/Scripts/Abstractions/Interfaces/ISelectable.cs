using UnityEngine;

namespace RTSPrototype.Abstractions 
{
    public interface ISelectable: IHealthHolder, IIconHandler
    {
        Transform PivotPoint { get; }
    }
}
