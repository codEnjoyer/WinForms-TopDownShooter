using GameProject.Physics;
using GameProject.Properties;

namespace GameProject.Entities.Enemies
{
    internal class HeavyZombie : Enemy
    {
        internal HeavyZombie(Vector location) : base(location, Resources.HeavyZombie)
        {
            Speed = 2;

            Damage = 1;

            MaxHealth = 200 * 1000;
            Health = MaxHealth;

            Score = 3;
        }
    }
}
