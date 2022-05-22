using GameProject.Physics;
using GameProject.Properties;

namespace GameProject.Entities
{
    internal class HealthBoost : Booster
    {
        internal HealthBoost(Vector location) : base(location, Resources.HealthBoost)
        {
            Type = BoosterTypes.HealthBoost;
            Impact = 25;
        }
    }
}
