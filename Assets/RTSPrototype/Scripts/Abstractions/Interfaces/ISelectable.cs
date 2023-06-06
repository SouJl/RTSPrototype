using UnityEngine;

namespace RTSPrototype.Abstractions 
{
    public interface ISelectable: IHealthHolder, IIconHandler
    {
        string Name { get; }
        Transform PivotPoint { get; }
    }
}
