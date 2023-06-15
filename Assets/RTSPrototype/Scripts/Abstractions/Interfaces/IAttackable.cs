namespace RTSPrototype.Abstractions
{
    public interface IAttackable : IHealthHolder
    {
        float AttackDistanceOffset { get; }
        void RecieveDamage(int amount);
    }
}
