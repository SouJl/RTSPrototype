using UnityEngine;

namespace RTSPrototype.Abstractions
{
    public interface IFactionMember
    {
        int FactionId { get; }
        Color FactionColor { get; }

        void SetFactionId(int factionId);

        void SetFactionColor(Color factionColor);
    }
}
