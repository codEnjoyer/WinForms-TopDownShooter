using GameProject.Physics;
using GameProject.Properties;

namespace GameProject.Entities
{
    internal class DamageBooster : Booster
    {
        internal DamageBooster(Vector location) : base(location, Resources.DamageBoost)
        {
            Type = BoosterTypes.DamageBoost;
            Impact = 5;
            Time = 10 * 1000;
        }
    }
}
