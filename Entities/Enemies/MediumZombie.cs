using GameProject.Physics;
using GameProject.Properties;

namespace GameProject.Entities.Enemies
{
    internal class MediumZombie : Enemy
    {
        internal MediumZombie(Vector location) : base(location, Resources.MediumZombie)
        {
            Speed = 3;
            //MaxSpeed = 2 * Speed;

            Damage = 15;
            //MaxDamage = 2 * Damage;

            MaxHealth = 100 * 1000;
            Health = MaxHealth;

            Value = 2;
        }
    }
}
