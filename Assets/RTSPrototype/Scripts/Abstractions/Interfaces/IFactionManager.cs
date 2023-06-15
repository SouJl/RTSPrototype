namespace RTSPrototype.Abstractions
{
    public interface IFactionManager
    {
        int FactionsCount { get; }

        void Register(int factionId, int memberId);

        void Unregister(int factionId, int memberId);

        int GetWinner();
    }
}
