using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameProject.Physics;
using GameProject.Properties;

namespace GameProject.Entities.Enemies
{
    internal class HeavyZombie : Enemy
    {
        internal HeavyZombie(Vector location) : base(location, Resources.SmallZombie)
        {
            Speed = 2;
        }
    }
}
