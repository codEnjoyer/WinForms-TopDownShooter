using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameProject.Physics;
using GameProject.Properties;

namespace GameProject.Entities
{
    internal class DamageBoost : Booster
    {
        internal DamageBoost(Vector location) : base(location, Resources.DamageBoost)
        {
        }
    }
}
