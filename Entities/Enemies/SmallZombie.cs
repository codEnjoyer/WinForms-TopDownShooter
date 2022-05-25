using GameProject.Physics;
using GameProject.Properties;

namespace GameProject.Entities
{
    internal class SmallZombie : Enemy
    {
        internal SmallZombie(Vector location) : base(location, Resources.SmallZombie)
        {
            Speed = 4;

            Damage = 10;

            MaxHealth = 50 * 1000;
            Health = MaxHealth;

            Score = 1;
        }
    }
}
