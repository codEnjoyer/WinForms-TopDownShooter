using GameProject.Physics;
using GameProject.Properties;

namespace GameProject.Entities
{
    internal class SpeedBoost : Booster
    {
        internal SpeedBoost(Vector location) : base(location, Resources.SpeedBoost)
        {
            Type = BoosterTypes.SpeedBoost;
            Impact = 3;
        }
    }
}
