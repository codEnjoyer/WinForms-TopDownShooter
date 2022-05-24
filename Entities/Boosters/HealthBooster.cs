using GameProject.Physics;
using GameProject.Properties;

namespace GameProject.Entities
{
    internal class HealthBooster : Booster
    {
        internal HealthBooster(Vector location) : base(location, Resources.HealthBoost)
        {
            Type = BoosterTypes.HealthBoost;
            Impact = 25;
            Time = 5 * 1000;
        }
    }
}
