using UnityEngine;

namespace RTSPrototype.Abstractions 
{
    public interface ISelectable: IHealthHolder, IIconHolder
    {
        string Name { get; }
        Transform PivotPoint { get; }
    }
}
