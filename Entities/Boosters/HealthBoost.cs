using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameProject.Physics;
using GameProject.Properties;

namespace GameProject.Entities
{
    internal class HealthBoost : Booster
    {
        internal HealthBoost(Vector location) : base(location, Resources.HealthBoost)
        {

        }
    }
}
