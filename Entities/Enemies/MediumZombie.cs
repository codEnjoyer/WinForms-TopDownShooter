using GameProject.Physics;
using GameProject.Properties;

namespace GameProject.Entities.Enemies
{
    internal class MediumZombie : Enemy
    {
        internal MediumZombie(Vector location) : base(location, Resources.MediumZombie)
        {
            Speed = 3;

            Damage = 15;

            MaxHealth = 100 * 1000;
            Health = MaxHealth;

            Score = 2;
        }
    }
}
