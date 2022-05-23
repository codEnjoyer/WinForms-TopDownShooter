using GameProject.Physics;
using GameProject.Properties;

namespace GameProject.Entities
{
    internal class SpeedBooster : Booster
    {
        internal SpeedBooster(Vector location) : base(location, Resources.SpeedBoost)
        {
            Type = BoosterTypes.SpeedBoost;
            Impact = 3;
            Time = 10 * 1000;
        }
    }
}
