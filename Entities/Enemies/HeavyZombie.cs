using GameProject.Physics;
using GameProject.Properties;

namespace GameProject.Entities.Enemies
{
    internal class HeavyZombie : Enemy
    {
        internal HeavyZombie(Vector location) : base(location, Resources.HeavyZombie)
        {
            Speed = 2;
            //MaxSpeed = 2 * Speed;

            Damage = 15;
            //MaxDamage = 2 * Damage;

            MaxHealth = 200 * 1000;
            Health = MaxHealth;

            Value = 3;
        }
    }
}
