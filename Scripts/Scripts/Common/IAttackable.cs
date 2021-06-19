namespace Assets.Scripts.Common
{
    public interface IAttackable
    {
        float Hitpoints { get; set; }

        void Attacked(float damage);
    }
}
