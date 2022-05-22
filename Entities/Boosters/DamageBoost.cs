using GameProject.Physics;
using GameProject.Properties;

namespace GameProject.Entities
{
    internal class DamageBoost : Booster
    {
        internal DamageBoost(Vector location) : base(location, Resources.DamageBoost)
        {
            Type = BoosterTypes.DamageBoost;
            Impact = 5;
        }
    }
}
