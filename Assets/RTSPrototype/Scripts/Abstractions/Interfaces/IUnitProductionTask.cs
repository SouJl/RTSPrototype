

namespace RTSPrototype.Abstractions
{
    public interface IUnitProductionTask : IIconHolder
    {
        public string UnitName { get; }
        public float ProductionTime { get; }

        public float TimeLeft { get; }
    }

}
