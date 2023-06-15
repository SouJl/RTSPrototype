using System.Linq;
using System.Collections.Generic;

namespace RTSPrototype.Utils
{
    public sealed class FactionGameState
    {
        public static int FactionsCount
        {
            get
            {
                lock (_membersCount)
                {
                    return _membersCount.Count;
                }
            }
        }

        public static int GetWinner()
        {
            lock (_membersCount)
            {
                return _membersCount.Keys.First();
            }
        }

        private static Dictionary<int, List<int>> _membersCount = new Dictionary<int, List<int>>();


        public static void Register(int factionId, int memberId)
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

        public static void Unregister(int factionId, int memberId)
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

    }
}
