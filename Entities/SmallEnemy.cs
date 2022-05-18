using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameProject.Physics;

namespace GameProject.Entities
{
    internal class SmallEnemy : Enemy
    {
        internal SmallEnemy(Vector location, Image image) : base(location, image)
        {
            Speed = 10;
        }
    }
}
