using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Numerics;
using GameProject.Physics;

namespace GameProject.Entities
{
    internal class Player
    {
        internal Vector Location { get; private set; }
        internal int Speed{ get; private set; }
        internal Vector Rotation { get; set; }

        internal Player()
        {
            Location = new Vector(100, 100);
            Speed = 10;
            Rotation = new Vector(0, 1);
        }
        internal Player(Vector location) : this()
        {
            Location = location;
        }

        internal void Move(int dx, int dy)
        {
            Location = new Vector(Location.X + dx, Location.Y + dy);
        }

        internal void Accelerate(int speed)
        {
            Speed += speed;
        }
    }
}
