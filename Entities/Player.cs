using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using GameProject.Physics;

namespace GameProject.Entities
{
    internal class Player
    {
        internal Vector Location { get; private set; }
        internal int Speed{ get; private set; }
        internal Vector Rotation { get; set; }
        internal bool Up, Left, Down, Right;
        internal Size Size { get; }
        internal Image Image { get; }

        internal Player()
        {
            Location = new Vector(100, 100);
            Speed = 5;
            Size = new Size(100, 50);
            Rotation = new Vector(0, 1);
            Image = Image.FromFile(@"C:\Учёба\Прога\GameProject\Sprites\survivor-idle_knife_0.png");
        }
        internal Player(Vector location) : this()
        {
            Location = new Vector(location.X - Image.Width / 2, location.Y - Image.Height / 2);
        }

        internal void Move()
        {
            if (Up)
                Location = new Vector(Location.X, Location.Y - Speed);
            else if (Left)
                Location = new Vector(Location.X - Speed, Location.Y);
            else if (Down)
                Location = new Vector(Location.X, Location.Y + Speed);
            else if (Right)
                Location = new Vector(Location.X + Speed, Location.Y);
        }

        internal void Accelerate(int speed)
        {
            Speed += speed;
        }
    }
}
