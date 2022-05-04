using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Numerics;
using GameProject.Physics;

namespace GameProject.Entities
{
    internal class Player
    {
        internal Vector Position { get; private set; }
        internal int Speed{ get; private set; }
        internal Vector Rotation { get; set; }

        internal Player()
        {
            Position = new Vector(100, 100);
            Speed = 10;
            Rotation = new Vector(0, 1);
        }
        internal Player(Vector position) : this()
        {
            Position = position;
        }

        internal void Move(int dx, int dy)
        {
            Position = new Vector(Position.X + dx, Position.Y + dy);
        }

        internal void Accelerate(int speed)
        {
            Speed += speed;
        }
    }
}
