using UnityEngine;

namespace RTSPrototype.Abstractions
{
    public interface IOutlineSelector
    {
        void ChangeState(bool outlineState);

        void ChangeColor(Color newColor);
    }
}
