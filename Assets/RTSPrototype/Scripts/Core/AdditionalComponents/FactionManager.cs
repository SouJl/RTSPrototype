using System.Linq;
using RTSPrototype.Abstractions;
using System.Collections.Generic;

namespace RTSPrototype.Core.AdditionalComponents
{
    public sealed class FactionManager : IFactionManager
    {

        private readonly Dictionary<int, List<int>> _membersCount 
            = new Dictionary<int, List<int>>();

        public int FactionsCount
        {
            get
            {
                lock (_membersCount)
                {
                    return _membersCount.Count;
                }
            }
        }

        public void Register(int factionId, int memberId)
        {
            lock (_membersCount)
            {
                if (!_membersCount.ContainsKey(factionId))
                {
                    _membersCount.Add(factionId, new List<int>());
                }
                if (!_membersCount[factionId].Contains(memberId))
                {
                    _membersCount[factionId].Add(memberId);
                }
            }
        }

        public void Unregister(int factionId, int memberId)
        {
            lock (_membersCount)
            {
                if (_membersCount[factionId].Contains(memberId))
                {
                    _membersCount[factionId].Remove(memberId);
                }
                if (_membersCount[factionId].Count == 0)
                {
                    _membersCount.Remove(factionId);
                }
            }
        }

        public int GetWinner()
        {
            lock (_membersCount)
            {
                return _membersCount.Keys.First();
            }
        }
    }
}
