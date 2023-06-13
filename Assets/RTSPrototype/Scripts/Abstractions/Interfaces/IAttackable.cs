namespace RTSPrototype.Abstractions
{
    public interface IAttackable : IHealthHolder
    {
        void RecieveDamage(int amount);
    }
}
